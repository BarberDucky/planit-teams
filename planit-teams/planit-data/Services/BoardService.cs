using planit_data.DTOs;
using planit_data.Entities;
using planit_data.RabbitMQ;
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
        public List<ReadBoardDTO> GetAllBoards()
        {
            List<ReadBoardDTO> dtos;
            using (UnitOfWork unit = new UnitOfWork())
            {
                List<Board> boards = unit.BoardRepository.GetAll();
                dtos = ReadBoardDTO.FromEntityList(boards);
            }

            return dtos;
        }

        public ReadBoardDTO GetBoard(int boardId)
        {
            ReadBoardDTO boardDTO = null;
            using (UnitOfWork unit = new UnitOfWork())
            {
                Board b = unit.BoardRepository.GetById(boardId);

                if (b != null)
                {
                    boardDTO = new ReadBoardDTO(b);
                }
            }

            return boardDTO;
        }

        public ReadBoardDTO GetBoardUser(int boardId, int userId)
        {
            ReadBoardDTO boardDTO = null;
            using (UnitOfWork unit = new UnitOfWork())
            {
                List<Permission> p = unit.PermissionRepository.Get(x => (x.User.UserId == userId && x.Board.BoardId == boardId)).ToList();

                if (p != null && p.Count > 0)
                {
                    Board b = p[0].Board;
                    boardDTO = new ReadBoardDTO(b);
                }
            }

            return boardDTO;
        }

        public List<ReadBoardDTO> GetBoardsByUser(int userId)
        {
            List<ReadBoardDTO> dtos = new List<ReadBoardDTO>();
            using (UnitOfWork unit = new UnitOfWork())
            {
                List<Board> boardList = new List<Board>();
                User user = unit.UserRepository.GetById(userId);

                if (user != null)
                {
                    foreach (var p in user.Permissions)
                    {
                        if (p != null)
                        {
                            boardList.Add(p.Board);
                        }
                    }
                    dtos = ReadBoardDTO.FromEntityList(boardList);
                }

            }

            return dtos;
        }

        //Ako ne uspe dodavanje board-a vratice se 0
        public int InsertBoard(CreateBoardDTO boardDTO)
        {
            Board board = boardDTO.FromDTO();
            board.ExchangeName = Guid.NewGuid().ToString();
            using (UnitOfWork unit = new UnitOfWork())
            {
                User creator = unit.UserRepository.GetById(boardDTO.CreatedByUser);

                if (board != null && creator != null)
                {
                    Permission permision = new Permission()
                    {
                        IsAdmin = true,
                        Board = board,
                        User = creator
                    };

                    unit.PermissionRepository.Insert(permision);
                    if (unit.Save())
                    {
                        RabbitMQService.DeclareExchange(board.ExchangeName);
                    }
                }
            }

            return board.BoardId;
        }

        public bool UpdateBoard(UpdateBoardDTO boardDTO)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                Board board = unit.BoardRepository.GetById(boardDTO.BoardId);

                if (board != null)
                {
                    board.Name = boardDTO.Name;

                    unit.BoardRepository.Update(board);
                    ret = unit.Save();
                    if (ret)
                    {
                        RabbitMQService.PublishToExchange(board.ExchangeName, $"Novo ime board {board.Name}");
                    }
                }
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
