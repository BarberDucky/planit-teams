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
        public bool Post([FromBody]CreateCardListDTO cardList)
        {
            if (cardList != null && service.InsertCardList(cardList) != 0)
                return true;
            return false;
        }

        // PUT: api/CardList/5
        public bool Put(int id, [FromBody]UpdateCardListDTO cardList)
        {
            if(cardList!=null)
            {
                return service.UpdateCardList(cardList);
            }
            return false;
        }

        // DELETE: api/CardList/5
        public bool Delete(int id)
        {
            return service.DeleteCardList(id);
        }
    }
}
