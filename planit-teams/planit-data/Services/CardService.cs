using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_data.DTOs;
using planit_data.Entities;
using planit_data.Helpers;
using planit_data.RabbitMQ;
using planit_data.Repository;

namespace planit_data.Services
{
    public class CardService
    {

        public List<ReadCardDTO> GetAllCards()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                List<Card> cards = uw.CardRepository.GetAll();

                return ReadCardDTO.FromEntityList(cards);
            }
        }

        public List<ReadCardDTO> GetAllCardsOnBoard(int boardId, int userId)
        {
            if (!PermissionHelper.HasPermissionOnBoard(boardId, userId))
                return new List<ReadCardDTO>();

            using (UnitOfWork uw = new UnitOfWork())
            {
                List<Card> cards = uw.CardRepository.Get(x => x.List.Board.BoardId == boardId).ToList();

                return ReadCardDTO.FromEntityList(cards);
            }
        }

        public ReadCardDTO GetCardById(int id)
        {
            ReadCardDTO dto;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Card card = uw.CardRepository.GetById(id);
                if (card == null) return null;
                dto = new ReadCardDTO(card);
            }
            return dto;
        }

        public ReadCardDTO GetCardByUser(int cardId, int idUser)
        {
            if (!PermissionHelper.HasPermissionOnCard(cardId, idUser))
            {
                return null;
            }

            return GetCardById(cardId);
        }

        /*
         * //Treba get all za sve kartice 1 boarda
        public List<ReadCommentDTO> GetAllComments()
        {
            List<ReadCommentDTO> listDTO = new List<ReadCommentDTO>();
            List<Comment> retList;
            using (UnitOfWork uw = new UnitOfWork())
            {
                retList = uw.CommentRepository.GetAll();
                foreach(Comment c in retList)
                {
                    listDTO.Add(new ReadCommentDTO(c));
                }
            }
            return listDTO;
        }*/

        public int InsertCard(int userId, CreateCardDTO dto)
        {
            if (!PermissionHelper.HasPermissionOnList(dto.ListId, userId))
            {
                return 0;
            }
            Card card;
            using (UnitOfWork uw = new UnitOfWork())
            {
                CardList list = uw.CardListRepository.GetById(dto.ListId);
                User user = uw.UserRepository.GetById(dto.UserId);
                card = CreateCardDTO.FromDTO(dto);
                if (user == null || card == null || list == null) return 0;
                card.User = user;
                card.List = list;
                uw.CardRepository.Insert(card);
                if (uw.Save())
                {
                    Message message = new Message()
                    {
                        MessageType = MessageType.Card,
                        ObjectId = card.CardId
                    };
                    RabbitMQ.RabbitMQService.PublishToExchange(list.Board.ExchangeName, message);
                }
            }
            return card.CardId;
        }

        public bool UpdateCard(UpdateCardDTO dto)
        {
            bool succ = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Card card = uw.CardRepository.GetById(dto.CardId);
                if (card != null)
                {
                    card.Name = dto.Name;
                    card.Description = dto.Description;
                    card.DueDate = dto.DueDate;
                    uw.CardRepository.Update(card);
                    succ = uw.Save();
                }

            }
            return succ;
        }

        public bool MoveCardToList(int cardId, int listId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                Card card = uw.CardRepository.GetById(cardId);
                CardList list = uw.CardListRepository.GetById(listId);
                if (card == null || list == null) return false;
                list.Cards.Add(card);
                uw.CardListRepository.Update(list);
                uw.Save();
            }
            return true;
        }

        public bool DeleteCard(int id)
        {
            bool success = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                success = uw.CardRepository.Delete(id);
                uw.Save();
            }
            return success;
        }
    }
}
