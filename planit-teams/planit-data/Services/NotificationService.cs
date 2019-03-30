using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_data.DTOs;
using planit_data.Entities;
using planit_data.RabbitMQ;
using planit_data.Repository;

namespace planit_data.Services
{
    public class NotificationService
    {
        #region Should Delete
        public List<ReadNotificationDTO> GetAllNotifications()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                List<Notification> notificationsFromDB = uw.NotificationRepository.GetAll();

                return ReadNotificationDTO.FromList(notificationsFromDB);
            }
        }

        public void DeleteNotification(int notificationId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                uw.NotificationRepository.Delete(notificationId);
                uw.Save();
            }
        }

        #endregion

        public bool CreateMoveNotification(CreateNotificationDTO notificationDTO)
        {
            bool ret = false;
            using (UnitOfWork uw = new UnitOfWork())
            {

                User user = uw.UserRepository.GetById(notificationDTO.UserId);
                Card card = uw.CardRepository.GetById(notificationDTO.CardId);

                if (user != null && card != null)
                {
                    Board b = card.List.Board;

                    List<User> users = uw.PermissionRepository
                        .GetAllUsersWithPermissionOnBoard(b.BoardId);

                    List<Notification> notifs = new List<Notification>();

                    foreach (var u in users)
                    {
                        Notification obj = new Notification
                        {
                            CreatedByUser = user,
                            Card = card,
                            NotificationType = notificationDTO.NotificationType,
                            BelongsToUserId = u.UserId
                        };

                        uw.NotificationRepository.Insert(obj);
                        notifs.Add(obj);
                    }

                    ret = uw.Save();

                    if (ret)
                    {
                        foreach (var n in notifs)
                        {
                            RabbitMQService.PublishToExchange(n.BelongsToUser.ExchangeName,
                            new MessageContext(new NotificationMessageStrategy(
                                new ReadNotificationDTO(n), MessageType.Move)));
                        }
                    }
                }
            }
            return ret;
        }

        //TODO srediti ove dve metode
        public bool CreateChangeNotification(CreateNotificationDTO notificationDTO)
        {
            bool ret = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                User user = uw.UserRepository.GetById(notificationDTO.UserId);
                Card card = uw.CardRepository.GetById(notificationDTO.CardId);

                if (user != null && card != null)
                {
                    List<Notification> notifs = new List<Notification>();

                    foreach (var u in card.ObserverUsers)
                    {

                        Notification obj = new Notification
                        {
                            CreatedByUser = user,
                            Card = card,
                            NotificationType = notificationDTO.NotificationType,
                            BelongsToUserId = u.UserId
                        };

                        uw.NotificationRepository.Insert(obj);
                        notifs.Add(obj);
                    }
                    ret = uw.Save();

                    if (ret)
                    {
                        foreach (var n in notifs)
                        {
                            RabbitMQService.PublishToExchange(n.BelongsToUser.ExchangeName,
                            new MessageContext(new NotificationMessageStrategy
                            (new ReadNotificationDTO(n), MessageType.Move)));
                        }
                    }
                }

                return ret;
            }
        }

        public ReadNotificationDTO GetNotification(int notificationId)
        {
            ReadNotificationDTO dto = null;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Notification notificationFromDB = uw.NotificationRepository
                    .GetById(notificationId);

                if (notificationFromDB != null)
                    dto = new ReadNotificationDTO(notificationFromDB);
            }

            return dto;
        }

        public bool ReadNotification(int notificationId)
        {
            bool succ = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Notification notificationFromDB = uw.NotificationRepository.GetById(notificationId);

                if (notificationFromDB != null)
                {
                    notificationFromDB.IsRead = true;
                    uw.NotificationRepository.Update(notificationFromDB);
                    succ = uw.Save();
                }
            }

            return succ;
        }

        public bool ReadAllNotifications(string username)
        {
            try
            {
                bool succ = false;
                using (UnitOfWork unit = new UnitOfWork())
                {
                    User user = unit.UserRepository.GetUserByUsername(username);
                    if (user != null)
                    {
                        foreach (var n in user.Notifications)
                        {
                            n.IsRead = true;
                            unit.NotificationRepository.Update(n);
                        }
                    }

                    succ = unit.Save();
                }

                return succ;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
