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
        public void CreateNotification (CreateNotificationDTO notificationDTO)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                Notification obj = notificationDTO.FromDTO();
                User user = uw.UserRepository.GetById(notificationDTO.UserId);
                Card card = uw.CardRepository.GetById(notificationDTO.CardId);

                if(user!=null && card!=null)
                {
                    obj.User = user;
                    obj.Card = card;
                    uw.NotificationRepository.Insert(obj);
                    uw.Save();
                }
            }
        }

        public List<ReadNotificationDTO> GetAllNotifications ()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                List<Notification> notificationsFromDB = uw.NotificationRepository.GetAll();

                return ReadNotificationDTO.FromList(notificationsFromDB);
            }
        }

        public ReadNotificationDTO GetNotification (int notificationId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            { 
                Notification notificationFromDB = uw.NotificationRepository.GetById(notificationId);

                if (notificationFromDB == null)
                    return null;

                return new ReadNotificationDTO(notificationFromDB);
            }
        }

        public ReadNotificationDTO ReadNotification (int notificationId)
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
