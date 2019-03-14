using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.DTOs
{
    public class BasicCardListDTO
    {
        public int ListId { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }
        public String BoardName { get; set; }
        public int BoardId { get; set; }
    }

    public class CreateCardListDTO
    {
        public String Name { get; set; }
        public String Color { get; set; }
        public int BoardId { get; set; }
        // public string Username { get; set; }
    }

    public class ReadCardListDTO
    {
        public int ListId { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }
        public String BoardName { get; set; }
        public List<ReadCardDTO> Cards { get; set; }
    }

    public class UpdateCardListDTO
    {
        //public int ListId { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }
    }
}
