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
    public class PermissionController : ApiController
    {
        PermissionService ps = new PermissionService();

        // PUT: api/Permission/5
        public void Put(int id, [FromBody]UpdatePermissionDTO value)
        {
            ps.UpdatePermission(value);
        }
    }
}
