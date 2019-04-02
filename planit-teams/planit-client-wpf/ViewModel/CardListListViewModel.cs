using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Model;
using planit_client_wpf.MQ;
using planit_client_wpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class CardListListViewModel : ViewModelBase, IDisposable
    {
        private ObservableCollection<CardListViewModel> cardListViewModels;
        private int parentBoardId;
        //private ReadCardList selectedCardList;
        private IPanelContainer detailsContainer;

        #region Properties

        public ObservableCollection<CardListViewModel> CardListViewModels
        {
            get { return cardListViewModels; }
            set { SetProperty(ref cardListViewModels, value); }
        }

        //public ReadCardList SelectedCardList
        //{
        //    get { return selectedCardList; }
        //    set { SetProperty(ref selectedCardList, value); }
        //}

        #endregion

        #region Commands

        public CommandBase NewListCommand { get; private set; }

        #endregion

        #region Message Actions

        private Action<object> createCardList;
        private Action<object> deleteCardList;
        private Action<object> moveCardAction;

        #endregion

        public CardListListViewModel(ObservableCollection<ReadCardList> cardLists, int parentBoardId, IPanelContainer detailsContainer)
        {
            this.parentBoardId = parentBoardId;
            this.CardListViewModels = new ObservableCollection<CardListViewModel>();
            this.detailsContainer = detailsContainer;

            foreach (ReadCardList cardList in cardLists)
            {
                CardListViewModels.Add(new CardListViewModel(cardList, OnDeleteCardList, detailsContainer, OnMoveCard, OnSelectedCard));
            }

            NewListCommand = new CommandBase(OnNewListClick);

            InitActions();
            Subscribe();
        }

        public async void OnNewListClick()
        {
            if (ActiveUser.IsActive == true)
            {
                CreateCardListDTO createCardListDTO = new CreateCardListDTO() { BoardId = parentBoardId, Color = "white", Name = "Untitled list" };
                BasicCardListDTO basicCardListDTO = await CardListService.CreateCardList(ActiveUser.Instance.LoggedUser.Token, createCardListDTO);

                if (basicCardListDTO != null)
                {
                    var list = new ReadCardList(basicCardListDTO);
                    CardListViewModels.Add(new CardListViewModel(list, OnDeleteCardList, detailsContainer, OnMoveCard, OnSelectedCard));
                }
                else
                {
                    ShowMessageBox(null, "Error creating list.");
                }
            }
            else
            {
                ShowMessageBox(null, "Error getting user.");
            }
        }

        public async void OnDeleteCardList(ReadCardList cardList)
        {
            if (ActiveUser.IsActive == true)
            {
                bool succ = await CardListService.DeleteCardList(ActiveUser.Instance.LoggedUser.Token, cardList.ListId);
                if (succ == true)
                {
                    CardListViewModel vm = CardListViewModels.FirstOrDefault(x => x.CardList.ListId == cardList.ListId);
                    CardListViewModels.Remove(vm);
                    vm.DestroyPanel();
                    ShowMessageBox(null, "Card list deleted.");
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

        public void OnSelectedCard(ReadCardList cardList)
        {
            foreach (CardListViewModel vm in CardListViewModels)
            {
                if (vm.CardList.ListId != cardList.ListId)
                {
                    vm.SelectedCard = null;
                }
            }
        }

        public async void OnMoveCard(MoveCard moveCard)
        {
            bool result = await CardService.MoveCard(ActiveUser.Instance.LoggedUser.Token, moveCard.CardId, moveCard.NewListId);
            if (!result)
            {
                ShowMessageBox(null, "Error moving card.");
                return;
            }

            CardListViewModel oldListVM = CardListViewModels.FirstOrDefault(listVM => listVM.CardList.ListId == moveCard.OldListId);
            if (oldListVM == null) return;

            ReadCard oldCard = oldListVM.CardList.Cards.FirstOrDefault(card => card.CardId == moveCard.CardId);
            if (oldCard == null) return;

            oldListVM.CardList.Cards.Remove(oldCard);
            oldCard.ListId = moveCard.NewListId;

            CardListViewModel newListVM = CardListViewModels.FirstOrDefault(listVM => listVM.CardList.ListId == moveCard.NewListId);
            if (newListVM == null) return;
            newListVM.CardList.Cards.Add(oldCard);

        }

        #region Subscribe for Notifications

        private void InitActions()
        {
            createCardList = new Action<object>(CreateCardListAction);
            deleteCardList = new Action<object>(DeleteCardListAction);
            moveCardAction = new Action<object>(MoveCardAction);
        }

        private void Subscribe()
        {
            MessageBroker.Instance.Subscribe(createCardList, MessageEnum.CardListCreate);
            MessageBroker.Instance.Subscribe(deleteCardList, MessageEnum.CardListDelete);
            MessageBroker.Instance.Subscribe(moveCardAction, MessageEnum.CardMove);
        }

        private void CreateCardListAction(object obj)
        {
            BasicCardListDTO cardList = (BasicCardListDTO)obj;

            if (cardList != null)
            {
                CardListViewModels.Add(new CardListViewModel(new ReadCardList(cardList), OnDeleteCardList, detailsContainer, OnMoveCard, OnSelectedCard));
            }
        }

        private void DeleteCardListAction(object obj)
        {
            int cardListId = (int)obj;

            CardListViewModel vm = CardListViewModels.FirstOrDefault(x => x.CardList.ListId == cardListId);

            if (vm != null)
            {
                CardListViewModels.Remove(vm);
            }       
        }

        private void MoveCardAction(object obj)
        {
            BasicCardDTO card = (BasicCardDTO)obj;
            ReadCard oldCard = null;
            ReadCardList oldList = null;

            if (card != null)
            {
                foreach (var vm in CardListViewModels)
                {

                    oldCard = vm.CardList.Cards.FirstOrDefault(c => c.CardId == card.CardId);
                    oldList = vm.CardList;

                    if (oldCard != null)
                        break;
                }

                oldList.Cards.Remove(oldCard);

                CardListViewModel newList = CardListViewModels.FirstOrDefault(c => c.CardList.ListId == card.ListId);

                if (newList != null)
                {
                    oldCard.ListId = card.ListId;
                    newList.CardList.Cards.Add(oldCard);
                }

            }
        }


        #endregion

        public void Dispose()
        {
            foreach(CardListViewModel vm in CardListViewModels)
            {
                vm.Dispose();
            }
        }
    }

}
