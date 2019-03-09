using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_data.DTOs;
using planit_data.Entities;
using planit_data.Repository;

namespace planit_data.Services
{
    public class NotificationService
    {
        //TODO publish na notif kanal - user kanal
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

                    foreach (var u in users)
                    {
                        u.Notifications.Add(obj);
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

                    foreach (var u in card.ObserverUsers)
                    {
                        u.Notifications.Add(obj);
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
