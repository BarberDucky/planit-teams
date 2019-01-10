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
        private BoardMesage boardMessage;

        public BoardMessageStrategy(ReadBoardDTO data)
        {
            boardMessage = new BoardMesage()
            {
                Data = data
            };
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(boardMessage);
        }
    }

    public class CardMessageStrategy : IMessageStrategy
    {
        private CardMessage cardMessage;

        public CardMessageStrategy(ReadCardDTO data)
        {
            cardMessage = new CardMessage()
            {
                Data = data
            };
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(cardMessage);
        }
    }

    public class CardListMessageStrategy : IMessageStrategy
    {
        private CardListMessage cardListMessage;

        public CardListMessageStrategy(ReadCardListDTO data)
        {
            cardListMessage = new CardListMessage()
            {
                Data = data
            };
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(cardListMessage);
        }
    }
}
