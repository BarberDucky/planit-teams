using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public String Text { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Card Card { get; set; }
        public virtual User User { get; set; }
    }
}
