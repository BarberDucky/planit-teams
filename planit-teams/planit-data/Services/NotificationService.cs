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
                obj.User = uw.UserRepository.GetById(notificationDTO.UserId);
                obj.Card = uw.CardRepository.GetById(notificationDTO.CardId);

                uw.NotificationRepository.Insert(obj);
                uw.Save();
            }
        }

        public ReadNotificationDTO GetNotification (int notificationId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            { 
                Notification notificationFromDB = uw.NotificationRepository.GetById(notificationId);

                if (notificationFromDB != null)
                {
                    return new ReadNotificationDTO(notificationFromDB);
                }
                else
                {
                    return null;
                }
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
