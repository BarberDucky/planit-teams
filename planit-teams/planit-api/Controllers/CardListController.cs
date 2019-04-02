using planit_data.DTOs;
using planit_data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace planit_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class CardListController : ApiController
    {
        CardListService service = new CardListService();

        //// GET: api/CardList
        //public IEnumerable<ReadCardListDTO> Get()
        //{
        //    return service.GetAllCardList();
        //}

        // GET: api/CardList/5
        [HttpGet]
        [Route("api/CardList/{id}")]
        public ReadCardListDTO Get(int id)
        {
            return service.GetCardList(id);
        }

        // POST: api/CardList
        [HttpPost]
        [Route("api/CardList")]
        public BasicCardListDTO Post([FromBody]CreateCardListDTO cardList)
        {
            return service.InsertCardList(cardList, User.Identity.Name);
        }

        // PUT: api/CardList/5
        [HttpPut]
        [Route("api/CardList/{id}")]
        public bool Put(int id, [FromBody]UpdateCardListDTO cardList)
        {
            if (cardList != null)
            {
                return service.UpdateCardList(id, cardList, User.Identity.Name);
            }
            return false;
        }

        // DELETE: api/CardList/5
        [HttpDelete]
        [Route("api/CardList/{id}")]
        public bool Delete(int id)
        {
            return service.DeleteCardList(id, User.Identity.Name);
        }

        [HttpGet]
        [Route("api/CardList/Board/{idBoard:int}")]
        public IEnumerable<ReadCardListDTO> CardListUser(int idBoard)
        {
            return service.GetAllCardListsOnBoard(idBoard);
        }

    }
}
