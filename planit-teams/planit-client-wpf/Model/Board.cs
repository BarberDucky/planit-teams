﻿using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ShortBoard(BasicBoardDTO dto)
        {
            Name = dto.Name;
            BoardId = dto.BoardId;
            IsRead = false;
        }
    }

    public class ReadBoard : BindableBase
    {
        private int boardId;
        private string name;
        private bool isRead;
        private ObservableCollection<ReadCardList> cardLists;
        private ObservableCollection<ReadUser> users;

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

        public ObservableCollection<ReadUser> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }

        public ObservableCollection<ReadCardList> CardLists
        {
            get { return cardLists; }
            set { SetProperty(ref cardLists, value); }
        }

        #endregion

        public ReadBoard()
        {

        }

        public ReadBoard(ReadBoardDTO dto)
        {
            BoardId = dto.BoardId;
            Name = dto.Name;
            IsAdmin = dto.IsAdmin;

            CardLists = new ObservableCollection<ReadCardList>();
            Users = new ObservableCollection<ReadUser>();
            foreach(ReadCardListDTO list in dto.CardList)
            {
                CardLists.Add(new ReadCardList(list));
            }
            foreach(ReadUserDTO users in dto.Users)
            {
                Users.Add(new ReadUser(users));
            }
        }

    }
}
