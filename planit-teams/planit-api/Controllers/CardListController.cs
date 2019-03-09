using planit_data.DTOs;
using planit_data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace planit_api.Controllers
{
    public class CardListController : ApiController
    {
        CardListService service = new CardListService();

        // GET: api/CardList
        public IEnumerable<ReadCardListDTO> Get()
        {
            return service.GetAllCardList();
        }

        // GET: api/CardList/5
        public ReadCardListDTO Get(int id)
        {
            return service.GetCardList(id);
        }

        // POST: api/CardList
        [Route("api/CardList/{userId:int}")]
        [HttpPost]
        public bool Post(int userId, [FromBody]CreateCardListDTO cardList)
        {
            if (cardList != null && service.InsertCardList(userId, cardList) != 0)
                return true;
            return false;
        }

        // PUT: api/CardList/5
        [HttpPut]
        [Route("api/CardList/{id}")]
        public bool Put(int id, [FromBody]UpdateCardListDTO cardList)
        {
            if(cardList!=null)
            {
                return service.UpdateCardList(cardList);
            }
            return false;
        }

        // DELETE: api/CardList/5
        [HttpDelete]
        [Route("api/CardList/{id}")]
        public bool Delete(int id)
        {
            return service.DeleteCardList(id);
        }

        [HttpGet]
        [Route("api/CardList/{idBoard:int}/User/{idUser:int}")]
        public IEnumerable<ReadCardListDTO> CardListUser(int idBoard, int idUser)
        {
            return service.GetAllCardListsOnBoard(idBoard, idUser);
        }

        [HttpGet]
        [Route("api/CardList/CardListByUser/{cardListId:int}/User/{idUser:int}")]
        public ReadCardListDTO CardListByUser(int cardListId, int idUser)
        {
            return service.GetCardListByUser(cardListId, idUser);
        }
    }
}
