using planit_client_wpf.Base;
using planit_client_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class CardViewModel : ViewModelBase
    {
        public ReadCard card;
        public ViewModelBase comments;
        private EditCardViewModel editForm;

        #region Properties

        public ReadCard Card
        {
            get { return card; }
            set { SetProperty(ref card, value); }
        }

        public ViewModelBase Comments
        {
            get { return comments; }
            set { SetProperty(ref comments, value); }
        }

        public EditCardViewModel EditFormViewModel
        {
            get { return editForm; }
            set { SetProperty(ref editForm, value); }
        }

        #endregion

        #region Commands

        public CommandBase EditCardCommand { get; protected set; }

        #endregion

        public CardViewModel(ReadCard card)
        {
            if(card != null)
            {
                this.card = card;
                comments = new CommentsViewModel(card.Comments, card.CardId);
                EditCardCommand = new CommandBase(OnEditCard);
                EditFormViewModel = null;
            }
        }

        public void OnEditCard()
        {
            ReadCard c = new ReadCard(new DTOs.BasicCardDTO() { Name = card.Name, Description = card.Description });
            EditFormViewModel = new EditCardViewModel(OnEditCardExecute, c);
            EditFormViewModel.IsOpen = true;
        }

        public bool OnEditCardExecute(BindableBase model)
        {
            ShowMessageBox(null, "radiii");
            return true;
        }

    }
}
