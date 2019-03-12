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
        #region Should Delete
        public List<ReadCardDTO> GetAllCards()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                List<Card> cards = uw.CardRepository.GetAll();

                return ReadCardDTO.FromEntityList(cards);
            }
        }

        #endregion

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

        //TODO OVE DVE METODE SU ISTE
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

                if (user != null && list != null)
                {
                    card.User = user;
                    card.List = list;
                    uw.CardRepository.Insert(card);
                    if (uw.Save())
                    {
                        BasicCardDTO cardDto = new BasicCardDTO(card);
                        RabbitMQService.PublishToExchange(list.Board.ExchangeName,
                            new MessageContext(new CardMessageStrategy(cardDto, MessageType.Create)));

                        BoardNotificationService.ChangeBoardNotifications(list.Board.BoardId);
                    }
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

                    NotificationService notif = new NotificationService();
                    notif.CreateChangeNotification(new CreateNotificationDTO()
                    {
                        CardId = dto.CardId,
                        UserId = dto.UpdatedByUser,
                        NotificationType = NotificationType.Change
                    });

                    succ = uw.Save();

                    if (succ)
                    {
                        BasicCardDTO cardDto = new BasicCardDTO(card);
                        RabbitMQService.PublishToExchange(card.List.Board.ExchangeName,
                            new MessageContext(new CardMessageStrategy(cardDto, MessageType.Update)));

                        BoardNotificationService.ChangeBoardNotifications(card.List.Board.BoardId);
                    }
                }

            }
            return succ;
        }
        
        //TODO Prepraviti da radi sa tokenima
        public bool MoveCardToList(int cardId, int listId, int userId)
        {
            bool succ = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Card card = uw.CardRepository.GetById(cardId);
                if (card.List.ListId != listId)
                {
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

                            BasicCardDTO cardDto = new BasicCardDTO(card);
                            RabbitMQService.PublishToExchange(card.List.Board.ExchangeName,
                                new MessageContext(new CardMessageStrategy(cardDto, MessageType.Move)));

                            BoardNotificationService.ChangeBoardNotifications(card.List.Board.BoardId);
                        }
                    }
                }
            }
            return succ;
        }

        public bool DeleteCard(int id)
        {
            bool success = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Board board = uw.CardRepository.GetById(id).List.Board;
                uw.CardRepository.Delete(id);
                success = uw.Save();

                if (success)
                {
                    RabbitMQService.PublishToExchange(board.ExchangeName,
                        new MessageContext(new CardMessageStrategy(id)));

                    BoardNotificationService.ChangeBoardNotifications(board.BoardId);
                }
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
