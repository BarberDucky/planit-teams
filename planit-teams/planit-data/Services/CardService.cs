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

        public List<ReadCardDTO> GetAllCardsOnList(int listId)
        {
            //if (!PermissionHelper.HasPermissionOnBoard(boardId, userId))
            //    return new List<ReadCardDTO>();

            using (UnitOfWork uw = new UnitOfWork())
            {
                List<Card> cards = uw.CardRepository
                    .Get(x => x.List.ListId == listId).ToList();

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

        public BasicCardDTO InsertCard(string username, CreateCardDTO dto)
        {
            //if (!PermissionHelper.HasPermissionOnList(dto.ListId, userId))
            //{
            //    return 0;
            //}

            BasicCardDTO cardDto = null;
            using (UnitOfWork uw = new UnitOfWork())
            {
                CardList list = uw.CardListRepository.GetById(dto.ListId);
                User user = uw.UserRepository.GetUserByUsername(username);

                Card card = CreateCardDTO.FromDTO(dto);

                if (user != null && list != null)
                {
                    card.User = user;
                    card.List = list;
                    uw.CardRepository.Insert(card);
                    if (uw.Save())
                    {
                        cardDto = new BasicCardDTO(card);
                        RabbitMQService.PublishToExchange(list.Board.ExchangeName,
                            new MessageContext(new CardMessageStrategy(cardDto, MessageType.Create)));

                        BoardNotificationService.ChangeBoardNotifications(list.Board.BoardId);
                    }
                }

            }
            return cardDto;
        }

        public bool UpdateCard(int cardId, string username, UpdateCardDTO dto)
        {
            bool succ = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Card card = uw.CardRepository.GetById(cardId);
                User user = uw.UserRepository.GetUserByUsername(username);
                if (card != null && user != null)
                {
                    card.Name = dto.Name;
                    card.Description = dto.Description;
                    card.DueDate = dto.DueDate;
                    uw.CardRepository.Update(card);

                    NotificationService notif = new NotificationService();
                    notif.CreateChangeNotification(new CreateNotificationDTO()
                    {
                        CardId = card.CardId,
                        UserId = user.UserId,
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

        public bool MoveCardToList(int cardId, int listId, string username)
        {
            bool succ = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Card card = uw.CardRepository.GetById(cardId);
                if (card.List.ListId != listId)
                {
                    CardList list = uw.CardListRepository.GetById(listId);
                    User user = uw.UserRepository.GetUserByUsername(username);
                    if (card != null && list != null && user != null)
                    {
                        list.Cards.Add(card);
                        uw.CardListRepository.Update(list);

                        if (uw.Save())
                        {
                            NotificationService notif = new NotificationService();
                            succ = notif.CreateMoveNotification(new CreateNotificationDTO()
                            {
                                CardId = cardId,
                                UserId = user.UserId,
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

        //TODO Nece da se obrise
        //Treba da se updateuje context
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

        public bool WatchCard(int cardId, string username)
        {
            bool succ = false;
            using (UnitOfWork u = new UnitOfWork())
            {
                User user = u.UserRepository.GetUserByUsername(username);
                Card card = u.CardRepository.GetById(cardId);
                if (user != null && card != null)
                {
                    card.ObserverUsers.Add(user);
                    succ = u.Save();
                }
            }

            return succ;
        }

        public bool UnwatchCard(int cardId, string username)
        {
            bool succ = false;
            using (UnitOfWork u = new UnitOfWork())
            {
                Card card = u.CardRepository.GetById(cardId);
                User user = u.UserRepository.GetUserByUsername(username);

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
