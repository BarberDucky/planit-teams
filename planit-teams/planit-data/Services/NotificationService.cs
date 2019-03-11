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
        //TODO ZASTO OVO NECE DA SE SNIMI LEPO
        public bool CreateMoveNotification(CreateNotificationDTO notificationDTO)
        {
            bool ret = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Notification obj = new Notification();
                User user = uw.UserRepository.GetById(notificationDTO.UserId);
                Card card = uw.CardRepository.GetById(notificationDTO.CardId);

                if (user != null && card != null)
                {

                    obj.CreatedByUser = user;
                    obj.Card = card;
                    obj.NotificationType = notificationDTO.NotificationType;

                    Board b = card.List.Board;

                    List<User> users = uw.PermissionRepository
                        .GetAllUsersWithPermissionOnBoard(b.BoardId);


                    ReadNotificationDTO dto = new ReadNotificationDTO(obj);
                    foreach (var u in users)
                    {
                        obj.BelongsToUserId = u.UserId;
                        uw.NotificationRepository.Insert(obj);
                        uw.Save();
                        RabbitMQService.PublishToExchange(u.ExchangeName,
                            new MessageContext(new NotificationMessageStrategy(dto, MessageType.Move)));
                    }

                    ret = uw.Save();
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
                Notification obj = new Notification();
                User user = uw.UserRepository.GetById(notificationDTO.UserId);
                Card card = uw.CardRepository.GetById(notificationDTO.CardId);

                if (user != null && card != null)
                {

                    obj.CreatedByUser = user;
                    obj.Card = card;
                    obj.NotificationType = notificationDTO.NotificationType;

                    ReadNotificationDTO dto = new ReadNotificationDTO(obj);

                    foreach (var u in card.ObserverUsers)
                    {
                        obj.BelongsToUserId = u.UserId;
                        uw.NotificationRepository.Insert(obj);
                        uw.Save();
                        RabbitMQService.PublishToExchange(u.ExchangeName,
                            new MessageContext(new NotificationMessageStrategy(dto, MessageType.Change)));
                    }
                    ret = uw.Save();
                }

                return ret;
            }
        }

        //TODO ova funckija nema smisla
        public List<ReadNotificationDTO> GetAllNotifications()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                List<Notification> notificationsFromDB = uw.NotificationRepository.GetAll();

                return ReadNotificationDTO.FromList(notificationsFromDB);
            }
        }

        public ReadNotificationDTO GetNotification(int notificationId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                Notification notificationFromDB = uw.NotificationRepository.GetById(notificationId);

                if (notificationFromDB == null)
                    return null;

                return new ReadNotificationDTO(notificationFromDB);
            }
        }

        public ReadNotificationDTO ReadNotification(int notificationId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                Notification notificationFromDB = uw.NotificationRepository.GetById(notificationId);

                if (notificationFromDB == null)
                    return null;

                notificationFromDB.IsRead = true;
                uw.NotificationRepository.Update(notificationFromDB);
                uw.Save();
                return new ReadNotificationDTO(notificationFromDB);
            }
        }

        public bool ReadAllNotifications(int userId)
        {
            try
            {
                bool succ = false;
                using(UnitOfWork unit = new UnitOfWork())
                {
                    User user = unit.UserRepository.GetById(userId);
                    if (user != null)
                    {
                        foreach(var n in user.Notifications)
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

        //TODO Zasto imamo brisanje notifikacija??
        public void DeleteNotification(int notificationId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                uw.NotificationRepository.Delete(notificationId);
                uw.Save();
            }
        }

    }
}
