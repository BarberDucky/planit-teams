using planit_client_wpf.Base;
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
        private string boardName;

        public ObservableCollection<ReadComment> Comments { get; set; }

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

        public string BoardName
        {
            get { return boardName; }
            set { SetProperty(ref boardName, value); }
        }

        public ReadCard(ReadCardDTO dto)
        {
            CardId = dto.CardId;
            Name = dto.Name;
            Description = dto.Description;
            Timestamp = dto.TimeStamp;
            DueDate = dto.DueDate;
            BoardName = dto.BoardName;
            Comments = new ObservableCollection<ReadComment>();
            foreach(ReadCommentDTO com in dto.Comments)
            {
                Comments.Add(new ReadComment(com));
            }
        }

    }
}
