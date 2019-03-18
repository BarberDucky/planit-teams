using planit_client_wpf.Base;
using planit_client_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class CardListViewModel : ViewModelBase
    {
        public ReadCardList cardList;

        #region Properties

        public ReadCardList CardList
        {
            get { return cardList; }
            set { SetProperty(ref cardList, value); }
        }

        #endregion

        public CardListViewModel(ReadCardList list)
        {
            this.CardList = list;
        }

    }
}
