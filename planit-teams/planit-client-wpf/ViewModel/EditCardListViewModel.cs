using planit_client_wpf.Base;
using planit_client_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class EditCardListViewModel : EditViewModelBase
    {
        EditCardList cardList;

        public EditCardList CardList
        {
            get { return cardList; }
            set { SetProperty(ref cardList, value); }
        }

        public EditCardListViewModel(Action<IEditable> onSubmit, EditCardList cardList)
            :base(onSubmit)
        {
            CardList = cardList;
        }

        public override IEditable GetInstance()
        {
            return cardList;
        }

        public override bool ValidateInstance()
        {
            return cardList != null && !String.IsNullOrWhiteSpace(cardList.Name);
        }
    }
}
