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
        private static readonly object lockInstance = new object();

        public TokenUser LoggedUser { get; set; }

        private static ActiveUser instance;

        public static ActiveUser Instance
        {
            get
            {
                lock(lockInstance)
                {
                    if (instance == null)
                        instance = new ActiveUser();
                    return instance;
                }

            }
        }

        public static bool IsActive
        {
            get { return Instance.LoggedUser != null && Instance.LoggedUser.Token != null; }
        }

    }
}
