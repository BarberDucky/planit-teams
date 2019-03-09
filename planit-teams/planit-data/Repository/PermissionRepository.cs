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
    }
}
