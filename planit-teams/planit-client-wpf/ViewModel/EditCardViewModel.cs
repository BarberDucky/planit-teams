using planit_client_wpf.Base;
using planit_client_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class EditCardViewModel : EditViewModelBase
    {
        private ReadCard card;
        public ReadCard Card
        {
            get { return card; }
            set { SetProperty(ref card, value); }
        }

        public EditCardViewModel(Func<BindableBase, bool> onExecute, ReadCard card)
            : base(onExecute)
        {
            this.card = card;
        }

        public override bool ValidateInstance()
        {
            return card!= null && !String.IsNullOrWhiteSpace(card.Name) &&
                !String.IsNullOrWhiteSpace(card.Description);
        }

        public override BindableBase GetInstance()
        {
            return card;
        }
    }
}
