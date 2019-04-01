using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Model;
using planit_client_wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class CardListViewModel : ViewModelBase
    {
        private ReadCardList cardList;
        private ReadCard selectedCard;

        #region Properties

        public ReadCardList CardList
        {
            get { return cardList; }
            set { SetProperty(ref cardList, value); }
        }

        public ReadCard SelectedCard
        {
            get { return selectedCard; }
            set
            {
                SetProperty(ref selectedCard, value);
                SelectedCardAction?.Invoke(selectedCard);
            }
        }

        #endregion

        #region Commands

        public CommandBase AddCardCommand { get; protected set; }
        public CommandBase<ReadCardList> DeleteCardListCommand { get; protected set; }
        public CommandBase<ReadCardList> RenameCardListCommand { get; protected set; }
        public CommandBase<ReadCard> DeleteCardCommand { get; protected set; }

        #endregion

        #region Action and Func

        private Action<ReadCardList> DeleteCardListAction { get;  set; }
        private Action<ReadCard> SelectedCardAction { get; set; }

        #endregion

        public CardListViewModel(ReadCardList list, Action<ReadCardList> onDeleteCardList, Action<ReadCard> onSelectedCard)
        {
            this.CardList = list;
            AddCardCommand = new CommandBase(OnAddCardClick);
            DeleteCardListCommand = new CommandBase<ReadCardList>(OnDeleteCardList);
            DeleteCardListAction = onDeleteCardList;
            RenameCardListCommand = new CommandBase<ReadCardList>(OnRenameCardList);
            SelectedCardAction = onSelectedCard;
            DeleteCardCommand = new CommandBase<ReadCard>(OnDeleteCard);

        }

        public async void OnAddCardClick()
        {
            if (ActiveUser.IsActive == true)
            {
                BasicCardDTO dto = await CardService.CreateCard(ActiveUser.Instance.LoggedUser.Token, new CreateCardDTO() { Name = "Untitled", Description = "No Description", DueDate = DateTime.Now.AddDays(7), ListId = cardList.ListId });

                if (dto != null)
                {
                    CardList.Cards.Add(new ReadCard(dto));
                }
                else
                {
                    ShowMessageBox(null, "Error creating card.");
                }
            }
        }

        public void OnDeleteCardList(ReadCardList card)
        {
            DeleteCardListAction?.Invoke(card);
        }

        public void OnRenameCardList(ReadCardList cardList)
        {
            ShowMessageBox(null, "Pravimo se da se otvara rename card list dijalog");
        }

        public async void OnDeleteCard(ReadCard card)
        {
            if (ActiveUser.IsActive == true && CardList != null)
            {
                bool succ = await CardService.DeleteCard(ActiveUser.Instance.LoggedUser.Token, card.CardId);
                if (succ == true)
                {
                    ReadCard rc = CardList.Cards.FirstOrDefault(x => x.CardId == card.CardId);
                    CardList.Cards.Remove(rc);
                }
                else
                {
                    ShowMessageBox(null, "Error deleting card list.");
                }
            }
            else
            {
                ShowMessageBox(null, "Error getting user.");
            }
        }
    }
}
