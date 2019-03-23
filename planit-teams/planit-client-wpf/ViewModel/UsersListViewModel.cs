using planit_client_wpf.Base;
using planit_client_wpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class UsersListViewModel : ViewModelBase
    {
        private ObservableCollection<ReadUser> users;
        private bool isAdmin;
        private string newUsername;

        #region Properties

        public ObservableCollection<ReadUser> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }

        public bool IsAdmin
        {
            get { return isAdmin; }
            set { SetProperty(ref isAdmin, value); }
        }

        public string NewUsername
        {
            get { return newUsername; }
            set
            {
                SetProperty(ref newUsername, value);
                AddUserCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public CommandBase AddUserCommand { get; protected set; }

        #endregion

        public UsersListViewModel(ObservableCollection<ReadUser> users, bool isAdmin)
        {
            this.Users = users;
            this.isAdmin = isAdmin;
            AddUserCommand = new CommandBase(AddUserButtonClick, CanAddUser);
        }

        public void AddUserButtonClick()
        {
            ShowMessageBox(null, "Dodaje se " + NewUsername);
            //Users.Add(new ReadUser(new DTOs.ReadUserDTO() { FirstName = "Damjan", LastName = "Trifunovic", Email = "dakica@gmail.com", Username = "dakica" }));
            //Show dialog nekako, get dialog result -> username usera koji se dodaje...
            //ReadUserDTO user = UserService.GetUser(username);
            //if(user != null)
            //{
            //    Users.Add(new ReadUser(user));
            //}
        }

        public bool CanAddUser()
        {
            return isAdmin && !String.IsNullOrWhiteSpace(NewUsername);
        }

    }
}
