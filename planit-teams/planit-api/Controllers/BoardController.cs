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
    public class BoardController : ApiController
    {
        BoardService service = new BoardService();
        // GET: api/Board
        public IEnumerable<ReadBoardDTO> Get()
        {
            return service.GetAllBoards();
        }

        // GET: api/Board/5
        public ReadBoardDTO Get(int id)
        {
            return service.GetBoard(id, 3);
        }

        // POST: api/Board
        public int Post([FromBody]CreateBoardDTO board)
        {
            if (board != null)
            {
                return service.InsertBoard(board);
            }
            else
            {
                return 0;
            }
        }

        // PUT: api/Board/5
        [HttpPut]
        [Route("api/Board/{userId:int}")]
        public bool Put(int userId, [FromBody]UpdateBoardDTO board)
        {
            if (board != null && PermissionHelper.HasPermissionOnBoard(board.BoardId, userId))
            {
                return service.UpdateBoard(board);
            }

            return false;
        }

        // DELETE: api/Board/5
        [HttpDelete]
        [Route("api/Board/{id}")]
        public bool Delete(int id)
        {
            return service.DeleteBoard(id, 1);
        }

        [HttpGet]
        [Route("api/Board/BoardsByUser/{id:int}")]
        public IEnumerable<ShortBoardDTO> BoardsByUser(int id)
        {
            return service.GetBoardsByUser(id);
        }

        [HttpGet]
        [Route("api/Board/{idBoard:int}/User/{idUser:int}")]
        public ReadBoardDTO BoardUser(int idBoard, int idUser)
        {
            if (PermissionHelper.HasPermissionOnBoard(idBoard, idUser))
            {
                return service.GetBoard(idBoard, idUser);
            }
            return null;
        }
    }
}
