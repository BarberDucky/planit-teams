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
    public class UserApiController : ApiController
    {
        UserService service = new UserService();

        // GET: api/UserApi
        //public IEnumerable<ReadUserDTO> Get()
        //{
        //    return service.GetAllUsers();
        //}

        // GET: api/UserApi
        [HttpGet]
        [Route("api/UserApi")]
        public ReadUserDTO GetUser()
        {
            return service.GetUser(User.Identity.Name);
        }

        // POST: api/UserApi
        [AllowAnonymous]
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
        [HttpPut]
        [Route("api/UserApi")]
        public bool Put([FromBody]UpdateUserDTO user)
        {
            if (user != null && User.Identity.IsAuthenticated)
            {
                return service.UpdateUser(user, User.Identity.Name);
            }
            return false;

        }

        // DELETE: api/UserApi/5
        [HttpDelete]
        [Route("api/UserApi")]
        public bool Delete()
        {
            if (User.Identity.IsAuthenticated)
            {
                return service.DeleteUser(User.Identity.Name);
            }
            return false;
        }

        [HttpGet]
        [Route("api/UserApi/UserNotifications")]
        public IEnumerable<ReadNotificationDTO> UserNotifications()
        {
            if (User.Identity.IsAuthenticated)
            {
                return service.GetUserNotifications(User.Identity.Name);
            }

            return new List<ReadNotificationDTO>();
        }

    }
}
