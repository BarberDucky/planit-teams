using planit_data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.DTOs
{
    public class CreateBoardDTO
    {
        public String Name { get; set; }
        public int CreatedByUser { get; set; }

        public Board FromDTO()
        {
            return new Board()
            {
                Name = Name
            };
        }
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
        public List<ReadCardListDTO> CardList { get; set; }

        public ReadBoardDTO(Board b)
        {
            CardList = new List<ReadCardListDTO>();
            this.BoardId = b.BoardId;
            this.Name = b.Name;
            foreach(var c in b.CardLists)
            {
                CardList.Add(new ReadCardListDTO(c));
            }
        }

        public static List<ReadBoardDTO> FromEntityList(List<Board> boards)
        {
            List<ReadBoardDTO> list = new List<ReadBoardDTO>();

            foreach (Board b in boards)
            {
                if (b != null)
                {
                    list.Add(new ReadBoardDTO(b));
                }

            }

            return list;
        }
    }
}
