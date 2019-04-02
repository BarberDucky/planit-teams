using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Model;
using planit_client_wpf.MQ;
using planit_client_wpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public bool deleteBoardCommandVisible;

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
            get { return ShortBoard.Name; }
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

        //TODO - da li je rename board admin komanda?
        public CommandBase RenameBoardCommand { get; private set; }

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

        #region Message Action

        private Action<object> updateBoardAction;

        #endregion

        public BoardViewModel(ShortBoard shortBoard, Action<int> boardDeleted)
        {
            //Partial data
            ShortBoard = shortBoard;

            //View models
            UsersViewModel = new EmptyViewModel();
            CardListViewModel = new EmptyViewModel();

            //Commands
            DeleteBoardCommand = new CommandBase(OnDeleteBoardClick);
            DeleteBoardCommandVisible = false;
            BoardDeleted = boardDeleted;
            RenameBoardCommand = new CommandBase(OnRenameBoardClick, CanRenameBoard);

            //Load data
            LoadBoard();
            InitActions();
            Subscribe();
        }

        public void UnsubscribeFromBoard()
        {
            if (ShortBoard != null)
            {
                MQService.Instance.Unsubscribe(ShortBoard.ExchangeName);
                MessageBroker.Instance.UnsubscribeStartingFrom(MessageEnum.CardListCreate);
            }
        }

        public async void LoadBoard()
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

                    UsersViewModel = new UsersListViewModel(Board.Users, Board.IsAdmin, Board.BoardId);
                    CardListViewModel = new CardListListViewModel(Board.CardLists, Board.BoardId);

                    //Subscribe to board
                    MQService.Instance.SubscribeToExchange(ShortBoard.ExchangeName);

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

        public bool CanDeleteBoard()
        {
            return Board != null && Board.IsAdmin == true;
        }

        public void OnRenameBoardClick()
        {
            if (ActiveUser.IsActive == true && ShortBoard != null)
            {
                Name = "Nesto";
                ShowMessageBox(null, "Pravimo se da se otvara dijalog u kome moze da se renamuje board.");
                
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

        #region Subscribe for Notifications

        private void InitActions()
        {
            updateBoardAction = new Action<object>(UpdateBoardAction);
        }

        private void Subscribe()
        {
            MessageBroker.Instance.Subscribe(updateBoardAction, MessageEnum.BoardUpdate);
        }

        //TODO - proveriti da li radi sa interface-om
        private void UpdateBoardAction(object obj)
        {
            BasicBoardDTO newBoard = (BasicBoardDTO)obj;

            if (obj != null)
            {
                Name = newBoard.Name;
            }
        }

        #endregion

    }
}
