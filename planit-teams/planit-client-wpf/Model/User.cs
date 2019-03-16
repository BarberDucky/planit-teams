using planit_client_wpf.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Model
{
    public class User : BindableBase
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
}
