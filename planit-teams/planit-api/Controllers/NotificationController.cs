﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using planit_data.DTOs;
using planit_data.Services;


namespace planit_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class NotificationController : ApiController
    {
        NotificationService ns = new NotificationService();

        //// GET: api/Notification
        //public IEnumerable<ReadNotificationDTO> Get()
        //{
        //    return ns.GetAllNotifications();
        //}

        // GET: api/Notification/5
        [HttpGet]
        [Route("api/Notification/{id}")]
        public ReadNotificationDTO Get(int id)
        {
            return ns.GetNotification(id);
        }

        //// POST: api/Notification
        //public void Post([FromBody]CreateNotificationDTO value)
        //{
        //    ns.CreateMoveNotification(value);
        //}

        // PUT: api/Notification/5
        // radi citanje notifikacije
        [HttpPut]
        [Route("api/Notification/Read/{id}")]
        public bool Put(int id)
        {
            return ns.ReadNotification(id);
        }

        // PUT: api/Notification/5
        // radi citanje svih notifikacija
        [HttpPut]
        [Route("api/Notification/ReadAll")]
        public bool ReadAll()
        {
            return ns.ReadAllNotifications(User.Identity.Name);
        }

        //// DELETE: api/Notification/5
        //public void Delete(int id)
        //{
        //    ns.DeleteNotification(id);
        //}
    }
}
