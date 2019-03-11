using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_data.Entities;

namespace planit_data.DTOs
{
    //TODO Dodaditi ovaj dto
    public class BasicCommentDTO
    {
        public int CommentId { get; set; }
        public int CardId { get; set; }
        public int CardListId { get; set; }
        public int BoardId { get; set; }
        public int UserId { get; set; }
        public String Text { get; set; }
        public DateTime TimeStamp { get; set; }

        public BasicCommentDTO(Comment comment)
        {
            CommentId = comment.CommentId;
            Text = comment.Text;
            TimeStamp = comment.TimeStamp;
            CardId = comment.Card.CardId;
            UserId = comment.User.UserId;
            CardListId = comment.Card.List.ListId;
            BoardId = comment.Card.List.Board.BoardId;
        }

    }

    public class CreateCommentDTO
    {
        public String Text { get; set; }
        public int CardId { get; set; }
        public int UserId { get; set; }

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
        public int UserId { get; set; }

        public ReadCommentDTO(Comment comment)
        {
            CommentId = comment.CommentId;
            Text = comment.Text;
            TimeStamp = comment.TimeStamp;
            if (comment.Card != null)
            {
                CardId = comment.Card.CardId;
            }
            if (comment.User != null)
            {
                UserId = comment.User.UserId;
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
