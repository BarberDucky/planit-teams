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
    public class ReadCard : BindableBase
    {
        private int cardId;
        private string name;
        private string description;
        private DateTime timestamp;
        private DateTime dueDate;
        private ObservableCollection<ReadComment> comments;
        private int listId;
        private int boardId;
        private bool isWatched;

        public int CardId
        {
            get { return cardId; }
            set { SetProperty(ref cardId, value); }
        }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        public DateTime Timestamp
        {
            get { return timestamp; }
            set { SetProperty(ref timestamp, value); }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set { SetProperty(ref dueDate, value); }
        }

        public ObservableCollection<ReadComment> Comments
        {
            get { return comments; }
            set { SetProperty(ref comments, value); }
        }

        public int BoardId
        {
            get { return boardId; }
            set { SetProperty(ref boardId, value); }
        }

        public int ListId
        {
            get { return listId; }
            set { SetProperty(ref listId, value); }
        }

        //public string BoardName
        //{
        //    get { return boardName; }
        //    set { SetProperty(ref boardName, value); }
        //}

        public bool IsWatched
        {
            get { return isWatched; }
            set { SetProperty(ref isWatched, value); }
        }

        public ReadCard(BasicCardDTO dto)
        {
            if (dto != null)
            {
                CardId = dto.CardId;
                Name = dto.Name;
                Description = dto.Description;
                Timestamp = dto.TimeStamp;
                DueDate = dto.DueDate;
                BoardId = dto.BoardId;
                ListId = dto.ListId;
                IsWatched = false;
                Comments = new ObservableCollection<ReadComment>();
            }

        }

        public ReadCard(ReadCardDTO dto)
        {
            if (dto != null)
            {
                CardId = dto.CardId;
                Name = dto.Name;
                Description = dto.Description;
                Timestamp = dto.TimeStamp;
                DueDate = dto.DueDate;
                BoardId = dto.BoardId;
                ListId = dto.ListId;
                IsWatched = dto.IsWatched;
                Comments = new ObservableCollection<ReadComment>();
                if (dto.Comments != null)
                {
                    foreach (ReadCommentDTO comDTO in dto.Comments)
                    {
                        Comments.Add(new ReadComment(comDTO));
                    }
                }
            }

        }

        public static void UpdateCard(ReadCard readCard, EditCard editCard)
        {
            if(readCard != null && editCard != null)
            {
                readCard.Name = editCard.Name;
                readCard.Description = editCard.Description;
                readCard.DueDate = editCard.DueDate;
            }
        }

        public static void UpdateCard(ReadCard card, BasicCardDTO dto)
        {
            card.Description = dto.Description;
            card.DueDate = dto.DueDate;
            card.Name = dto.Name;
        }

    }
   
    public class EditCard : BindableBase, IEditable
    {
        private int cardId;
        private string name;
        private string description;
        private DateTime timestamp;
        private DateTime dueDate;

        #region Properties 

        public int CardId
        {
            get { return cardId; }
            set { SetProperty(ref cardId, value); }
        }
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        public DateTime Timestamp
        {
            get { return timestamp; }
            set { SetProperty(ref timestamp, value); }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set { SetProperty(ref dueDate, value); }
        }

        #endregion

        public EditCard(ReadCard card)
        {
            if (card != null)
            {
                CardId = card.CardId;
                Name = String.Copy(card.Name);
                Description = String.Copy(card.Description);
                Timestamp = card.Timestamp;
                DueDate = card.DueDate;                
            }
        }

    }

    public class MoveCard
    {
        public int OldListId { get; set; }
        public int NewListId { get; set; }
        public int CardId { get; set; }
    }
}
