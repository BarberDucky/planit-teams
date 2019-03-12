using Newtonsoft.Json;
using planit_data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.RabbitMQ
{
    public class BoardMessageStrategy : IMessageStrategy
    {
        private bool isDeleteMessage = false;
        private BoardMesage boardMessage;
        private DeleteMessage deleteMessage;

        public BoardMessageStrategy(BasicBoardDTO data, MessageType type)
        {
            boardMessage = new BoardMesage()
            {
                Data = data
            };
        }

        public BoardMessageStrategy(int id)
        {
            deleteMessage = new DeleteMessage()
            {
                Data = id,
                MessageEntity = MessageEntity.Board
            };

            isDeleteMessage = true;
        }

        public string Serialize()
        {
            if (!isDeleteMessage)
            {
                return JsonConvert.SerializeObject(boardMessage);
            }
            else
            {
                return JsonConvert.SerializeObject(deleteMessage);
            }
            
        }
    }

    public class CardMessageStrategy : IMessageStrategy
    {
        private bool isDeleteMessage = false;
        private CardMessage cardMessage;
        private DeleteMessage deleteMessage;

        public CardMessageStrategy(BasicCardDTO data, MessageType type)
        {
            cardMessage = new CardMessage()
            {
                Data = data,
                MessageType = type
            };
        }

        public CardMessageStrategy(int id)
        {
            deleteMessage = new DeleteMessage()
            {
                Data = id,
                MessageEntity = MessageEntity.Card
            };

            isDeleteMessage = true;
        }

        public string Serialize()
        {
            if (!isDeleteMessage)
            {
                return JsonConvert.SerializeObject(cardMessage);
            }
            else
            {
                return JsonConvert.SerializeObject(deleteMessage);
            }

        }
    }

    public class CardListMessageStrategy : IMessageStrategy
    {
        private bool isDeleteMessage = false;
        private CardListMessage cardListMessage;
        private DeleteMessage deleteMessage;

        public CardListMessageStrategy(BasicCardListDTO data, MessageType type)
        {
            cardListMessage = new CardListMessage()
            {
                Data = data,
                MessageType = type
            };
        }

        public CardListMessageStrategy(int id)
        {
            deleteMessage = new DeleteMessage()
            {
                Data = id,
                MessageEntity = MessageEntity.CardList
            };

            isDeleteMessage = true;
        }

        public string Serialize()
        {
            if (!isDeleteMessage)
            {
                return JsonConvert.SerializeObject(cardListMessage);
            }
            else
            {
                return JsonConvert.SerializeObject(deleteMessage);
            }

        }
    }

    //Done
    public class CommentMessageStrategy : IMessageStrategy
    {
        private CommentMessage commentMessage;

        public CommentMessageStrategy(BasicCommentDTO data)
        {
            commentMessage = new CommentMessage()
            {
                Data = data
            };
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(commentMessage);
        }
    }

    public class NotificationMessageStrategy : IMessageStrategy
    {
        private NotificationMessage notificationMessage;

        public NotificationMessageStrategy(ReadNotificationDTO data, MessageType type)
        {
            notificationMessage = new NotificationMessage()
            {
                Data = data,
                MessageType = type
            };
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(notificationMessage);
        }
    }

    public class BoardNotificationMessageStrategy : IMessageStrategy
    {
        private BoardNotificationMessage boardMessage;

        public BoardNotificationMessageStrategy(int id)
        {
            boardMessage = new BoardNotificationMessage()
            {
                Data = id
            };
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(boardMessage);
        }
    }
}
