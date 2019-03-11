using planit_data.DTOs;
using planit_data.Entities;
using planit_data.Helpers;
using planit_data.RabbitMQ;
using planit_data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Services
{
    public class CardListService
    {
        public List<ReadCardListDTO> GetAllCardList()
        {
            List<ReadCardListDTO> dtos;
            using (UnitOfWork unit = new UnitOfWork())
            {
                List<CardList> list = unit.CardListRepository.GetAll();
                dtos = ReadCardListDTO.FromEntityList(list);
            }

            return dtos;
        }

        public ReadCardListDTO GetCardList(int id)
        {
            ReadCardListDTO cardListDTO = null;
            using (UnitOfWork unit = new UnitOfWork())
            {
                CardList c = unit.CardListRepository.GetById(id);
                if (c != null)
                {
                    cardListDTO = new ReadCardListDTO(c);
                }

            }

            return cardListDTO;
        }

        //TODO prepraviti da radi sa tokenima
        public int InsertCardList(int userId, CreateCardListDTO cardListDto)
        {
            if (!PermissionHelper.HasPermissionOnBoard(cardListDto.BoardId, userId))
            {
                return 0;
            }

            CardList list = cardListDto.FromDTO();
            using (UnitOfWork unit = new UnitOfWork())
            {
                Board board = unit.BoardRepository.GetById(cardListDto.BoardId);

                if (board != null)
                {
                    list.Board = board;

                    unit.CardListRepository.Insert(list);
                    if (unit.Save())
                    {
                        BasicCardListDTO dto = new BasicCardListDTO(list);
                        RabbitMQService.PublishToExchange(board.ExchangeName,
                            new MessageContext(new CardListMessageStrategy(dto, MessageType.Create)));

                        BoardNotificationService.ChangeBoardNotifications(board.BoardId);
                    }
                }
            }

            return list.ListId;
        }

        //TODO prepraviti da radi sa tokenima
        public ReadCardListDTO GetCardListByUser(int cardId, int idUser)
        {
            if (!PermissionHelper.HasPermissionOnList(cardId, idUser))
            {
                return null;
            }

            return GetCardList(cardId);
        }

        //TODO prepraviti da radi sa tokenima
        public IEnumerable<ReadCardListDTO> GetAllCardListsOnBoard(int idBoard, int idUser)
        {
            if (!PermissionHelper.HasPermissionOnBoard(idBoard, idUser))
                return new List<ReadCardListDTO>();

            using (UnitOfWork uw = new UnitOfWork())
            {
                List<CardList> cards = uw.CardListRepository
                    .Get(x => x.Board.BoardId == idBoard).ToList();

                return ReadCardListDTO.FromEntityList(cards);
            }
        }

        public bool UpdateCardList(UpdateCardListDTO cardListDTO)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                CardList cardList = unit.CardListRepository.GetById(cardListDTO.ListId);

                if (cardList != null)
                {
                    cardList.Name = cardListDTO.Name;
                    cardList.Color = cardListDTO.Color;

                    unit.CardListRepository.Update(cardList);

                    ret = unit.Save();

                    if (ret)
                    {
                        BasicCardListDTO dto = new BasicCardListDTO(cardList);
                        RabbitMQService.PublishToExchange(cardList.Board.ExchangeName,
                            new MessageContext(new CardListMessageStrategy(dto, MessageType.Update)));

                        BoardNotificationService.ChangeBoardNotifications(cardList.Board.BoardId);
                    }
                }

            }

            return ret;
        }

        public bool DeleteCardList(int id)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                Board board = unit.CardListRepository.GetById(id).Board;
                string exchangeName = board.ExchangeName;
                int boardId = board.BoardId;


                unit.CardListRepository.Delete(id);
                ret = unit.Save();

                if (ret)
                {
                    RabbitMQService.PublishToExchange(exchangeName,
                        new MessageContext(new CardListMessageStrategy(id)));

                    BoardNotificationService.ChangeBoardNotifications(boardId);
                }

            }

            return ret;
        }
    }
}
