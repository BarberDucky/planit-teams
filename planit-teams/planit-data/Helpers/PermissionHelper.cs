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
        public static bool HasPermission(int boardId, int userId)
        {
            PermissionService service = new PermissionService();
            return service.GetPermission(boardId, userId);
        }
    }
}
