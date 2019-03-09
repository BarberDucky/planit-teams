using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using planit_data.DTOs;
using planit_data.Services;


namespace planit_api.Controllers
{
    public class NotificationController : ApiController
    {
        NotificationService ns = new NotificationService();

        // GET: api/Notification
        public IEnumerable<ReadNotificationDTO> Get()
        {
            return ns.GetAllNotifications();
        }

        // GET: api/Notification/5
        public ReadNotificationDTO Get(int id)
        {
            return ns.GetNotification(id);
        }

        // POST: api/Notification
        public void Post([FromBody]CreateNotificationDTO value)
        {
            ns.CreateMoveNotification(value);
        }

        // PUT: api/Notification/5
        // radi citanje notifikacije
        //public void Put(int id)
        //{
        //    ns.ReadNotification(id);
        //}

        // DELETE: api/Notification/5
        public void Delete(int id)
        {
            ns.DeleteNotification(id);
        }
    }
}
