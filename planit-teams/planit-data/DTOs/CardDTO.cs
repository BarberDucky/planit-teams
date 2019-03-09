using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_data.Entities;

namespace planit_data.DTOs
{
    public class BasicCardDTO
    {
        public int CardId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime DueDate { get; set; }
        public int ListId { get; set; }
        public String ListName { get; set; }
        public int BoardId { get; set; }
        public String BoardName { get; set; }

        public BasicCardDTO(Card card)
        {
            CardId = card.CardId;
            Name = card.Name;
            Description = card.Description;
            TimeStamp = card.CreationDate;
            DueDate = card.DueDate;
            ListName = card.List.Name;
            BoardName = card.List.Board.Name;
            ListId = card.List.ListId;
            BoardId = card.List.Board.BoardId;
        }
    }
    public class CreateCardDTO
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime DueDate { get; set; }
        public int ListId { get; set; }
        public int UserId { get; set; }

        public static Card FromDTO(CreateCardDTO dto)
        {
            Card c = new Card()
            {
                Name = dto.Name,
                Description = dto.Description,
                DueDate = dto.DueDate
            };
            return c;
        }
    }

    public class ReadCardDTO
    {
        public int CardId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime DueDate { get; set; }
        public String ListName { get; set; }
        public String BoardName { get; set; }
        public List<ReadCommentDTO> Comments { get; set; }
        public ReadCardDTO(Card card)
        {
            Comments = new List<ReadCommentDTO>();
            CardId = card.CardId;
            Name = card.Name;
            Description = card.Description;
            TimeStamp = card.CreationDate;
            DueDate = card.DueDate;
            ListName = card.List.Name;
            BoardName = card.List.Board.Name;
            this.Comments = ReadCommentDTO.FromEntityList(card.Comments.ToList());
        }

        public static List<ReadCardDTO> FromEntityList(List<Card> list)
        {
            List<ReadCardDTO> dtoList = new List<ReadCardDTO>();
            foreach (var c in list)
            {
                if (c != null)
                {
                    dtoList.Add(new ReadCardDTO(c));
                }
            }

            return dtoList;
        }
    }

    public class UpdateCardDTO
    {
        public int CardId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime DueDate { get; set; }
        public int UpdatedByUser { get; set; }

        public static Card FromDTO(UpdateCardDTO dto)
        {
            Card c = new Card()
            {
                CardId = dto.CardId,
                Name = dto.Name,
                Description = dto.Description,
                DueDate = dto.DueDate
            };
            return c;
        }
    }
}
