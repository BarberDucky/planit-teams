using planit_client_wpf.Base;
using planit_client_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string username;
        private string password;

        public string Username
        {
            get { return username; }
            set
            {
                SetProperty<string>(ref username, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                SetProperty<string>(ref password, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }


        public CommandBase LoginCommand { get; private set; }
        public CommandBase RegisterCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new CommandBase(OnLoginButtonClick, CanLogin);
            RegisterCommand = new CommandBase(OnRegisterButtonClick);
        }

        #region Logion

        public void OnLoginButtonClick()
        {
            //Api call, pokupim token
            ActiveUser.Instance.LoggedUser = new User
            {
                Username = Username,
                Token = "Poslala mi baza neka slovca"
            };
            MessengerBus.MessengerBusInstance.OpenHomeWindowDelegate?.Invoke();
        }

        public bool CanLogin()
        {
            return !String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(password);
        }

        #endregion

        #region Register

        public void OnRegisterButtonClick()
        {
            MessengerBus.MessengerBusInstance.OpenRegisterWindowDelegate?.Invoke();
        }

        #endregion
    }
}
