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
    public class BoardNotificationController : ApiController
    {
        BoardNotificationService service = new BoardNotificationService();

        // PUT: api/BoardNotification/5
        [HttpPut]
        [Route("api/BoardNotification/Board/{boardId}")]
        public bool Put(int boardId)
        {
            return service.ReadBoard(boardId, User.Identity.Name);
        }
    }
}
