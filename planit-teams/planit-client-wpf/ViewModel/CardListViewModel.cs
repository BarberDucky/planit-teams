﻿using planit_client_wpf.Base;
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
        public ReadCardList cardList;

        #region Properties

        public ReadCardList CardList
        {
            get { return cardList; }
            set { SetProperty(ref cardList, value); }
        }

        #endregion

        #region Commands

        public CommandBase AddCardCommand { get; protected set; }
        public CommandBase<ReadCardList> DeleteCardListCommand { get; protected set; }

        #endregion

        #region Action and Func

        private Action<ReadCardList> DeleteCardListAction { get;  set; }

        #endregion

        public CardListViewModel(ReadCardList list, Action<ReadCardList> onDeleteCardList)
        {
            this.CardList = list;
            AddCardCommand = new CommandBase(OnAddCardClick);
            DeleteCardListCommand = new CommandBase<ReadCardList>(OnDeleteCardList);
            DeleteCardListAction = onDeleteCardList;
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

    }
}
