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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace planit_client_wpf.ViewModel
{
    public class BoardListViewModel : ViewModelBase
    {
        private ShortBoard selectedBoard;

        #region Properties

        public ObservableCollection<ShortBoard> Boards { get; set; }

        public ShortBoard SelectedBoard
        {
            get { return selectedBoard; }
            set
            {
                SetProperty(ref selectedBoard, value);
                if (selectedBoard == null)
                    BoardDeselectedAction?.Invoke();
                else
                {
                    BoardSectedAction?.Invoke(selectedBoard);
                    ReadBoardNotification();
                }
            }
        }

        #endregion

        #region Commands

        public CommandBase NewBoardCommand { get; private set; }

        #endregion

        #region Actions and Func

        private Action<ShortBoard> BoardSectedAction { get; set; }
        private Action BoardDeselectedAction { get; set; }

        #endregion

        #region Message Actions

        Action<object> boardNotificationAction;
        Action<object> updateBoardAction;
        Action<object> addPermissionAction;
        Action<object> deletePermissionAction;

        #endregion

        public BoardListViewModel(Action<ShortBoard> boardSelectedAction, Action boardDeselectedAction)
        {
            Boards = new ObservableCollection<ShortBoard>();
            NewBoardCommand = new CommandBase(OnNewBoardClick);
            BoardSectedAction = boardSelectedAction;
            BoardDeselectedAction = boardDeselectedAction;

            LoadBoardsByUser();

            InitActions();
            Subscribe();
        }

        private async void LoadBoardsByUser()
        {
            if (ActiveUser.IsActive == true)
            {
                List<ShortBoardDTO> listaDTO = await BoardService.GetBoardsByUser(ActiveUser.Instance.LoggedUser.Token);
                if (listaDTO != null)
                {
                    List<ShortBoard> lista = new List<ShortBoard>();
                    foreach (ShortBoardDTO dto in listaDTO)
                    {
                        //Consider refactoring - observable mehanizam se zove svaki put kad dodam novi bord
                        Boards.Add(new ShortBoard(dto));
                    }
                }
                else
                {
                    ShowMessageBox(null, "Error getting boards.");
                }
            }
            else
            {
                ShowMessageBox(null, "Error getting active user.");
            }
        }

        private async void OnNewBoardClick()
        {
            if (ActiveUser.IsActive == true)
            {
                ShortBoardDTO dto = await BoardService.CreateBoard(ActiveUser.Instance.LoggedUser.Token, new CreateBoardDTO("Unnamed Board"));
                if (dto == null)
                {
                    ShowMessageBox(null, "Creation unsuccessful.");
                }
                else
                {
                    Boards.Add(new ShortBoard(dto));
                }
            }
        }

        public void RemoveSelectedBoard(int boardId)
        {
            SelectedBoard = null;
            if (Boards != null)
            {
                var item = Boards.FirstOrDefault(x => x.BoardId == boardId);
                Boards.Remove(item);
            }
        }

        private async void ReadBoardNotification()
        {
            bool succ = await BoardNotificationService.
                ReadBoardNotification(ActiveUser.Instance.LoggedUser.Token, SelectedBoard.BoardId);

            if (succ)
                selectedBoard.IsRead = true;
        }

        #region Subscribe for Notifications

        private void InitActions()
        {
            boardNotificationAction = new Action<object>(BoardNotificationAction);
            addPermissionAction = new Action<object>(AddPermissionUserAction);
            deletePermissionAction = new Action<object>(DeletePermissionUserAction);
            updateBoardAction = new Action<object>(UpdateBoardAction);
        }

        private void Subscribe()
        {
            MessageBroker.Instance.Subscribe(boardNotificationAction, MessageEnum.BoardBoardNotification);
            MessageBroker.Instance.Subscribe(addPermissionAction, MessageEnum.BoardCreate);
            MessageBroker.Instance.Subscribe(deletePermissionAction, MessageEnum.BoardDelete);
            MessageBroker.Instance.Subscribe(updateBoardAction, MessageEnum.BoardUserUpdate);
        }

        private void BoardNotificationAction(object obj)
        {
            int id = (int)obj;

            if (selectedBoard == null || id != selectedBoard.BoardId)
            {
                Boards.SingleOrDefault(el => el.BoardId == id).IsRead = false;
            }
            else if (selectedBoard != null && id == selectedBoard.BoardId)
            {
                ReadBoardNotification();
               // BoardNotificationService.ReadBoardNotification(ActiveUser.Instance.LoggedUser.Token, id);
            }
        }

        private void AddPermissionUserAction(object obj)
        {
            BasicBoardDTO board = (BasicBoardDTO)obj;

            if (board != null)
            {
                Boards.Add(new ShortBoard(board));
            }
            ShowMessageBox(null, "Stigla poruka");
        }

        private void DeletePermissionUserAction(object obj)
        {
            int id = (int)obj;

            ShortBoard deleteBoard = Boards.SingleOrDefault(el => el.BoardId == id);

            if (deleteBoard != null)
            {
                Boards.Remove(deleteBoard);

                if (selectedBoard != null && selectedBoard.BoardId == deleteBoard.BoardId)
                {
                    selectedBoard = null;
                }
            }
            
        }

        private void UpdateBoardAction(object obj)
        {
            BasicBoardDTO newBoard = (BasicBoardDTO)obj;

            if (obj != null)
            {
                ShortBoard old = Boards.FirstOrDefault(b => b.BoardId == newBoard.BoardId);

                if (old != null)
                {
                    ShortBoard.UpdateBoard(old, newBoard);
                }
            }
        }

        #endregion
    }
}
