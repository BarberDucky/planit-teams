using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_data.Entities;

namespace planit_data.DTOs
{

    public class CreateUserDTO
    {
        public String Username { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }

    public class ReadUserDTO
    {
        public int UserID { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Card> Cards { get; set; }
        public List<Card> WatchedCards { get; set; }
        public List<Permission> Permissions { get; set; }

        public ReadUserDTO (User user)
        {
            UserID = user.UserId;
            Username = user.IdentificationUser.UserName;
            Email = user.IdentificationUser.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;

        }
    }

    public class UpdateUserDTO
    {
        public int UserID { get; set; }
        public String Username { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Card> Cards { get; set; }
        public List<Card> WatchedCards { get; set; }
        public List<Permission> Permissions { get; set; }
    }

    public class DeleteUserDTO
    {
        public int UserID { get; set; }
    }

}