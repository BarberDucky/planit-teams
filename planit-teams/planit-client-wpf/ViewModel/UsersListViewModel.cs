using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Model;
using planit_client_wpf.Services;
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
        private ReadUser selectedUser;
        public bool leaveBoardCommandVisible;
        private int parentBoardId;

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

        public ReadUser SelectedUser
        {
            get { return selectedUser; }
            set
            {
                SetProperty(ref selectedUser, value);
            }
        }

        public bool LeaveBoardCommandVisible
        {
            get { return leaveBoardCommandVisible; }
            set { SetProperty(ref leaveBoardCommandVisible, value); }
        }

        #endregion

        #region Commands

        public CommandBase AddUserCommand { get; protected set; }
        public CommandBase<ReadUser> RemoveUserCommand { get; protected set; }
        public CommandBase LeaveBoardCommmand { get; private set; }


        #endregion

        public UsersListViewModel(ObservableCollection<ReadUser> users, bool isAdmin, int parentBoardId)
        {
            this.Users = users;
            this.isAdmin = isAdmin;
            this.parentBoardId = parentBoardId;
            leaveBoardCommandVisible = !isAdmin;
            AddUserCommand = new CommandBase(AddUserButtonClick, CanAddUser);
            RemoveUserCommand = new CommandBase<ReadUser>(RemoveUserButtonClick, CanRemoveUser);
            LeaveBoardCommmand = new CommandBase(OnLeaveBoardClick, CanLeaveBoard);
        }

        public async void AddUserButtonClick()
        {
            //Users.Add(new ReadUser(new DTOs.ReadUserDTO() { FirstName = "Damjan", LastName = "Trifunovic", Email = "dakica@gmail.com", Username = NewUsername }));
            //ShowMessageBox(null, "Dodajem " + NewUsername + " u lokalnu listu ali ne zovem api.");
            ////ReadUserDTO user = UserService.GetUserByUsername(NewUsername);
            ////if (user != null)
            ////{
            ////    bool succ = BoardService.AddUserToBoard(...nesto);
            ////    if(succ == true)
            ////    {
            ////        Users.Add(new ReadUser(user));
            ////    }
            ////}

            if (ActiveUser.IsActive == true)
            {
                AddUserBoardPermisionDTO dto = new AddUserBoardPermisionDTO()
                {
                    BoardId = parentBoardId,
                    Username = NewUsername
                };

                ReadUserDTO user = await PermissionService.CreatePermission(ActiveUser.Instance.LoggedUser.Token, dto);

                if (user != null)
                {
                    ShowMessageBox(null, "Dodat user");
                    Users.Add(new ReadUser(user));
                }
                else
                {
                    ShowMessageBox(null, "Error adding user.");
                }
            }
            else
            {
                ShowMessageBox(null, "Error getting user.");
            }
        }

        public bool CanAddUser()
        {
            return isAdmin && !String.IsNullOrWhiteSpace(NewUsername);
        }

        public async void RemoveUserButtonClick(ReadUser user)
        {
            //Da li mogu sam sebe da obrisem iz boarda iako nisam admin?
            //ReadUser u = Users.FirstOrDefault(x => x.username == user.username);
            //Users.Remove(u);
            //ShowMessageBox(null, "Brisem " + u.username + " iz lokalne liste ali ne zovem api.");

            if (ActiveUser.IsActive == true)
            {
                bool succ = await PermissionService.DeletePermission(ActiveUser.Instance.LoggedUser.Token,
                    parentBoardId, user.username);

                if (succ)
                {
                    ShowMessageBox(null, "Obrisan user");
                    ReadUser u = Users.FirstOrDefault(x => x.username == user.username);
                    Users.Remove(u);
                }
                else
                {
                    ShowMessageBox(null, "Error deleting user.");
                }
            }
            else
            {
                ShowMessageBox(null, "Error getting user.");
            }
        }

        //Privremeno dok se ne resi ko koga dodaje i brise
        public bool CanRemoveUser(ReadUser user)
        {
            return isAdmin && ActiveUser.IsActive == true
                && ActiveUser.Instance.LoggedUser.Username != user.Username;
        }

        public void OnLeaveBoardClick()
        {
            ShowMessageBox(null, "Pravim se da napustam board, ustv ne zovem api...");
        }

        public bool CanLeaveBoard()
        {
            return isAdmin == false;
        }

    }
}
