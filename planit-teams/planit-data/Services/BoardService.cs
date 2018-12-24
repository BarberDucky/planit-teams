using planit_data.DTOs;
using planit_data.Entities;
using planit_data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Services
{
    public class BoardService
    {
        public ReadBoardDTO GetBoard(int boardId)
        {
            ReadBoardDTO boardDTO = null;
            using (UnitOfWork unit = new UnitOfWork())
            {
                Board b = unit.BoardRepository.GetById(boardId);

                if(b!=null)
                {
                    boardDTO = new ReadBoardDTO(b);
                }
            }

            return boardDTO;
        }

        public List<ReadBoardDTO> GetBoardsByUser(int userId)
        {
            List<ReadBoardDTO> dtos;
            using (UnitOfWork unit = new UnitOfWork())
            {
                List<Board> boardList = new List<Board>();
                User user = unit.UserRepository.GetById(userId);

                foreach(var p in user.Permissions)
                {
                    boardList.Add(p.Board);
                }

                dtos = ReadBoardDTO.FromEntityList(boardList);
            }

            return dtos;
        }

        //Ako ne uspe dodavanje board-a vratice se 0
        public int InsertBoard(CreateBoardDTO boardDTO)
        {
            Board board = boardDTO.FromDTO(); 
            using (UnitOfWork unit = new UnitOfWork())
            {
                User creator = unit.UserRepository.GetById(boardDTO.CreatedByUser);

                Permission permision = new Permission()
                {
                    IsAdmin = true,
                    Board = board,
                    User = creator
                };

                unit.PermissionRepository.Insert(permision);
                unit.Save();
            }

            return board.BoardId;
        }

        public bool UpdateBoard(UpdateBoardDTO boardDTO)
        {
            bool ret;
            using (UnitOfWork unit = new UnitOfWork())
            {
                Board board = unit.BoardRepository.GetById(boardDTO.BoardId);
                board.Name = boardDTO.Name;

                unit.BoardRepository.Update(board);
                ret = unit.Save();
            }

            return ret;
        }

        public bool DeleteBoard(int id)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.BoardRepository.Delete(id);
                ret = unit.Save();
            }

            return ret;
        }
    }
}
