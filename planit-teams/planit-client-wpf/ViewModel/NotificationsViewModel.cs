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
    public class NotificationsViewModel : ViewModelBase
    {
        private Notification selectedNotification;
        private bool isOpen;
        private bool isNotificationUnread;

        public CommandBase ReadAllNotificationsCommand { get; private set; }


        #region Properties

        public ObservableCollection<Notification> Notifications { get; set; }

        public Notification SelectedNotification
        {
            get { return selectedNotification; }
            set
            {
                SetProperty(ref selectedNotification, value);
                SelectNotification();
            }
        }

        public bool IsNotificationUnread
        {
            get { return isNotificationUnread; }
            set { SetProperty(ref isNotificationUnread, value); }
        }

        public bool IsOpen
        {
            get { return isOpen; }
            set { SetProperty(ref isOpen, value); }
        }

        #endregion

        #region Message Actions
        Action<object> getNotification;
        #endregion

        public bool FindUnreadFlag()
        {
            foreach (Notification n in Notifications)
            {
                if (n.IsRead == false) return true;
            }
            return false;
        }

        public NotificationsViewModel()
        {
            ReadAllNotificationsCommand = new CommandBase(OnReadAllNotifications);
            Notifications = new ObservableCollection<Notification>();
            InitializeNotifications();
            InitActions();
            Subscribe();
        }

        private async void OnReadAllNotifications()
        {
            await NotificationService.ReadAllNotifications(ActiveUser.Instance.LoggedUser.Token);
            foreach (Notification notification in Notifications)
            {
                notification.IsRead = true;
            }
            IsNotificationUnread = FindUnreadFlag();
        }

        public async void InitializeNotifications()
        {
            List<ReadNotificationDTO> list = await UserService.GetUserNotifications(ActiveUser.Instance.LoggedUser.Token);
            foreach(ReadNotificationDTO notificationDTO in list)
            {
                Notifications.Add(new Notification(notificationDTO));
            }
            IsNotificationUnread = FindUnreadFlag();
        }

        private async void SelectNotification()
        {
            bool succ = await NotificationService.ReadNotification(ActiveUser.Instance.LoggedUser.Token,
                selectedNotification.NotificationId);

            if(succ)
            {
                selectedNotification.IsRead = true;
                IsNotificationUnread = FindUnreadFlag();
            }
            else
            {
                ShowMessageBox(null, "Error reading notification");
            }
        }

        #region Subscribe for Notifications

        private void InitActions()
        {
            getNotification = new Action<object>(MoveNotification);
        }

        private void Subscribe()
        {
            MessageBroker.Instance.Subscribe(getNotification, MessageEnum.NotificationMove);
            MessageBroker.Instance.Subscribe(getNotification, MessageEnum.NotificationChange);
        }

        private void MoveNotification(object obj)
        {
            ReadNotificationDTO dto = (ReadNotificationDTO)obj;

            if (dto != null)
            {
                Notifications.Add(new Notification(dto));
                IsNotificationUnread = FindUnreadFlag();
            }
        }

        #endregion
    }
}
