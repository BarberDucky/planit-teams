using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_forms.DTOs
{
    public class CreateCardListDTO
    {
        public String Name { get; set; }
        public String Color { get; set; }
        public int BoardId { get; set; }
    }

    public class ReadCardListDTO
    {
        public int ListId { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }
        public String BoardName { get; set; }
        public List<ReadCardDTO> Cards { get; set; }

        public override string ToString()
        {
            return $"\nList: {ListId} \nName: {Name} \nColor: {Color}\nBoard Name: {BoardName}\n";
        }
    }

    public class UpdateCardListDTO
    {
        public int ListId { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }
    }
}
