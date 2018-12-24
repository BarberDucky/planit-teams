using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_data.Entities;

namespace planit_data.DTOs
{
    public class CreateCommentDTO
    {
        public String Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public int CardId { get; set; }
        public int UserId { get; set; }
    }

    public class ReadCommentDTO
    {
        public int CommentId { get; set; }
        public DateTime TimeStamp { get; set; }
        public Card Card { get; set; }
        public User User { get; set; }

        public ReadCommentDTO (Comment comment)
        {
            CommentId = comment.CommentId;
            TimeStamp = comment.TimeStamp;
            Card = comment.Card;
            User = comment.User;
        }
    }

    public class UpdateCommentDTO
    {
        public int CommentId { get; set; }
        public String Text { get; set; }
    }

    public class DeleteCommentDTO
    {
        public int CommentId { get; set; }
    }
}
