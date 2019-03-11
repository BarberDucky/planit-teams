using planit_data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Repository
{
    public class PermissionRepository : GenericRepository<Permission>
    {
        public PermissionRepository(ApplicationContext context)
            : base(context)
        {

        }

        public List<User> GetAllUsersWithPermissionOnBoard(int boardId)
        {
            List<User> users = new List<User>();

            users = Get(x => x.Board.BoardId == boardId)
                .Select(x => x.User).ToList();

            return users;
        }

        public Permission GetPermission(int boardId, int userId)
        {
            List<Permission> perms = Get(x => x.Board.BoardId == boardId && x.User.UserId == userId)
                .ToList();

            if (perms != null && perms.Count > 0)
            {
                return perms[0];
            }

            return null;
        }
    }
}
