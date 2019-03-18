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
        public ReadCardList readCardList;

        #region Properties

        public ReadCardList ReadCardList
        {
            get { return readCardList; }
            set { SetProperty(ref readCardList, value); }
        }

        #endregion

        public CardListViewModel(ReadCardList list)
        {
            this.ReadCardList = list;
        }

    }
}
