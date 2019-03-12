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

        public User FromDTO()
        {
            return new User()
            {
                FirstName = FirstName,
                LastName = LastName
            };
        }
    }

    public class ReadUserDTO
    {
        //public int UserID { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public ReadUserDTO(User user)
        {
           // UserID = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;

            if (user.IdentificationUser != null)
            {
                Username = user.IdentificationUser.UserName;
                Email = user.IdentificationUser.Email;
            }
        }

        public static List<ReadUserDTO> FromEntityList(List<User> users)
        {
            List<ReadUserDTO> list = new List<ReadUserDTO>();
            foreach (User u in users)
            {
                if (u != null)
                {
                    list.Add(new ReadUserDTO(u));
                }

            }

            return list;
        }

    }

    public class UpdateUserDTO
    {
       // public int UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }
}