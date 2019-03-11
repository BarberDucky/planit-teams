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
    //TODO Obrisati permission
    public class PermissionService
    {
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

        //TODO Publish na notif kanal
        public bool AddUserBoardPermision(AddUserBoardPermisionDTO dto)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                Board b = unit.BoardRepository.GetById(dto.BoardId);
                User u = unit.UserRepository.GetById(dto.UserId);

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

        public bool DeletePermission(int boardId, int userId)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                Permission perm = unit.PermissionRepository.GetPermission(boardId, userId);
                User user = unit.UserRepository.GetById(userId);
                unit.PermissionRepository.Delete(perm.PermissionId);
                ret = unit.Save();

                if(ret)
                {
                    RabbitMQService.PublishToExchange(user.ExchangeName,
                        new MessageContext(new BoardMessageStrategy(boardId)));
                }
            }

            return ret;
        }
    }
}
