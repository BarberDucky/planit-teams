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
        [Required]
        public String Text { get; set; }
        [Required]
        public DateTime TimeStamp { get; protected set; }
        [Required]
        public virtual Card Card { get; set; }
        [Required]
        public virtual User User { get; set; }

        public Comment()
        {
            TimeStamp = DateTime.Now;
        }

    }
}
