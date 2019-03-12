using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_data.Entities;

namespace planit_data.DTOs
{
    public class BasicCommentDTO
    {
        public int CommentId { get; set; }
        public int CardId { get; set; }
        public int CardListId { get; set; }
        public int BoardId { get; set; }
        public string Username { get; set; }
        public String Text { get; set; }
        public DateTime TimeStamp { get; set; }

        public BasicCommentDTO(Comment comment)
        {
            CommentId = comment.CommentId;
            Text = comment.Text;
            TimeStamp = comment.TimeStamp;
            CardId = comment.Card.CardId;
            Username = comment.User.IdentificationUser.UserName;
            CardListId = comment.Card.List.ListId;
            BoardId = comment.Card.List.Board.BoardId;
        }

    }

    public class CreateCommentDTO
    {
        public String Text { get; set; }
        public int CardId { get; set; }
       // public string UserId { get; set; }

        public static Comment FromDTO(CreateCommentDTO dto)
        {
            Comment com = new Comment()
            {
                Text = dto.Text,
            };
            return com;
        }

    }

    public class ReadCommentDTO
    {
        public int CommentId { get; set; }
        public String Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public int CardId { get; set; }
        public int ListId { get; set; }
        public String Username { get; set; }

        public ReadCommentDTO(Comment comment)
        {
            CommentId = comment.CommentId;
            Text = comment.Text;
            TimeStamp = comment.TimeStamp;
            ListId = comment.Card.List.ListId;
            if (comment.Card != null)
            {
                CardId = comment.Card.CardId;
            }
            if (comment.User != null)
            {
                Username = comment.User.IdentificationUser.UserName;
            }

        }

        public static List<ReadCommentDTO> FromEntityList(List<Comment> comments)
        {
            List<ReadCommentDTO> newList = new List<ReadCommentDTO>();

            foreach (Comment c in comments)
            {
                if (c != null)
                {
                    newList.Add(new ReadCommentDTO(c));
                }

            }

            return newList;
        }
    }

    //Nema update komentara
    public class UpdateCommentDTO
    {
        public int CommentId { get; set; }
        public String Text { get; set; }

        public static Comment FromDTO(UpdateCommentDTO dto)
        {
            Comment com = new Comment()
            {
                CommentId = dto.CommentId,
                Text = dto.Text
            };
            return com;
        }
    }
}
