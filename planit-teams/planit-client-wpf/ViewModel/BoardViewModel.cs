using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Model;
using planit_client_wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace planit_client_wpf.ViewModel
{
    public class BoardViewModel : ViewModelBase
    {
        private int boardId;
        private ReadBoard board;
        private ViewModelBase usersViewModel;
        private ViewModelBase cardListViewModel;

        #region Properties 

        public ReadBoard Board
        {
            get { return board; }
            set { SetProperty(ref board, value); }
        }

        public ViewModelBase UsersViewModel
        {
            get { return usersViewModel; }
            set { SetProperty(ref usersViewModel, value); }
        }

        public ViewModelBase CardListViewModel
        {
            get { return cardListViewModel; }
            set { SetProperty(ref cardListViewModel, value); }
        }

        #endregion

        #region Commands

        //public CommandBase NewListCommand { get; private set; }
        public CommandBase DeleteBoardCommand { get; private set; }
        

        public bool deleteBoardCommandVisible;
        public bool DeleteBoardCommandVisible
        {
            get { return deleteBoardCommandVisible; }
            protected set
            {
                SetProperty(ref deleteBoardCommandVisible, value);
                DeleteBoardCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Actions and Func

        public Action<int> BoardDeleted;

        #endregion

        public BoardViewModel(int id, Action<int> boardDeleted)
        {
            boardId = id;
            //NewListCommand = new CommandBase(OnNewListClick);
            DeleteBoardCommand = new CommandBase(OnDeleteBoardClick);
            DeleteBoardCommandVisible = false;
            BoardDeleted = boardDeleted;
            UsersViewModel = new EmptyViewModel();
            CardListViewModel = new EmptyViewModel();

            LoadBoard();
        }

        public async Task LoadBoard()
        {
            if (ActiveUser.IsActive == true)
            {
                ReadBoardDTO dto = await BoardService.GetBoard(ActiveUser.Instance.LoggedUser.Token, boardId);
                if (dto != null)
                {
                    Board = new ReadBoard(dto);
                    if (Board.IsAdmin == true)
                    {
                        DeleteBoardCommandVisible = true;
                    }

                    UsersViewModel = new UsersListViewModel(Board.Users, Board.IsAdmin);
                    CardListViewModel = new CardListListViewModel();

                }
                else
                {
                    ShowMessageBox(null, "Error getting board.");
                }
            }
            else
            {
                ShowMessageBox(null, "Error getting user.");
            }
        }

        //public void OnNewListClick()
        //{
        //    if (ActiveUser.IsActive == true)
        //    {
        //        ShowMessageBox(null, "Kreirala se lista");
        //    }
        //    else
        //    {
        //        ShowMessageBox(null, "Error getting user.");
        //    }
        //}

        public void OnDeleteBoardClick()
        {
            if (ActiveUser.IsActive == true)
            {
                Action<MessageBoxResult> yesno = (MessageBoxResult res) =>
                {
                    if (res == MessageBoxResult.Yes)
                        BoardDeleted?.Invoke(boardId);
                };

                ShowMessageBox(yesno, "Are you sure you want to delete this board?", "Warning", MessageBoxButton.YesNo);
            }
            else
            {
                ShowMessageBox(null, "Error getting user.");
            }
        }

    }
}
