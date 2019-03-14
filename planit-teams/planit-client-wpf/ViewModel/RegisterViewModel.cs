using planit_client_wpf.Base;
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

        public CommandBase RegisterCommand { get; private set; }
        public CommandBase BackCommand { get; private set; }

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

        #endregion

        public RegisterViewModel()
        {
            RegisterCommand = new CommandBase(OnRegisterButtonClick, CanRegister);
            BackCommand = new CommandBase(OnBackButtonClick);
        }

        public void OnRegisterButtonClick()
        {
            //Api call, pokupim rezultat
            ShowMessageBox(null, "Registracija uspesno zavrsena");
            MessengerBus.MessengerBusInstance.OpenLoginWindowDelegate?.Invoke();
        }

        public bool CanRegister()
        {
            return !String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(password)
                && !String.IsNullOrWhiteSpace(email) && !String.IsNullOrWhiteSpace(firstName)
                && !String.IsNullOrWhiteSpace(lastName);
        }

        public void OnBackButtonClick()
        {
            MessengerBus.MessengerBusInstance.OpenLoginWindowDelegate?.Invoke();
        }

    }
}
