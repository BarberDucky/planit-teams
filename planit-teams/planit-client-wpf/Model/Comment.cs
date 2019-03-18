using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Model
{
    public class ReadComment : BindableBase
    {
        private int commentId;
        private string text;
        private  DateTime timeStamp;
        //private int cardId;
        //private int listId;
        private string username;

        public int CommentId
        {
            get { return commentId; }
            set { SetProperty(ref commentId, value); }
        }

        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { SetProperty(ref timeStamp, value); }
        }

        //public int CardId
        //{
        //    get { return cardId; }
        //    set { SetProperty(ref cardId, value); }
        //}

        //public int ListId
        //{
        //    get { return listId; }
        //    set { SetProperty(ref listId, value); }
        //}

        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        public ReadComment(ReadCommentDTO dto)
        {
            if(dto != null)
            {
                CommentId = dto.CommentId;
                Text = dto.Text;
                TimeStamp = dto.TimeStamp;
                //CardId = dto.CardId;
                //ListId = dto.ListId;
                Username = dto.Username;
            }
        }

    }
}
