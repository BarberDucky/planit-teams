using planit_client_wpf.Base;
using planit_client_wpf.Model;
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

        #region Properties

        public ObservableCollection<Notification> Notifications { get; set; }

        public Notification SelectedNotification
        {
            get { return selectedNotification; }
            set
            {
                SetProperty(ref selectedNotification, value);
                selectedNotification.IsRead = true;
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
            Notifications = new ObservableCollection<Notification>();

            Notifications.Add(new Notification() { IsRead = false, NotificationId = 1});
            Notifications.Add(new Notification() { IsRead = true, NotificationId = 2});
            Notifications.Add(new Notification() { IsRead = false, NotificationId = 3});
        }
    }
}
