﻿using planit_client_wpf.Base;
using planit_client_wpf.Model;
using planit_client_wpf.DTOs;
using planit_client_wpf.MQ;
using planit_client_wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace planit_client_wpf.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private ViewModelBase leftViewModel;
        private ViewModelBase centerViewModel;
        private ViewModelBase notificationViewModel;

        private MQService MQService;

        #region Properies

        public ViewModelBase LeftViewModel
        {
            get { return leftViewModel; }
            set { SetProperty(ref leftViewModel, value); }
        }

        public ViewModelBase CenterViewModel
        {
            get { return centerViewModel; }
            set { SetProperty(ref centerViewModel, value); }
        }

        public ViewModelBase NotificationViewModel
        {
            get { return notificationViewModel; }
            set { SetProperty(ref notificationViewModel, value); }
        }

        #endregion

        #region Commands

        public CommandBase LogoutCommand { get; protected set; }
        public CommandBase ToggleNotificationsCommand { get; protected set; }

        #endregion

        #region Actions and Func

        public Action GoToLogin;

        #endregion

        public HomeViewModel(Action goToLogin)
        {
            //Init MQ
            MQService = new MQService();
            InitializeMQ();

            //Init komande
            LogoutCommand = new CommandBase(OnLogoutButtonClick, CanLogout);
            ToggleNotificationsCommand= new CommandBase(OnToggleNotification);

            GoToLogin = goToLogin;

            //Init polja
            LeftViewModel = new BoardListViewModel(OnBoardSelected, OnBoardDeselectd);
            CenterViewModel = new EmptyViewModel();

            NotificationViewModel = new NotificationsViewModel();
            ((NotificationsViewModel)NotificationViewModel).IsOpen = false;

        }

        public void OnLogoutButtonClick()
        {
            ActiveUser.Instance.LoggedUser = null;
            GoToLogin?.Invoke();
        }

        public bool CanLogout()
        {
            return ActiveUser.Instance.LoggedUser != null;
        }

        public void OnBoardSelected(int boardId)
        {
            CenterViewModel = new BoardViewModel(boardId, OnBoardDeleted);
        }

        public void OnBoardDeselectd()
        {
            CenterViewModel = new EmptyViewModel();
        }

        public void OnBoardDeleted(int boardId)
        {
            if(LeftViewModel is BoardListViewModel)
            {
                var list = LeftViewModel as BoardListViewModel;
                list.RemoveSelectedBoard(boardId);
            }
        }

        public async void InitializeMQ()
        {
            ReadUserDTO readUserDTO = await UserService.GetUser(ActiveUser.Instance.LoggedUser.Token);

            if (readUserDTO != null)
            {
                MQService.SubscribeToExchange(readUserDTO.ExchangeName, (IMQMessage message) => 
                {
                    Application.Current.Dispatcher.BeginInvoke(
                      DispatcherPriority.Background,
                      new Action(() => {
                          IUserNotificationHandler userNotificationHandler = UserNotifHandlerFactory.CreateHandler(message);
                          userNotificationHandler.HandleUserNotification(
                              ((BoardListViewModel)leftViewModel).Boards,
                              ((BoardListViewModel)leftViewModel).SelectedBoard,
                              ((NotificationsViewModel)notificationViewModel).Notifications,
                              message);
                          ShowMessageBox(null, "Stigla poruka");
                      }));
                    return true;
                });
            }
        }

        public void OnToggleNotification()
        {
            if (!((NotificationsViewModel)NotificationViewModel).IsOpen)
            {
                ((NotificationsViewModel)NotificationViewModel).IsOpen = true;
            }
        }
    }
}
