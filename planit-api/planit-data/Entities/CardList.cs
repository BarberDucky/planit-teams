using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Entities
{
    public class CardList
    {
        [Key]
        public int ListId { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }

        public virtual Board Board { get; set; }
        
        public virtual ICollection<Card> Cards { get; set; }

        public CardList()
        {
            Cards = new HashSet<Card>();
        }
    }
}
