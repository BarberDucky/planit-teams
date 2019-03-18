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
    public class NotificationsViewModel : ViewModelBase
    {
        private Notification selectedNotification;
        private bool isOpen;

        public CommandBase ReadAllNotificationsCommand { get; private set; }


        #region Properties

        public ObservableCollection<Notification> Notifications { get; set; }

        public Notification SelectedNotification
        {
            get { return selectedNotification; }
            set
            {
                SetProperty(ref selectedNotification, value);
                selectedNotification.IsRead = true;
                NotificationService.ReadNotification(ActiveUser.Instance.LoggedUser.Token, selectedNotification.NotificationId);
            }
        }

        public bool IsOpen
        {
            get { return isOpen; }
            set { SetProperty(ref isOpen, value); }
        }

        #endregion

        public NotificationsViewModel()
        {
            ReadAllNotificationsCommand = new CommandBase(OnReadAllNotifications);
            Notifications = new ObservableCollection<Notification>();
            InitializeNotifications();
        }

        private async void OnReadAllNotifications()
        {
            await NotificationService.ReadAllNotifications(ActiveUser.Instance.LoggedUser.Token);
            foreach (Notification notification in Notifications)
            {
                notification.IsRead = true;
            }
        }

        public async void InitializeNotifications()
        {
            List<ReadNotificationDTO> list = await UserService.GetUserNotifications(ActiveUser.Instance.LoggedUser.Token);
            foreach(ReadNotificationDTO notificationDTO in list)
            {
                Notifications.Add(new Notification(notificationDTO));
            }
        }
    }
}
