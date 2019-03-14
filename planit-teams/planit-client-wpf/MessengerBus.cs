using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf
{
    public class MessengerBus
    {
        private static MessengerBus bus;

        public static MessengerBus MessengerBusInstance
        {
            get
            {
                if (bus == null)
                    bus = new MessengerBus();
                return bus;
            }
        }

        private MessengerBus() { }

        public delegate void OpenHomeWindow();
        public delegate void OpenLoginWindow();
        public delegate void OpenRegisterWindow();

        public OpenHomeWindow OpenHomeWindowDelegate { get; set; }
        public OpenLoginWindow OpenLoginWindowDelegate { get; set; }
        public OpenRegisterWindow OpenRegisterWindowDelegate { get; set; }

    }
}
