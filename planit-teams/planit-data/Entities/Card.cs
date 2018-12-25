using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Entities
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }
        [Required]
        public String Name { get; set; }
        public String Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        [Required]
        public virtual CardList List { get; set; }
        [Required]
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<User> ObserverUsers { get; set; }

        public Card()
        {
            Comments = new HashSet<Comment>();
            ObserverUsers = new HashSet<User>();
        }
    }
}
