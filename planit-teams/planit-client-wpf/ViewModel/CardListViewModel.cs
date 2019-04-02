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
    public class CardListViewModel : PanelOwnerViewModel, IDisposable
    {
        private ReadCardList cardList;
        private ReadCard selectedCard;
        //private ObservableCollection<CardViewModel> cardViewModels;
        //IPanelContainer container;
        //CardViewModel cardViewModel;
        //IPanelOwner PanelOwner { get; set; }


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
                //CardViewModel vm = cardViewModels.FirstOrDefault(x => x.Card.CardId == selectedCard.CardId);
                //container.InstantiateDetailsPanel(vm);
                if (selectedCard != null)
                {
                    var cardViewModel = new CardViewModel(selectedCard, OnEditButtonClick);
                    base.InstantiatePanel(cardViewModel);
                    //container.InstantiatePanel(cardViewModel, this);
                }
                //else
                //{
                //    openContainer = null;
                //    container.InstantiateDetailsPanel(new EmptyViewModel(), null);
                //}

            }
        }

        public void OnEditButtonClick(EditCard card)
        {
            var panel = new EditCardViewModel(OnEditCardCompleted, card);
            //DestroyPanel();
            InstantiatePanel(panel);
        }

        public void OnEditCardCompleted(IEditable model)
        {
            if (model != null)
            {
                EditCard editCard = model as EditCard;
                if (editCard != null)
                {
                    var readCard = CardList.Cards.FirstOrDefault(x => x.CardId == editCard.CardId);
                    readCard?.UpdateEditedFields(editCard);
                    //container.InstantiatePanel(new EmptyViewModel(), null);
                    //DestroyPanel();
                    InstantiatePanel(new CardViewModel(readCard, OnEditButtonClick));
                }
            }
        }

        //public ObservableCollection<CardViewModel> CardViewModels
        //{
        //    get { return cardViewModels; }
        //    set { SetProperty(ref cardViewModels, value); }
        //}

        #endregion

        #region Commands

        public CommandBase AddCardCommand { get; protected set; }
        public CommandBase<ReadCardList> DeleteCardListCommand { get; protected set; }
        public CommandBase<ReadCardList> RenameCardListCommand { get; protected set; }
        public CommandBase<ReadCard> DeleteCardCommand { get; protected set; }

        #endregion

        #region Action and Func

        private Action<ReadCardList> DeleteCardListAction { get; set; }
        //private Action<ReadCard> SelectedCardAction { get; set; }
        private Action<MoveCard> OnMoveCardAction { get; set; }

        #endregion

        #region Message Actions

        private Action<object> createCardAction;
        private Action<object> deleteCardAction;
        private Action<object> updateCardAction;
        private Action<object> createCommentAction;
        private Action<object> updateCardListAction;
        #endregion

        public CardListViewModel(ReadCardList list, Action<ReadCardList> onDeleteCardList, IPanelContainer container, Action<MoveCard> onMoveCard)
            :base(container)
        {
            this.CardList = list;
            //this.container = container;
            //this.cardViewModel = null;
            //Init
            //CardViewModels = new ObservableCollection<CardViewModel>();
            //foreach(ReadCard card in list.Cards)
            //{
            //    CardViewModels.Add(new CardViewModel(card, container));
            //}

            //PanelOwner = new PanelOwnerViewModel(container)

            //Commands
            AddCardCommand = new CommandBase(OnAddCardClick);
            DeleteCardListCommand = new CommandBase<ReadCardList>(OnDeleteCardList);
            DeleteCardListAction = onDeleteCardList;
            RenameCardListCommand = new CommandBase<ReadCardList>(OnRenameCardList);
            //SelectedCardAction = onSelectedCard;
            DeleteCardCommand = new CommandBase<ReadCard>(OnDeleteCard);
            OnMoveCardAction = onMoveCard;

            InitActions();
            Subscribe();

        }

        public void OnMoveCard(MoveCard moveCard)
        {
            OnMoveCardAction?.Invoke(moveCard);
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
                    //if(cardViewModel != null && cardViewModel.Card.CardId == card.CardId)
                    //{
                    //    cardViewModel = null;
                    //    container.InstantiatePanel(new EmptyViewModel(), null);
                    //}
                    //if(base.HasPanelOpen)
                    //    base.DestroyPanel();

                    if(HasPanelOpen == true && OpenPanel is CardViewModel)
                    {
                        var panel = OpenPanel as CardViewModel;
                        if (panel.Card.CardId == card.CardId)
                        {
                            DestroyPanel();
                            SelectedCard = null;
                        }
                    }
                    else if(HasPanelOpen == true && OpenPanel is EditCardViewModel)
                    {
                        var panel = OpenPanel as EditCardViewModel;
                        if (panel.Card.CardId == card.CardId)
                            DestroyPanel();

                    }

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

        #region Subscribe for Notifications

        private void InitActions()
        {
            createCardAction = new Action<object>(CreateCardAction);
            deleteCardAction = new Action<object>(DeleteCardAction);
            createCommentAction = new Action<object>(CreateCommentAction);
            updateCardAction = new Action<object>(UpdateCardAction);
            updateCardListAction = new Action<object>(UpdateCardListAction);
        }

        private void Subscribe()
        {
            MessageBroker.Instance.Subscribe(createCardAction, MessageEnum.CardCreate);
            MessageBroker.Instance.Subscribe(deleteCardAction, MessageEnum.CardDelete);
            MessageBroker.Instance.Subscribe(createCommentAction, MessageEnum.CommentCreate);
            MessageBroker.Instance.Subscribe(updateCardAction, MessageEnum.CardUpdate);
            MessageBroker.Instance.Subscribe(updateCardListAction, MessageEnum.CardListUpdate);
        }

        private void CreateCardAction(object obj)
        {
            BasicCardDTO card = (BasicCardDTO)obj;

            if (card != null && card.ListId == cardList.ListId)
            {
                CardList.Cards.Add(new ReadCard(card));
            }
        }

        private void DeleteCardAction(object obj)
        {
            int id = (int)obj;

            ReadCard rc = CardList.Cards.FirstOrDefault(x => x.CardId == id);

            if (rc != null)
            {
                CardList.Cards.Remove(rc);
            }
        }

        private void CreateCommentAction(object obj)
        {
            BasicCommentDTO comment = (BasicCommentDTO)obj;

            if (comment != null && comment.CardListId == CardList.ListId)
            {
                ReadCard card = CardList.Cards.FirstOrDefault(c => c.CardId == comment.CardId);

                if (card != null)
                {
                    card.Comments.Add(new ReadComment(comment));
                }
            }
        }

        public void OnEditCompleted(IEditable card)
        {
            EditCard c = card as EditCard;
            ShowMessageBox(null, "Editovala sam karticu " + c.Name);
        }

        //public void NotifyContainerClosing()
        //{
        //    //cardViewModel = null;
        //    //SelectedCard = null;
        //    hasPanelOpen = false;
        //}

        //public void Dispose()
        //{
        //    if(cardViewModel != null)
        //    {
        //        container.InstantiatePanel(new EmptyViewModel(), null);
        //        cardViewModel.Dispose();
        //        cardViewModel = null;
        //    }
        //}

        public void Dispose()
        {
            base.DestroyPanel();
        }

        //TODO - proveriti da li radi sa interface-om
        private void UpdateCardAction(object obj)
        {
            BasicCardDTO newCard = (BasicCardDTO)obj;

            if (newCard != null)
            {
                ReadCard old = CardList.Cards.FirstOrDefault(c => c.CardId == newCard.CardId);

                if (old != null)
                {
                    ReadCard.UpdateCard(old, newCard);
                }
            }
        }

        private void UpdateCardListAction(object obj)
        {
            BasicCardListDTO newList = (BasicCardListDTO)obj;

            if (newList != null && newList.ListId == cardList.ListId)
            {
                ReadCardList.UpdateCardList(CardList, newList);
            }
        }

        
        #endregion
    }
}
