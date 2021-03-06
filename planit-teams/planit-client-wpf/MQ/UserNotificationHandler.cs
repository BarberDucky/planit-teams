﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_client_wpf.Model;
using planit_client_wpf.Services;

namespace planit_client_wpf.MQ
{
    public class GivePermissionHandler : IUserNotificationHandler
    {
        public void HandleUserNotification(ObservableCollection<ShortBoard> boardList, ShortBoard selectedBoard, ObservableCollection<Notification> notifications, IMQMessage message)
        {
            boardList.Add(new ShortBoard(((BoardMesage)message).Data));
        }
    }

    public class DeletePermissionHandler : IUserNotificationHandler
    {
        public void HandleUserNotification(ObservableCollection<ShortBoard> boardList, ShortBoard selectedBoard, ObservableCollection<Notification> notifications, IMQMessage message)
        {
            ShortBoard deleteBoard = boardList.SingleOrDefault(el => el.BoardId == ((DeleteMessage)message).Data);
            boardList.Remove(deleteBoard);
            if (selectedBoard != null && selectedBoard.BoardId == deleteBoard.BoardId)
            {
                selectedBoard = null;
            }
        }
    }

    public class BoardChangeHandler : IUserNotificationHandler
    {
        public void HandleUserNotification(ObservableCollection<ShortBoard> boardList, ShortBoard selectedBoard, ObservableCollection<Notification> notifications, IMQMessage message)
        {
            int targetBoardId = ((BoardNotificationMessage)message).Data;
            if (selectedBoard == null || targetBoardId != selectedBoard.BoardId)
            {
                boardList.SingleOrDefault(el => el.BoardId == targetBoardId).IsRead = false;
            }
            else if (selectedBoard != null && targetBoardId == selectedBoard.BoardId)
            {
                //BoardNotificationService.ReadBoardNotification(ActiveUser.Instance.LoggedUser.Token, targetBoardId);
            }
        }
    }

    public class CardMoveHandler : IUserNotificationHandler
    {
        public void HandleUserNotification(ObservableCollection<ShortBoard> boardList, ShortBoard selectedBoard, ObservableCollection<Notification> notifications, IMQMessage message)
        {
            notifications.Add(new Notification(((NotificationMessage)message).Data));
        }
    }

    public class WatchedCardHandler : IUserNotificationHandler
    {
        public void HandleUserNotification(ObservableCollection<ShortBoard> boardList, ShortBoard selectedBoard, ObservableCollection<Notification> notifications, IMQMessage message)
        {
            notifications.Add(new Notification(((NotificationMessage)message).Data));
        }
    }
}
