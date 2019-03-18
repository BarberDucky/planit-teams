using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Model
{
    public class TokenUser : BindableBase
    {
        private string username;
        private string token;

        #region Properties

        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        public string Token
        {
            get { return token; }
            set { SetProperty(ref token, value); }
        }

        #endregion
    }

    public class ReadUser : BindableBase
    {
        public string username;
        public string email;
        public string firstName;
        public string lastName;

        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        public string FirstName
        {
            get { return firstName; }
            set { SetProperty(ref firstName, value); }
        }

        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

        public string Display
        {
            get
            {
                return String.Format("{0} ({1} {2}), {3}", Username, FirstName, LastName, Email);
            }
        }


        public ReadUser(ReadUserDTO dto)
        {
            if(dto != null)
            {
                Username = dto.Username;
                Email = dto.Email;
                FirstName = dto.FirstName;
                LastName = dto.LastName;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} ({1} {2}), {3}", Username, FirstName, LastName, Email);
        }
    }
}
