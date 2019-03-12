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
    public class PermissionController : ApiController
    {
        PermissionService ps = new PermissionService();

        // PUT: api/Permission/5
        //public void Put(int id, [FromBody]UpdatePermissionDTO value)
        //{
        //    ps.UpdatePermission(value);
        //}

        //POST: api/Permission
        public bool Post([FromBody]AddUserBoardPermisionDTO value)
        {
            if (value != null)
            {
                return ps.AddUserBoardPermision(value, User.Identity.Name);
            }
            return false;
        }

        [HttpDelete]
        [Route("api/Permission/Board/{boardId}/User/{username}")]
        public bool Delete(int boardId, string username)
        {
            return ps.DeletePermission(boardId, username, User.Identity.Name);
        }

    }
}
