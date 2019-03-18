using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Model
{
    public class ReadCardList : BindableBase
    {
        private int listId;
        private string name;
        private string color;
        private string boardName;
        private ObservableCollection<ReadCard> cards;

        #region Properties 

        public int ListId
        {
            get { return listId; }
            set { SetProperty(ref listId, value); }
        }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string Color
        {
            get { return color; }
            set { SetProperty(ref color, value); }
        }

        public string BoardName
        {
            get { return boardName; }
            set { SetProperty(ref boardName, value); }
        }

        public ObservableCollection<ReadCard> Cards
        {
            get { return cards; }
            set { SetProperty(ref cards, value); }
        }

        #endregion

        public ReadCardList()
        {

        }

        public ReadCardList(ReadCardListDTO dto)
        {
            ListId = dto.ListId;
            Name = dto.Name;
            Color = dto.Color;
            BoardName = dto.BoardName;

            Cards = new ObservableCollection<ReadCard>();
            foreach(ReadCardDTO card in dto.Cards)
            {
                Cards.Add(new ReadCard(card));
            }
        }
    }
}
