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
    public class PermissionService
    {
        public bool UpdatePermission(UpdatePermissionDTO permisionDTO)
        {
            bool ret = false;

            using (UnitOfWork unit = new UnitOfWork())
            {
                List<Permission> p = unit.PermissionRepository
                    .Get(x => (x.User.UserId == permisionDTO.UserId) && (x.Board.BoardId == permisionDTO.BoardId)).ToList();
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

        public bool AddUserBoardPermision(AddUserBoardPermisionDTO dto)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                Board b = unit.BoardRepository.GetById(dto.BoardId);
                User u = unit.UserRepository.GetById(dto.UserId);

                if(u!=null && b!=null)
                {
                    Permission p = new Permission()
                    {
                        Board = b,
                        User = u
                    };

                    unit.PermissionRepository.Insert(p);
                    ret = unit.Save();
                    if(ret)
                    {
                        string message = $"User dodat na board {b.BoardId}";
                        RabbitMQ.RabbitMQService.PublishToExchange(u.ExchangeName, message);
                    }
                }
            }

            return ret;
        }
    }
}
