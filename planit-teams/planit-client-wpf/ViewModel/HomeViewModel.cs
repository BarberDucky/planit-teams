using planit_client_wpf.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public CommandBase LogoutCommand { get; protected set; }

        public HomeViewModel()
        {
            LogoutCommand = new CommandBase(OnLogoutButtonClick, CanLogout);
        }

        public void OnLogoutButtonClick()
        {
            ActiveUser.Instance.LoggedUser = null;
            MessengerBus.MessengerBusInstance.OpenLoginWindowDelegate?.Invoke();
        }

        public bool CanLogout()
        {
            return ActiveUser.Instance.LoggedUser != null;
        }
    }
}
