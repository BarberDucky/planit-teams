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
        private ShortBoard shortBoard;
        private ReadBoard board;
        private ViewModelBase usersViewModel;
        private ViewModelBase cardListViewModel;

        #region Properties 

        public ShortBoard ShortBoard
        {
            get { return shortBoard; }
            set { SetProperty(ref shortBoard, value); }
        }

        public ReadBoard Board
        {
            get { return board; }
            set { SetProperty(ref board, value); }
        }

        public string Name
        {
            get { return shortBoard.Name; }
            set
            {
                Board.Name = value;
                ShortBoard.Name = value;
            }
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

        public CommandBase DeleteBoardCommand { get; private set; }
        public CommandBase RenameBoardCommand { get; private set; }

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

        public BoardViewModel(ShortBoard shortBoard, Action<int> boardDeleted)
        {
            ShortBoard = shortBoard;
            DeleteBoardCommand = new CommandBase(OnDeleteBoardClick);
            DeleteBoardCommandVisible = false;
            BoardDeleted = boardDeleted;
            RenameBoardCommand = new CommandBase(OnRenameBoardClick, CanRenameBoard);
            UsersViewModel = new EmptyViewModel();
            CardListViewModel = new EmptyViewModel();

            LoadBoard();
        }

        public async Task LoadBoard()
        {
            if (ActiveUser.IsActive == true && ShortBoard != null)
            {
                ReadBoardDTO dto = await BoardService.GetBoard(ActiveUser.Instance.LoggedUser.Token, ShortBoard.BoardId);
                if (dto != null)
                {
                    Board = new ReadBoard(dto);
                    if (Board.IsAdmin == true)
                    {
                        DeleteBoardCommandVisible = true;
                    }

                    UsersViewModel = new UsersListViewModel(Board.Users, Board.IsAdmin);
                    CardListViewModel = new CardListListViewModel(Board.CardLists, Board.BoardId);

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

        public void OnDeleteBoardClick()
        {
            if (ActiveUser.IsActive == true && ShortBoard != null)
            {
                Action<MessageBoxResult> yesno = async (MessageBoxResult res) =>
                {
                    if (res == MessageBoxResult.Yes)
                    {
                        bool result = await BoardService.DeleteBoard(ActiveUser.Instance.LoggedUser.Token, Board.BoardId); 

                        if (result)
                        {
                            BoardDeleted?.Invoke(ShortBoard.BoardId);
                        }
                        else
                        {
                            ShowMessageBox(null, "Error deleting board.");
                        }

                    }
                };

                ShowMessageBox(yesno, "Are you sure you want to delete this board?", "Warning", MessageBoxButton.YesNo);
            }
            else
            {
                ShowMessageBox(null, "Error getting user.");
            }
        }

        public void OnRenameBoardClick()
        {
            if (ActiveUser.IsActive == true && ShortBoard != null)
            {                
                ShowMessageBox(null, "Kao se zove bord servis...");
            }
            else
            {
                ShowMessageBox(null, "Error getting user.");
            }
        }

        public bool CanRenameBoard()
        {
            return !String.IsNullOrWhiteSpace(Name);
        }

    }
}
