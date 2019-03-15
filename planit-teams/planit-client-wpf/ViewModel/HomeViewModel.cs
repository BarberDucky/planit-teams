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
        #region Commands

        public CommandBase LogoutCommand { get; protected set; }

        #endregion

        #region Actions and Func

        public Action GoToLogin;

        #endregion

        public HomeViewModel(Action goToLogin)
        {
            LogoutCommand = new CommandBase(OnLogoutButtonClick, CanLogout);
            GoToLogin = goToLogin;
        }

        public void OnLogoutButtonClick()
        {
            ActiveUser.Instance.LoggedUser = null;
            GoToLogin?.Invoke();
        }

        public bool CanLogout()
        {
            return ActiveUser.Instance.LoggedUser != null;
        }
    }
}
