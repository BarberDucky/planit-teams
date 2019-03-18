using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using planit_client_wpf.Model;
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
        private ObservableCollection<ReadCardList> cardLists;
        private ObservableCollection<ViewModelBase> cardListViewModels;
        private int parentBoardId;

        #region Properties

        public ObservableCollection<ReadCardList> CardLists
        {
            get { return cardLists; }
            set { SetProperty(ref cardLists, value); }
        }

        public ObservableCollection<ViewModelBase> CardListViewModels
        {
            get { return cardListViewModels; }
            set { SetProperty(ref cardListViewModels, value); }
        }

        #endregion

        #region Commands

        public CommandBase NewListCommand { get; private set; }

        #endregion

        public CardListListViewModel(ObservableCollection<ReadCardList> cardLists, int parentBoardId)
        {
            this.CardLists = cardLists;
            this.parentBoardId = parentBoardId;
            this.CardListViewModels = new ObservableCollection<ViewModelBase>();
            foreach(ReadCardList cardList in cardLists)
            {
                CardListViewModels.Add(new CardListViewModel(cardList));
            }

            NewListCommand = new CommandBase(OnNewListClick);
        }

        public async void OnNewListClick()
        {
            if (ActiveUser.IsActive == true)
            {
                CreateCardListDTO createCardListDTO = new CreateCardListDTO() { Name = "Untitled list", Color = "white", BoardId = parentBoardId };
                BasicCardListDTO createdCardList = await CardListService.CreateCardList(ActiveUser.Instance.LoggedUser.Token, createCardListDTO);

                if (createdCardList != null)
                {
                    ShowMessageBox(null, "Kreirala se lista");
                    var list = new ReadCardList(createdCardList);
                    CardLists.Add(list);
                    CardListViewModels.Add(new CardListViewModel(list));
                }
                else
                {
                    ShowMessageBox(null, "Error getting user.");
                }
            }
            else
            {
                ShowMessageBox(null, "Error getting user.");
            }
        }

    }
}
