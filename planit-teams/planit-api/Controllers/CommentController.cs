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
    public class CommentController : ApiController
    {
        CommentService service = new CommentService();

        // GET: api/Comment
        public IEnumerable<ReadCommentDTO> Get()
        {
            return service.GetAllComments();
        }

        // GET: api/Comment/5
        public ReadCommentDTO Get(int id)
        {
            return service.GetCommentById(id);
        }

        // POST: api/Comment
        public bool Post([FromBody]CreateCommentDTO comment)
        {
            if (comment != null)
            {
                return (service.InsertComment(comment) != 0);
            }
            return false;
        }

        // PUT: api/Comment/5
        public bool Put(int id, [FromBody]UpdateCommentDTO comment)
        {
            if (comment != null)
            {
                return service.UpdateComment(comment);
            }
            return false;
        }

        // DELETE: api/Comment/5
        public bool Delete(int id)
        {
            return service.DeleteComment(id);
        }
    }
}
