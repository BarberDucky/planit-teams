using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using planit_data.DTOs;
using planit_data.Entities;
using planit_data.Services;

namespace planit_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CardController : ApiController
    {

        CardService cs = new CardService();

        // GET: api/Card
        public IEnumerable<ReadCardDTO> Get()
        {
            return cs.GetAllCards();
        }

        // GET: api/Card/5
        public ReadCardDTO Get(int id)
        {
            return cs.GetCardById(id);
        }

        // POST: api/Card
        [Route("api/Card/{userId:int}")]
        [HttpPost]
        public bool Post(int userId, [FromBody]CreateCardDTO value)
        {
            if (value != null)
                return (cs.InsertCard(userId, value) != 0);
            return false;
        }

        // PUT: api/Card/5
        [HttpPut]
        [Route("api/Card/{id}")]
        public bool Put(int id, [FromBody]UpdateCardDTO value)
        {
            if (value != null)
                return cs.UpdateCard(value);
            return false;
        }

        [Route("api/Card/{cardId:int}/Move/{listId:int}/User/{userId:int}")]
        [HttpPut]
        public bool Move(int cardId, int listId, int userId)
        {
            return cs.MoveCardToList(cardId, listId, userId);
        }

        // DELETE: api/Card/5
        [HttpDelete]
        [Route("api/Card/{id}")]
        public bool Delete(int id)
        {
            return cs.DeleteCard(id);
        }

        [HttpGet]
        [Route("api/Card/{idBoard:int}/User/{idUser:int}")]
        public IEnumerable<ReadCardDTO> CardsUser(int idBoard, int idUser)
        {
            return cs.GetAllCardsOnBoard(idBoard, idUser);
        }

        [HttpGet]
        [Route("api/Card/CardByUser/{cardId:int}/User/{idUser:int}")]
        public ReadCardDTO CardByUser(int cardId, int idUser)
        {
            return cs.GetCardByUser(cardId, idUser);
        }

        [HttpGet]
        [Route("api/Card/WatchCard/{cardId:int}/User/{idUser:int}")]
        public bool WatchCard(int cardId, int idUser)
        {
            return cs.WatchCard(cardId, idUser);
        }

        [HttpGet]
        [Route("api/Card/UnwatchCard/{cardId:int}/User/{idUser:int}")]
        public bool UnwatchCard(int cardId, int idUser)
        {
            return cs.UnwatchCard(cardId, idUser);
        }
    }
}
