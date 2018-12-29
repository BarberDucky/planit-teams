using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using planit_data.DTOs;
using planit_data.Entities;
using planit_data.Services;

namespace planit_api.Controllers
{
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
        public bool Post([FromBody]CreateCardDTO value)
        {
            if (value != null)
                return (cs.InsertCard(value) != 0);
            return false;
        }

        // PUT: api/Card/5
        public bool Put(int id, [FromBody]UpdateCardDTO value)
        {
            if (value != null)
                return cs.UpdateCard(value);
            return false;
        }

        [Route("api/Card/{cardId:int}/Move/{listId:int}")]
        [HttpPut]
        public bool Move(int cardId, int listId)
        {
            return cs.MoveCardToList(cardId, listId);
        }

        // DELETE: api/Card/5
        public bool Delete(int id)
        {
            return cs.DeleteCard(id);
        }
    }
}
