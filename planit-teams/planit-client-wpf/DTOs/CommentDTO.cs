using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.DTOs
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
    }

    public class CreateCommentDTO
    {
        public String Text { get; set; }
        public int CardId { get; set; }
        // public string UserId { get; set; }
    }

    public class ReadCommentDTO
    {
        public int CommentId { get; set; }
        public String Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public int CardId { get; set; }
        public int ListId { get; set; }
        public String Username { get; set; }
    }
}
