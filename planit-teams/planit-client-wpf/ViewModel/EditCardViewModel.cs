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
        private EditCard card;
        public EditCard Card
        {
            get { return card; }
            set
            {
                SetProperty(ref card, value);
                //RaiseValidateInstanceChanged();
            }
        }

        public EditCardViewModel(Action<IEditable> onExecute, EditCard card)
            : base(onExecute)
        {
            this.card = card;
        }

        public override bool ValidateInstance()
        {
            return card!= null && !String.IsNullOrWhiteSpace(card.Name) &&
                !String.IsNullOrWhiteSpace(card.Description);
        }

        public override IEditable GetInstance()
        {
            return card;
        }

        //public void Dispose()
        //{
            
        //}
    }
}
