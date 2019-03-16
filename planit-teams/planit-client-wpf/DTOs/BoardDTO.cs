using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.DTOs
{
    public class ShortBoardDTO
    {
        public int BoardId { get; set; }
        public String Name { get; set; }
        public String ExchangeName { get; set; }
        public bool IsRead { get; set; }
    }

    public class BasicBoardDTO
    {
        public int BoardId { get; set; }
        public String Name { get; set; }
        public String ExchangeName { get; set; }
    }

    public class CreateBoardDTO
    {
        public String Name { get; set; }
        // public String CreatedByUser { get; set; }

        public CreateBoardDTO(string name)
        {
            Name = name;
        }
    }

    public class UpdateBoardDTO
    {
        public String Name { get; set; }
        //  public int BoardId { get; set; }
    }

    public class ReadBoardDTO
    {
        public int BoardId { get; set; }
        public String Name { get; set; }
        public String ExchangeName { get; set; }
        public bool IsAdmin { get; set; }
        public List<ReadUserDTO> Users { get; set; }
        public List<ReadCardListDTO> CardList { get; set; }
    }

}
