﻿using planit_data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.DTOs
{
    public class CreateCardListDTO
    {
        public String Name { get; set; }
        public String Color { get; set; }
        public int BoardId { get; set; }
        public int UserId { get; set; }

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
        public String BoardName { get; set; }
        public List<ReadCardDTO> Cards { get; set; }

        public ReadCardListDTO(CardList list)
        {
            this.ListId = list.ListId;
            this.Name = list.Name;
            this.Color = list.Color;
            this.BoardName = list.Board.Name;
            this.Cards = ReadCardDTO.FromEntityList(list.Cards.ToList());
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
    }

    public class UpdateCardListDTO
    {
        public int ListId { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }

    }
}
