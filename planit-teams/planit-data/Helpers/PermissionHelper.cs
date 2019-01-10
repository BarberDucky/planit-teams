using planit_data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Helpers
{
    public class PermissionHelper
    {
        public static bool HasPermissionOnBoard(int boardId, int userId)
        {
            PermissionService service = new PermissionService();
            return service.GetPermission(boardId, userId);
        }

        public static bool HasPermissionOnList(int listId, int userId)
        {
            PermissionService service = new PermissionService();
            return service.GetPermissionOnList(listId, userId);
        }
    }
}
