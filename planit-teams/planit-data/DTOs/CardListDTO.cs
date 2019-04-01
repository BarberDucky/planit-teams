using planit_data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.DTOs
{
    public class BasicCardListDTO
    {
        public int ListId { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }
        public String BoardName { get; set; }
        public int BoardId { get; set; }

        public BasicCardListDTO(CardList list)
        {
            this.ListId = list.ListId;
            this.Name = list.Name;
            this.Color = list.Color;
            this.BoardName = list.Board.Name;
            this.BoardId = list.Board.BoardId;
        }
    }

    public class CreateCardListDTO
    {
        public String Name { get; set; }
        public String Color { get; set; }
        public int BoardId { get; set; }
        // public string Username { get; set; }

        public CardList FromDTO()
        {
            return new CardList()
            {
                Name = this.Name,
                Color = this.Color
            };
        }
    }

    public class ReadCardListDTO
    {
        public int ListId { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }
        public int BoardId { get; set; }
        public List<ReadCardDTO> Cards { get; set; }

        private void Load(CardList list)
        {
            this.ListId = list.ListId;
            this.Name = list.Name;
            this.Color = list.Color;
            this.BoardId = list.Board.BoardId;
        }

        public ReadCardListDTO(CardList list)
        {
            Load(list);
            this.Cards = ReadCardDTO.FromEntityList(list.Cards.ToList());
        }

        public ReadCardListDTO(CardList list, string username)
            //: this(list)
        {
            Load(list);
            this.Cards = ReadCardDTO.FromEntityList(list.Cards.ToList(), username);
        }

        public static List<ReadCardListDTO> FromEntityList(List<CardList> list)
        {
            List<ReadCardListDTO> newList = new List<ReadCardListDTO>();

            foreach (CardList c in list)
            {
                if (c != null)
                {
                    newList.Add(new ReadCardListDTO(c));
                }

            }

            return newList;
        }

        public static List<ReadCardListDTO> FromEntityList(List<CardList> list, string username)
        {
            List<ReadCardListDTO> newList = new List<ReadCardListDTO>();

            foreach (CardList c in list)
            {
                if (c != null)
                {
                    newList.Add(new ReadCardListDTO(c, username));
                }

            }

            return newList;
        }
    }

    public class UpdateCardListDTO
    {
        //public int ListId { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }

    }
}
