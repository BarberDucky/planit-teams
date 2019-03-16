using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Model;
using planit_client_wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class BoardViewModel : ViewModelBase
    {
        ////public LongBoard Board { get; set; }

        //public BoardViewModel(int id)
        //{
        //    Board = new LongBoard() { BoardId = id };
        //    //LoadBoard();
        //}

        //public async Task LoadBoard()
        //{
        //    if(ActiveUser.IsActive == true)
        //    {
        //        ReadBoardDTO dto = await BoardService.GetBoard(ActiveUser.Instance.LoggedUser.Token, Board.BoardId);
        //        if (dto != null)
        //        {
        //            //Setuj sve ostalo sto nije ime
        //        }
        //        else
        //        {
        //            ShowMessageBox(null, "Error getting board.");
        //        }
        //    }
        //    else
        //    {
        //        ShowMessageBox(null, "Error getting user.");
        //    }
        //}
    }
}
