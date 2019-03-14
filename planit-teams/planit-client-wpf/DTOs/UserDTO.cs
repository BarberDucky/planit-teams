using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.DTOs
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
        //public int UserID { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }

    public class UpdateUserDTO
    {
        // public int UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }

    public class CredentialsUserDTO
    {
        public String username { get; set; }
        public String password { get; set; }
        public String grant_type { get; set; }
    }

    public class TokenUserDTO
    {
        public String access_token { get; set; }
        public String userName { get; set; }
        public bool success { get; set; }
    }
}
