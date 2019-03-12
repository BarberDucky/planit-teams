using planit_data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.DTOs
{
    public class ShortBoardDTO
    {
        public int BoardId { get; set; }
        public String Name { get; set; }
        public String ExchangeName { get; set; }
        public bool IsRead { get; set; }

        public ShortBoardDTO(Board b, bool isRead)
        {
            this.BoardId = b.BoardId;
            this.Name = b.Name;
            this.ExchangeName = b.ExchangeName;
            this.IsRead = isRead;
        }
    }

    public class BasicBoardDTO
    {
        public int BoardId { get; set; }
        public String Name { get; set; }
        public String ExchangeName { get; set; }

        public BasicBoardDTO(Board b)
        {
            this.BoardId = b.BoardId;
            this.Name = b.Name;
            this.ExchangeName = b.ExchangeName;
        }
    }

    public class CreateBoardDTO
    {
        public String Name { get; set; }
       // public String CreatedByUser { get; set; }

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

        public ReadBoardDTO(Board b)
        {
            CardList = new List<ReadCardListDTO>();
            Users = new List<ReadUserDTO>();
            this.BoardId = b.BoardId;
            this.Name = b.Name;
            this.ExchangeName = b.ExchangeName;
            CardList = ReadCardListDTO.FromEntityList(b.CardLists.ToList());
        }

        public ReadBoardDTO(Board b, bool isAdmin, List<User> users)
            : this(b)
        {
            IsAdmin = isAdmin;
            Users = ReadUserDTO.FromEntityList(users);
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
