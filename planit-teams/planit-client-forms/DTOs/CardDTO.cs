using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_forms.DTOs
{
    public class CreateCardDTO
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime DueDate { get; set; }
        public int ListId { get; set; }
        public int UserId { get; set; }
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

        public override string ToString()
        {
            return $"\nCard: {CardId} \nName: {Name} \nListname: {ListName}\nBoardName: {BoardName}\n";
        }
    }

    public class UpdateCardDTO
    {
        public int CardId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
