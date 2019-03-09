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

        //TODO prepraviti da radi sa tokenima
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

        //TODO prepraviti da radi sa tokenima
        public ReadCardDTO GetCardByUser(int cardId, int idUser)
        {
            if (!PermissionHelper.HasPermissionOnCard(cardId, idUser))
            {
                return null;
            }

            return GetCardById(cardId);
        }

        //TODO prepraviti da radi sa tokenima
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
                    ReadCardDTO cardDto = new ReadCardDTO(card);
                    RabbitMQService.PublishToExchange(list.Board.ExchangeName, new MessageContext(new CardMessageStrategy(cardDto)));
                }
            }
            return card.CardId;
        }

        //TODO publish na exchange
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

                    NotificationService notif = new NotificationService();
                    notif.CreateChangeNotification(new CreateNotificationDTO()
                    {
                        CardId = dto.CardId,
                        UserId = dto.UpdatedByUser,
                        NotificationType = NotificationType.Change
                    });
                   
                    succ = uw.Save();
                }

            }
            return succ;
        }

        //TODO publish na board
        //TODO Prepraviti da radi sa tokenima
        public bool MoveCardToList(int cardId, int listId, int userId)
        {
            bool succ = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Card card = uw.CardRepository.GetById(cardId);
                CardList list = uw.CardListRepository.GetById(listId);
                if (card != null && list != null)
                {
                    list.Cards.Add(card);
                    uw.CardListRepository.Update(list);

                    if (uw.Save())
                    {
                        NotificationService notif = new NotificationService();
                        succ = notif.CreateMoveNotification(new CreateNotificationDTO()
                        {
                            CardId = cardId,
                            UserId = userId,
                            NotificationType = NotificationType.Move
                        });
                    }
                }

            }
            return true;
        }

        //TODO publish na board
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

        //TODO Rad sa tokenima
        public bool WatchCard(int cardId, int userId)
        {
            bool succ = false;
            using (UnitOfWork u = new UnitOfWork())
            {
                User user = u.UserRepository.GetById(userId);
                Card card = u.CardRepository.GetById(cardId);
                if (user != null && card != null)
                {
                    card.ObserverUsers.Add(user);
                    succ = u.Save();
                }
            }

            return succ;
        }

        //TODO Rad sa tokenima
        public bool UnwatchCard(int cardId, int userId)
        {
            bool succ = false;
            using (UnitOfWork u = new UnitOfWork())
            {
                Card card = u.CardRepository.GetById(cardId);
                User user = u.UserRepository.GetById(userId);

                if (user != null && card != null)
                {
                    succ = card.ObserverUsers.Remove(user);
                    u.Save();
                }
            }

            return succ;
        }
    }
}
