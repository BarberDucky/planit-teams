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
    public class BoardViewModel : ViewModelBase, IPanelContainer, IPanelOwner
    {
        private ShortBoard shortBoard;
        private ReadBoard board;
        private ViewModelBase usersViewModel;
        private ViewModelBase cardListViewModel;
        private ViewModelBase rightViewModel;
        private IPanelOwner Owner { get; set; }
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

        public ViewModelBase RightViewModel
        {
            get { return rightViewModel; }
            set { SetProperty(ref rightViewModel, value); }
        }

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

        #region Commands

        public CommandBase DeleteBoardCommand { get; private set; }

        public CommandBase RenameBoardCommand { get; private set; }

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
            RightViewModel = new EmptyViewModel();

            //Actions
            BoardDeleted = boardDeleted;

            //Commands
            DeleteBoardCommand = new CommandBase(OnDeleteBoardClick);
            DeleteBoardCommandVisible = false;
            RenameBoardCommand = new CommandBase(OnRenameBoardClick);

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
                    CardListViewModel = new CardListListViewModel(Board.CardLists, Board.BoardId, this);

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
                InstantiatePanel(new EditBoardViewModel(OnSubmit, new EditBoard(board)));                
            }
            else
            {
                ShowMessageBox(null, "Error getting user.");
            }
        }

        public async void OnSubmit(IEditable model)
        {
            if (model != null)
            {
                EditBoard editBoard = model as EditBoard;

                if (editBoard != null)
                {
                    if (ActiveUser.IsActive == true)
                    {
                        
                        bool succ = await BoardService.UpdateBoard(ActiveUser.Instance.LoggedUser.Token,
                            editBoard.BoardId, new UpdateBoardDTO(editBoard));
                        if (succ == true)
                        {
                            Name = editBoard.Name;
                            DestroyPanel();
                        }
                        else
                        {
                            ShowMessageBox(null, "Error renaming board.");
                        }
                    }
                    else
                    {
                        ShowMessageBox(null, "Error getting user.");
                    }

                }
            }
        }

        //public bool CanRenameBoard()
        //{
        //    return !String.IsNullOrWhiteSpace(Name);
        //}

        #region Subscribe for Notifications

        private void InitActions()
        {
            updateBoardAction = new Action<object>(UpdateBoardAction);
        }

        private void Subscribe()
        {
            MessageBroker.Instance.Subscribe(updateBoardAction, MessageEnum.BoardUpdate);
        }

        private void UpdateBoardAction(object obj)
        {
            BasicBoardDTO newBoard = (BasicBoardDTO)obj;

            if (newBoard != null)
            {
                Name = newBoard.Name;
            }
        }

        #endregion

        #region IPanelContainer

        public void InstantiatePanel(ViewModelBase sidePanel, IPanelOwner newOwner)
        {
            if (sidePanel != null)
            {
                var oldOwner = Owner;
                oldOwner?.NotifyPanelClosed();
                Owner = newOwner;
                RightViewModel = sidePanel;
            }
        }

        #endregion

        #region IPanelOwner

        public void InstantiatePanel(ViewModelBase panel)
        {
            InstantiatePanel(new EditBoardViewModel(OnSubmit, new EditBoard(board)), this);
        }

        public void DestroyPanel()
        {
            RightViewModel = new EmptyViewModel();
        }

        public void DestroyPanelObject()
        {
            RightViewModel = new EmptyViewModel();
        }

        public void NotifyPanelClosed()
        {
            return;
        }

        #endregion
    }
}
