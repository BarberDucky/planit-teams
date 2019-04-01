﻿using planit_client_wpf.Base;
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

        #endregion    

        public CardViewModel(ReadCard card)
        {
            if(card != null)
            {
                this.card = card;
                comments = new CommentsViewModel(card.Comments, card.CardId);
            }
        }

    }
}