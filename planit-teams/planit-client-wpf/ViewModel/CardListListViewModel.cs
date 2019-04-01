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
    public class CardListListViewModel : ViewModelBase
    {
        private ObservableCollection<CardListViewModel> cardListViewModels;
        private int parentBoardId;
        //private ReadCardList selectedCardList;
        private ViewModelBase rightViewModel;

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

        public ViewModelBase RightViewModel
        {
            get { return rightViewModel; }
            set { SetProperty(ref rightViewModel, value); }
        }

        #endregion

        #region Commands

        public CommandBase NewListCommand { get; private set; }

        #endregion

        #region Message actions

        private Action<object> createCardList;
        private Action<object> deleteCardList;

        #endregion

        public CardListListViewModel(ObservableCollection<ReadCardList> cardLists, int parentBoardId)
        {
            this.parentBoardId = parentBoardId;
            this.CardListViewModels = new ObservableCollection<CardListViewModel>();

            foreach (ReadCardList cardList in cardLists)
            {
                CardListViewModels.Add(new CardListViewModel(cardList, OnDeleteCardList, OnSelectedCard));
            }

            NewListCommand = new CommandBase(OnNewListClick);
            RightViewModel = new EmptyViewModel();

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
                   // ShowMessageBox(null, "Kreirala se lista");
                    var list = new ReadCardList(basicCardListDTO);
                    CardListViewModels.Add(new CardListViewModel(list, OnDeleteCardList, OnSelectedCard));
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

        public void OnSelectedCard(ReadCard card)
        {
            if (card != null)
            {
                RightViewModel = new CardViewModel(card);
            }
            else
            {
                RightViewModel = new EmptyViewModel();
            }

        }

        #region Subscribe for Notifications

        private void InitActions()
        {
            createCardList = new Action<object>(CreateCardListAction);
            deleteCardList = new Action<object>(DeleteCardListAction);
        }

        private void Subscribe()
        {
            MessageBroker.Instance.Subscribe(createCardList, MessageEnum.CardListCreate);
            MessageBroker.Instance.Subscribe(deleteCardList, MessageEnum.CardListDelete);
        }

        private void CreateCardListAction(object obj)
        {
            BasicCardListDTO cardList = (BasicCardListDTO)obj;

            if (cardList != null)
            {
               CardListViewModels.Add(new CardListViewModel(new ReadCardList(cardList), OnDeleteCardList, OnSelectedCard)); 
            }
        }

        private void DeleteCardListAction(object obj)
        {
            int cardListId = (int)obj;
    
            CardListViewModel vm = CardListViewModels.FirstOrDefault(x => x.CardList.ListId == cardListId);
            CardListViewModels.Remove(vm);
        }

        #endregion
    }

}
