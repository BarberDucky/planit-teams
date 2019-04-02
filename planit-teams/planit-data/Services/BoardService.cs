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
        #region Should Delete
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

        public ReadBoardDTO GetBoardUser(int boardId, int userId)
        {
            ReadBoardDTO boardDTO = null;
            using (UnitOfWork unit = new UnitOfWork())
            {
                List<Permission> p = unit.PermissionRepository
                    .Get(x => (x.User.UserId == userId && x.Board.BoardId == boardId))
                    .ToList();

                if (p != null && p.Count > 0)
                {
                    Board b = p[0].Board;
                    boardDTO = new ReadBoardDTO(b);
                }
            }

            return boardDTO;
        }
        #endregion

        public ReadBoardDTO GetBoard(int boardId, string username)
        {
            ReadBoardDTO boardDTO = null;
            using (UnitOfWork unit = new UnitOfWork())
            {
                Board b = unit.BoardRepository.GetById(boardId);
                User user = unit.UserRepository.GetUserByUsername(username);

                if (b != null && user != null)
                {
                    List<User> users = unit.PermissionRepository
                        .GetAllUsersWithPermissionOnBoard(b.BoardId);

                    bool isAdmin = unit.PermissionRepository
                        .IsAdmin(b.BoardId, username);

                    boardDTO = new ReadBoardDTO(b, username, isAdmin, users);
                }
            }

            return boardDTO;
        }

        public List<ShortBoardDTO> GetBoardsByUser(string username)
        {
            List<ShortBoardDTO> dtos = new List<ShortBoardDTO>();
            using (UnitOfWork unit = new UnitOfWork())
            {
                User user = unit.UserRepository.GetUserByUsername(username);

                if (user != null)
                {
                    foreach (var p in user.Permissions)
                    {
                        if (p != null && p.Board != null)
                        {
                            BoardNotification notif = unit.BoardNotificationRepository
                                .GetBoardNotification(p.Board.BoardId, user.UserId);
                            if (notif != null)
                            {
                                dtos.Add(new ShortBoardDTO(p.Board, notif.IsRead));
                            }
                            else
                            {
                                dtos.Add(new ShortBoardDTO(p.Board, true));
                            }

                        }
                    }
                }

            }

            return dtos;
        }

        //Ako ne uspe dodavanje board-a vratice se null
        public ShortBoardDTO InsertBoard(CreateBoardDTO boardDTO, string username)
        {
            Board board = boardDTO.FromDTO();
            board.ExchangeName = Guid.NewGuid().ToString();
            ShortBoardDTO dto = null;
            using (UnitOfWork unit = new UnitOfWork())
            {
                User creator = unit.UserRepository.GetUserByUsername(username);

                if (board != null && creator != null)
                {
                    Permission permision = new Permission()
                    {
                        IsAdmin = true,
                        Board = board,
                        User = creator
                    };

                    BoardNotification boardNotif = new BoardNotification()
                    {
                        Board = board,
                        User = creator
                    };

                    unit.PermissionRepository.Insert(permision);
                    unit.BoardNotificationRepository.Insert(boardNotif);

                    if (unit.Save())
                    {
                        dto = new ShortBoardDTO(board, true);
                        RabbitMQService.DeclareExchange(board.ExchangeName);
                    }
                }
            }

            return dto;
        }

        public bool UpdateBoard(int boardId, UpdateBoardDTO boardDTO, string username)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                Board board = unit.BoardRepository.GetById(boardId);

                if (board != null)
                {
                    board.Name = boardDTO.Name;

                    unit.BoardRepository.Update(board);
                    ret = unit.Save();
                    if (ret)
                    {  
                        BoardNotificationService.ChangeBoardNotifications(board.BoardId);

                        BasicBoardDTO dto = new BasicBoardDTO(board);

                        RabbitMQService.PublishToExchange(board.ExchangeName,
                            new MessageContext(new BoardMessageStrategy(dto, MessageType.Update, username)));

                        List<User> users = unit.PermissionRepository.GetAllUsersWithPermissionOnBoard(board.BoardId);

                        foreach (var u in users)
                        {
                            RabbitMQService.PublishToExchange(u.ExchangeName,
                            new MessageContext(new BoardMessageStrategy(dto, MessageType.UserUpdate, username)));
                        }
                    }
                }
            }

            return ret;
        }

        public bool DeleteBoard(int id, string username)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                bool isAdmin = unit.PermissionRepository
                    .IsAdmin(id, username);

                if (isAdmin)
                {
                    List<User> users = unit.PermissionRepository
                        .GetAllUsersWithPermissionOnBoard(id);

                    unit.BoardRepository.Delete(id);
                    ret = unit.Save();

                    if (ret)
                        foreach (var u in users)
                        {
                            RabbitMQService.PublishToExchange(u.ExchangeName,
                                new MessageContext(new BoardMessageStrategy(id)));
                        }

                }
            }

            return ret;
        }

    }
}
