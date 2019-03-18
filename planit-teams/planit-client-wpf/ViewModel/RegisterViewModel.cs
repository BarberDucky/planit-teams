using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private string username;
        private string password;
        private string email;
        private string firstName;
        private string lastName;
        private bool canRegister;

        #region Properties 

        public string Username
        {
            get { return username; }
            set
            {
                SetProperty<string>(ref username, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                SetProperty<string>(ref password, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                SetProperty<string>(ref email, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                SetProperty<string>(ref firstName, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                SetProperty<string>(ref lastName, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanRegisterFlag
        {
            get { return canRegister; }
            set
            {
                SetProperty<bool>(ref canRegister, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands 

        public CommandBase RegisterCommand { get; private set; }
        public CommandBase BackCommand { get; private set; }

        #endregion

        #region Actions and Func

        public Action RegisterButtonAction;

        #endregion

        public RegisterViewModel(Action returnToLogin)
        {
            RegisterCommand = new CommandBase(OnRegisterButtonClick, CanRegister);
            CanRegisterFlag = true;
            BackCommand = new CommandBase(OnBackButtonClick);
            RegisterButtonAction = returnToLogin;
        }

        public async void OnRegisterButtonClick()
        {
            CanRegisterFlag = false;
            CreateUserDTO createUserDTO = new CreateUserDTO() { Username = Username, Password = Password, Email = Email, FirstName = FirstName, LastName = LastName };

            bool isRegisterSuccessful = await UserService.RegisterUser(createUserDTO);

            if (isRegisterSuccessful)
            {
                ShowMessageBox(null, "Registration successful");
                RegisterButtonAction?.Invoke();
            }
            else
            {
                ShowMessageBox(null, "Registration unsuccessful");
            }
            CanRegisterFlag = true;
        }

        public bool CanRegister()
        {
            return !String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(password)
                && !String.IsNullOrWhiteSpace(email) && !String.IsNullOrWhiteSpace(firstName)
                && !String.IsNullOrWhiteSpace(lastName) && CanRegisterFlag == true;
        }

        public void OnBackButtonClick()
        {
            RegisterButtonAction?.Invoke();
        }

    }
}
