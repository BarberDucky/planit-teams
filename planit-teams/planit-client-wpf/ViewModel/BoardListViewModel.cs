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
                BoardSectedAction?.Invoke(SelectedBoard.BoardId);
            }
        }

        #endregion

        #region Commands
        
        public CommandBase NewBoardCommand { get; private set; }
        //public CommandBase DeleteBoardCommand { get; private set; }
        #endregion

        #region Actions and Func

        private Action<int> BoardSectedAction { get; set; }

        #endregion

        public BoardListViewModel(Action<int> boardSelectedAction)
        {
            Boards = new ObservableCollection<ShortBoard>();
            NewBoardCommand = new CommandBase(OnNewBoardClick);
            //DeleteBoardCommand = new CommandBase(OnDeleteBoardClick, CanDelete);
            BoardSectedAction = boardSelectedAction;

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
                    ShowMessageBox(null, "Dodavanje radi, samo se to ne vidi za sad, izadji i udji opet. Cekamo rabbita.");
                }
            }
        }

        //private void OnDeleteBoardClick()
        //{
        //    if (ActiveUser.IsActive == true)
        //    {
        //        ShowMessageBox(null, "Ovde bi isao delete kad bi postojao...");
        //        //if (dto == null)
        //        //{
        //        //    ShowMessageBox(null, "Delete unsuccessful.");
        //        //}
        //        //else
        //        //{
        //        //    ShowMessageBox(null, "Dodavanje radi, samo se to ne vidi za sad, izadji i udji opet. Cekamo rabbita.");
        //        //}
        //    }
        //}

        //private bool CanDelete()
        //{
        //    return true;
        //}
    }
}
