using planit_client_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf
{
    public class ActiveUser
    {

        public User LoggedUser { get; set; }
        private static ActiveUser instance;
        public static ActiveUser Instance
        {
            get
            {
                if (instance == null)
                    instance = new ActiveUser();
                return instance;
            }
        }

        private ActiveUser() { }

    }
}
