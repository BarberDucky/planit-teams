using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Model
{
    public class ShortBoard : BindableBase
    {
        private int boardId;
        private string name;
        private bool isRead;

        #region Properties

        public int BoardId
        {
            get { return boardId; }
            set { boardId = value; }
        }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public bool IsRead
        {
            get { return isRead; }
            set { SetProperty(ref isRead, value); }
        }

        #endregion

        public ShortBoard(ShortBoardDTO dto)
        {
            Name = dto.Name;
            BoardId = dto.BoardId;
            IsRead = dto.IsRead;
        }
    }

    public class LongBoard : BindableBase
    {
        private int boardId;
        private string name;
        private bool isRead;

        #region Properties

        public int BoardId
        {
            get { return boardId; }
            set { boardId = value; }
        }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public bool IsAdmin
        {
            get { return isRead; }
            set { SetProperty(ref isRead, value); }
        }

        //TODO Users, CardList

        #endregion

        public LongBoard()
        {
            
        }
    }
}
