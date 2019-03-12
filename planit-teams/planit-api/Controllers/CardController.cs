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
    [Authorize]
    public class CardController : ApiController
    {
        CardService cs = new CardService();

        //// GET: api/Card
        //public IEnumerable<ReadCardDTO> Get()
        //{
        //    return cs.GetAllCards();
        //}

        // GET: api/Card/5
        [HttpGet]
        [Route("api/Card/{id}")]
        public ReadCardDTO Get(int id)
        {
            return cs.GetCardById(id);
        }

        // POST: api/Card
        [HttpPost]
        [Route("api/Card")]
        public BasicCardDTO Post([FromBody]CreateCardDTO value)
        {
            return cs.InsertCard(User.Identity.Name, value);
        }

        // PUT: api/Card/5
        [HttpPut]
        [Route("api/Card/{id}")]
        public bool Put(int id, [FromBody]UpdateCardDTO value)
        {
            if (value != null)
                return cs.UpdateCard(id, User.Identity.Name, value);
            return false;
        }

        [Route("api/Card/{cardId:int}/Move/{listId:int}")]
        [HttpPut]
        public bool Move(int cardId, int listId)
        {
            return cs.MoveCardToList(cardId, listId, User.Identity.Name);
        }

        // DELETE: api/Card/5
        [HttpDelete]
        [Route("api/Card/{id}")]
        public bool Delete(int id)
        {
            return cs.DeleteCard(id);
        }

        [HttpGet]
        [Route("api/Card/List/{listId:int}")]
        public IEnumerable<ReadCardDTO> CardsUser(int listId)
        {
            return cs.GetAllCardsOnList(listId);
        }

        [HttpGet]
        [Route("api/Card/WatchCard/{cardId:int}")]
        public bool WatchCard(int cardId)
        {
            return cs.WatchCard(cardId, User.Identity.Name);
        }

        [HttpGet]
        [Route("api/Card/UnwatchCard/{cardId:int}")]
        public bool UnwatchCard(int cardId)
        {
            return cs.UnwatchCard(cardId, User.Identity.Name);
        }
    }
}
