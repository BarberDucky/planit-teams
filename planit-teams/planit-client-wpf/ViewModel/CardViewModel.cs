using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Model;
using planit_client_wpf.MQ;
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
        //private EditCardViewModel editForm;
        //private IPanelContainer container;
        //EditCardViewModel openContainer;

        #region Properties

        public ReadCard Card
        {
            get { return card; }
            set
            {
                SetProperty(ref card, value);
                //if(card == null)
                //{
                //    //if (openContainer != null)
                //    //{
                //    //    container.InstantiatePanel(new EmptyViewModel(), null);
                //    //}
                //    //InstantiatePanel(new EditCardViewModel())
                //}
            }
        }

        public ViewModelBase Comments
        {
            get { return comments; }
            set { SetProperty(ref comments, value); }
        }

        //public EditCardViewModel EditFormViewModel
        //{
        //    get { return editForm; }
        //    set { SetProperty(ref editForm, value); }
        //}

        #endregion

        #region Commands

        public CommandBase<ReadCard> EditCardCommand { get; protected set; }

        #endregion

        #region Actions and Func
        private Action<EditCard> OnEditButtonClickAction { get; set; }

        #endregion

        public CardViewModel(ReadCard card, Action<EditCard> onEditButtonClick)
        {
            if (card != null)
            {
                this.card = card;
                comments = new CommentsViewModel(card.Comments, card.CardId);
                EditCardCommand = new CommandBase<ReadCard>(OnEditButtonClick);
                OnEditButtonClickAction = onEditButtonClick;
                //EditFormViewModel = null;
                //this.container = container;
                //openContainer = null;
            }
        }

        public void OnEditButtonClick(ReadCard card)
        {
            //var panel = new EditCardViewModel(OnEditCardCompleted, new EditCard(card));
            //InstantiatePanel(panel);
            //container.InstantiatePanel(new EditCardViewModel(OnEditCardCompleted, new EditCard(card)), null);
            OnEditButtonClickAction?.Invoke(new EditCard(card));
        }

        //public void OnEditCardCompleted(IEditable model)
        //{
        //    if(model != null)
        //    {
        //        EditCard editCard = model as EditCard;
        //        if (editCard != null)
        //        {
        //            Card.UpdateEditedFields(editCard);
        //            //container.InstantiatePanel(new EmptyViewModel(), null);
        //            DestroyPanel();
        //        }
        //    }
        //}

        //public void NotifyContainerClosed()
        //{
        //    openContainer = null;
        //}

        //public void Dispose()
        //{
        //    if(openContainer != null)
        //    {
        //        container.InstantiatePanel(new EmptyViewModel(), null);
        //    }
        //}

        //public void Dispose()
        //{
        //    DisposePanelOwner();
        //}
    }
}
