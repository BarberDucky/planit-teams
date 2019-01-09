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
            return service.GetBoard(id);
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
        public bool Put(int id, [FromBody]UpdateBoardDTO board)
        {
            if (board != null)
            {
                return service.UpdateBoard(board);
            }

            return false;
        }

        // DELETE: api/Board/5
        public bool Delete(int id)
        {
            return service.DeleteBoard(id);
        }
    }
}
