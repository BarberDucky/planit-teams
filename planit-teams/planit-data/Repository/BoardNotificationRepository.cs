using planit_data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Repository
{
    public class BoardNotificationRepository : GenericRepository<BoardNotification>
    {
        public BoardNotificationRepository(ApplicationContext context)
            : base(context)
        {

        }

        public List<User> GetAllUsersWithBoard(int boardId)
        {
            List<User> users = new List<User>();

            users = Get(x => x.Board.BoardId == boardId)
                .Select(x => x.User).ToList();

            return users;
        }

        public List<BoardNotification> GetBoardNotificationsByBoard(int boardId)
        {
            List<BoardNotification> boardNotifs =
                Get(x => x.Board.BoardId == boardId).ToList();

            return boardNotifs;
        }

        public BoardNotification GetBoardNotification(int boardId, int userId)
        {
            List<BoardNotification> notifs =
                Get(x => x.User.UserId == userId && x.Board.BoardId == boardId)
                .ToList();

            if (notifs != null && notifs.Count > 0)
            {
                return notifs[0];
            }

            return null;
        }

        public void Delete(int boardId, int userId)
        {
            BoardNotification notif = GetBoardNotification(boardId, userId);

            if (notif != null)
            {
                Delete(notif.BoardNotificationId);
            }
        }
    }
}
