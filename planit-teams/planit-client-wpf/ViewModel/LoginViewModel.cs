using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Model;
using planit_client_wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string username;
        private string password;

        #region Properties

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
        #endregion

        #region Commands

        public CommandBase LoginCommand { get; private set; }
        public CommandBase RegisterCommand { get; private set; }

        #endregion

        #region Action and Func

        public Action LoginButtonAction;
        public Action RegisterAction;

        #endregion

        public LoginViewModel(Action goToHome, Action goToRegister)
        {
            LoginCommand = new CommandBase(OnLoginButtonClick, CanLogin);
            RegisterCommand = new CommandBase(OnRegisterButtonClick);
            LoginButtonAction = goToHome;
            RegisterAction = goToRegister;
        }


        private async void OnLoginButtonClick()
        {
            CredentialsUserDTO credentials = new CredentialsUserDTO() { username = Username, password = Password, grant_type = "password" };

            TokenUserDTO token = await UserService.LoginUser(credentials);
            if (token.success)
            {
                ActiveUser.Instance.LoggedUser = new User
                {
                    Username = token.userName,
                    Token = "Bearer " + token.access_token
                };
                ShowMessageBox(null, "Login successful");
                LoginButtonAction?.Invoke();
            } 
            else
            {
                ShowMessageBox(null, "Login failed");
            }

        }

        private bool CanLogin()
        {
            return !String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(password);
        }

        private void OnRegisterButtonClick()
        {
            RegisterAction?.Invoke();
        }

    }
}
