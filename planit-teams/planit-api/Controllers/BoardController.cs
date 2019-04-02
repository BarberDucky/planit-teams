using planit_data.DTOs;
using planit_data.Helpers;
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
    public class BoardController : ApiController
    {
        BoardService service = new BoardService();
        //// GET: api/Board
        //public IEnumerable<ReadBoardDTO> Get()
        //{
        //    return service.GetAllBoards();
        //}

        // GET: api/Board/5
        [HttpGet]
        [Route("api/Board/{id}")]
        public ReadBoardDTO Get(int id)
        {
            return service.GetBoard(id, User.Identity.Name);
        }

        // POST: api/Board
        [HttpPost]
        [Route("api/Board")]
        public ShortBoardDTO Post([FromBody]CreateBoardDTO board)
        {
            return service.InsertBoard(board, User.Identity.Name);
        }

        // PUT: api/Board/5
        [HttpPut]
        [Route("api/Board/{boardId:int}")]
        public bool Put(int boardId, [FromBody]UpdateBoardDTO board)
        {
            if (board != null)
            {
                return service.UpdateBoard(boardId, board, User.Identity.Name);
            }

            return false;
        }

        // DELETE: api/Board/5
        [HttpDelete]
        [Route("api/Board/{id:int}")]
        public bool Delete(int id)
        {
            return service.DeleteBoard(id, User.Identity.Name);
        }

        [HttpGet]
        [Route("api/Board/BoardsByUser")]
        public IEnumerable<ShortBoardDTO> BoardsByUser()
        {
            return service.GetBoardsByUser(User.Identity.Name);
        }

        //[HttpGet]
        //[Route("api/Board/{idBoard:int}/User/{idUser:int}")]
        //public ReadBoardDTO BoardUser(int idBoard, int idUser)
        //{
        //    if (PermissionHelper.HasPermissionOnBoard(idBoard, idUser))
        //    {
        //        return service.GetBoard(idBoard, User.Identity.Name);
        //    }
        //    return null;
        //}
    }
}
