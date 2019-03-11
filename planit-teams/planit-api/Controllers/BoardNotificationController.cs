using planit_data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace planit_api.Controllers
{
    public class BoardNotificationController : ApiController
    {
        BoardNotificationService service = new BoardNotificationService();
        
        // GET: api/BoardNotification
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BoardNotification/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BoardNotification
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/BoardNotification/5
        [HttpPut]
        [Route("api/BoardNotification/User/{id}/Board/{boardId}")]
        public bool Put(int id, int boardId)
        {
            return service.ReadBoard(boardId, id);
        }

        // DELETE: api/BoardNotification/5
        public void Delete(int id)
        {
        }
    }
}
