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
    public class UserApiController : ApiController
    {
        UserService service = new UserService();
        // GET: api/UserApi
        public IEnumerable<ReadUserDTO> Get()
        {
            return service.GetAllUsers();
        }

        // GET: api/UserApi/5
        [Authorize]
        public ReadUserDTO Get(int id)
        {
            String user = User.Identity.Name;
            return service.GetUser(id);
        }

        // POST: api/UserApi
        public bool Post([FromBody]CreateUserDTO user)
        {
            if (user != null && service.InsertUser(user) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // PUT: api/UserApi/5
        public bool Put(int id, [FromBody]UpdateUserDTO user)
        {
            if (user != null)
            {
                return service.UpdateUser(user);
            }
            return false;

        }

        // DELETE: api/UserApi/5
        public bool Delete(int id)
        {
            return service.DeleteUser(id);
        }

        [HttpGet]
        [Route("api/UserApi/UserNotifications/{id:int}")]
        public IEnumerable<ReadNotificationDTO> UserNotifications(int id)
        {
            return service.GetUserNotifications(id);
        }

    }
}
