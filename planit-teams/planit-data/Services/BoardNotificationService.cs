using planit_data.Entities;
using planit_data.RabbitMQ;
using planit_data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Services
{
    public class BoardNotificationService
    {
        public static void ChangeBoardNotifications(int boardId)
        {
            using(UnitOfWork unit = new UnitOfWork())
            {
                List<BoardNotification> notifs = unit.BoardNotificationRepository
                    .GetBoardNotificationsByBoard(boardId);

                foreach(var n in notifs)
                {
                    n.IsRead = false;
                    unit.BoardNotificationRepository.Update(n);

                    RabbitMQService.PublishToExchange(n.User.ExchangeName,
                           new MessageContext(new BoardNotificationMessageStrategy(boardId)));
                }

                unit.Save();
            }
        }

        public bool ReadBoard(int boardId, string username)
        {
            bool succ = false;

            using (UnitOfWork unit = new UnitOfWork())
            {
                BoardNotification notif = unit.BoardNotificationRepository
                    .GetBoardNotification(boardId, username);

                if (notif != null)
                {
                    notif.IsRead = true;
                    unit.BoardNotificationRepository.Update(notif);

                    succ = unit.Save();
                }
            }

            return succ;
        }
    }
}
