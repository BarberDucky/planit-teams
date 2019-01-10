using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_forms.DTOs
{
    public class CreateBoardDTO
    {
        public String Name { get; set; }
        public int CreatedByUser { get; set; }
    }

    public class UpdateBoardDTO
    {
        public String Name { get; set; }
        public int BoardId { get; set; }
    }

    public class ReadBoardDTO
    {
        public int BoardId { get; set; }
        public String Name { get; set; }
        public String ExchangeName { get; set; }
        public List<ReadCardListDTO> CardList { get; set; }

        public override string ToString()
        {
            return $"\nBoard: {BoardId} \nName: {Name}\n";
        }
    }
}
