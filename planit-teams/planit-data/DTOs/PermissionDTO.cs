using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.DTOs
{
    //Ne treba
    public class UpdatePermissionDTO
    {
        public bool IsAdmin { get; set; }
        public int BoardId { get; set; }
        public int UserId { get; set; }
    }

    public class AddUserBoardPermisionDTO
    {
        public int BoardId { get; set; }
        public string Username { get; set; }
    }
}
