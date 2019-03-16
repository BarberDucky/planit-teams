using planit_client_wpf.Base;
using planit_client_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private ViewModelBase leftViewModel;
        private ViewModelBase centerViewModel;

        #region Properies

        public ViewModelBase LeftViewModel
        {
            get { return leftViewModel; }
            set { SetProperty(ref leftViewModel, value); }
        }

        public ViewModelBase CenterViewModel
        {
            get { return centerViewModel; }
            set { SetProperty(ref centerViewModel, value); }
        }

        #endregion

        #region Commands

        public CommandBase LogoutCommand { get; protected set; }

        #endregion

        #region Actions and Func

        public Action GoToLogin;

        #endregion

        public HomeViewModel(Action goToLogin)
        {
            //Init komande
            LogoutCommand = new CommandBase(OnLogoutButtonClick, CanLogout);
            GoToLogin = goToLogin;

            //Init polja
            LeftViewModel = new BoardListViewModel(OnBoardSelected);
            CenterViewModel = new EmptyViewModel();
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

        public void OnBoardSelected(int boardId)
        {
            //CenterViewModel = new BoardViewModel(boardId);
            CenterViewModel = new BoardViewModel();
        }
    }
}
