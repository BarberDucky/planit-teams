using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Entities
{
    public class Board
    {
        [Key]
        public int BoardId { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String ExchangeName { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<CardList> CardLists { get; set; }

        public Board()
        {
            Permissions = new HashSet<Permission>();
            CardLists = new HashSet<CardList>();
        }
    }
}
