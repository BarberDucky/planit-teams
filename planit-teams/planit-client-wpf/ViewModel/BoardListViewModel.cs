using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Model;
using planit_client_wpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                    selectedBoard.IsRead = true;
                    BoardNotificationService.ReadBoardNotification(ActiveUser.Instance.LoggedUser.Token, SelectedBoard.BoardId);
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

        public BoardListViewModel(Action<ShortBoard> boardSelectedAction, Action boardDeselectedAction)
        {
            Boards = new ObservableCollection<ShortBoard>();
            NewBoardCommand = new CommandBase(OnNewBoardClick);
            BoardSectedAction = boardSelectedAction;
            BoardDeselectedAction = boardDeselectedAction;

            LoadBoardsByUser();
        }

        private async Task LoadBoardsByUser()
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
            if(ActiveUser.IsActive == true)
            {
                ShortBoardDTO dto = await BoardService.CreateBoard(ActiveUser.Instance.LoggedUser.Token, new CreateBoardDTO("Unnamed Board"));
                if(dto == null)
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
            if(Boards != null)
            {
                var item = Boards.FirstOrDefault(x => x.BoardId == boardId);
                Boards.Remove(item);
            }
        }
    }
}
