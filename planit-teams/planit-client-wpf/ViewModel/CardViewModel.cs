using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Model;
using planit_client_wpf.MQ;
using planit_client_wpf.Services;
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

        #region Properties

        public ReadCard Card
        {
            get { return card; }
            set
            {
                SetProperty(ref card, value);
            }
        }

        public ViewModelBase Comments
        {
            get { return comments; }
            set { SetProperty(ref comments, value); }
        }

        #endregion

        #region Commands

        public CommandBase<ReadCard> EditCardCommand { get; protected set; }
        public CommandBase WatchCardCommand { get; protected set; }

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
                WatchCardCommand = new CommandBase(OnWatchCard);
            }
        }

        public void OnEditButtonClick(ReadCard card)
        {
            OnEditButtonClickAction?.Invoke(new EditCard(card));
        }

        public async void OnWatchCard()
        {
            bool result, newValue;
            if (Card.IsWatched)
            {
                result = await CardService.UnwatchCard(ActiveUser.Instance.LoggedUser.Token, Card.CardId);
                newValue = false;
            }
            else
            {
                result = await CardService.WatchCard(ActiveUser.Instance.LoggedUser.Token, Card.CardId);
                newValue = true;
            }
            if (result)
            {
                Card.IsWatched = newValue;
            }
        }

    }
}
