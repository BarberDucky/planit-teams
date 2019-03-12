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
    public class PermissionService
    {
        #region Should Delete
        public bool UpdatePermission(UpdatePermissionDTO permisionDTO)
        {
            bool ret = false;

            using (UnitOfWork unit = new UnitOfWork())
            {
                List<Permission> p = unit.PermissionRepository
                    .Get(x => (x.User.UserId == permisionDTO.UserId)
                    && (x.Board.BoardId == permisionDTO.BoardId)).ToList();
                if (p.Count > 0)
                {
                    Permission per = p[0];
                    per.IsAdmin = permisionDTO.IsAdmin;
                    unit.PermissionRepository.Update(per);
                    ret = unit.Save();
                }
            }

            return ret;
        }
        #endregion

        //MOGUCE DA OVE 3 METODE NISU POTREBNE
        public bool GetPermissionOnCard(int cardId, int userId)
        {
            bool ret = false;

            using (UnitOfWork unit = new UnitOfWork())
            {
                Card list = unit.CardRepository.GetById(cardId);

                if (list != null)
                {
                    List<Permission> p = unit.PermissionRepository
                    .Get(x => (x.User.UserId == userId) && (x.Board.BoardId == list.List.Board.BoardId)).ToList();
                    if (p.Count > 0)
                    {
                        ret = true;
                    }
                }

            }

            return ret;
        }

        public bool GetPermission(int boardId, int userId)
        {
            bool ret = false;

            using (UnitOfWork unit = new UnitOfWork())
            {
                List<Permission> p = unit.PermissionRepository
                    .Get(x => (x.User.UserId == userId) && (x.Board.BoardId == boardId)).ToList();
                if (p.Count > 0)
                {
                    ret = true;
                }
            }

            return ret;
        }

        public bool GetPermissionOnList(int listId, int userId)
        {
            bool ret = false;

            using (UnitOfWork unit = new UnitOfWork())
            {
                CardList list = unit.CardListRepository.GetById(listId);

                if (list != null)
                {
                    List<Permission> p = unit.PermissionRepository
                    .Get(x => (x.User.UserId == userId) && (x.Board.BoardId == list.Board.BoardId)).ToList();
                    if (p.Count > 0)
                    {
                        ret = true;
                    }
                }

            }

            return ret;
        }

        public bool AddUserBoardPermision(AddUserBoardPermisionDTO dto, string admin)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                bool isAdmin = unit.PermissionRepository.IsAdmin(dto.BoardId, admin);

                if (isAdmin)
                {
                    Board b = unit.BoardRepository.GetById(dto.BoardId);
                    User u = unit.UserRepository.GetUserByUsername(dto.Username);

                    if (u != null && b != null)
                    {
                        Permission p = new Permission()
                        {
                            Board = b,
                            User = u
                        };

                        BoardNotification boardNotification = new BoardNotification()
                        {
                            Board = b,
                            User = u
                        };

                        unit.PermissionRepository.Insert(p);
                        unit.BoardNotificationRepository.Insert(boardNotification);
                        ret = unit.Save();

                        if (ret)
                        {
                            RabbitMQService.PublishToExchange(u.ExchangeName,
                                new MessageContext(new BoardMessageStrategy(new BasicBoardDTO(b),
                                MessageType.Create)));
                        }

                    }
                }
            }

            return ret;
        }

        //TODO Treba ovde obrisati tog usera i iz svih kartica koje watchuje
        public bool DeletePermission(int boardId, string username, string admin)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                bool isAdmin = unit.PermissionRepository
                    .IsAdmin(boardId, admin);

                if (isAdmin && admin != username)
                {
                    Permission perm = unit.PermissionRepository.GetPermissionByUsername(boardId, username);
                    User user = perm.User;
                    unit.PermissionRepository.Delete(perm.PermissionId);
                    unit.BoardNotificationRepository.Delete(boardId, user.UserId);

                    ret = unit.Save();

                    if (ret)
                    {
                        RabbitMQService.PublishToExchange(user.ExchangeName,
                            new MessageContext(new BoardMessageStrategy(boardId)));
                    }
                }

            }

            return ret;
        }
    }
}
