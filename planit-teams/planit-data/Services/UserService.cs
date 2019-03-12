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
    public class UserService
    {
        #region Should Delete
        public List<ReadUserDTO> GetAllUsers()
        {
            List<ReadUserDTO> dtos;
            using (UnitOfWork unit = new UnitOfWork())
            {
                List<User> users = unit.UserRepository.GetAll();
                dtos = ReadUserDTO.FromEntityList(users);
            }
            return dtos;
        }

        public bool DeleteUser(string username)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.UserRepository.Delete(username);
                ret = unit.Save();
            }

            return ret;
        }
        #endregion

        public ReadUserDTO GetUser(string username)
        {
            ReadUserDTO userDTO = null;
            using (UnitOfWork unit = new UnitOfWork())
            {
                User u = unit.UserRepository.GetUserByUsername(username);

                if (u != null)
                {
                    userDTO = new ReadUserDTO(u);
                }

            }

            return userDTO;
        }

        public List<ReadNotificationDTO> GetUserNotifications(string username)
        {
            List<ReadNotificationDTO> list = null;
            using (UnitOfWork uw = new UnitOfWork())
            {
                User u = uw.UserRepository.GetUserByUsername(username);

                if (u != null)
                {
                    list = ReadNotificationDTO.FromList(u.Notifications.ToList());
                }
            }
            return list;
        }

        //Ako se nije kreirao user Id ce biti 0
        public int InsertUser(CreateUserDTO userDTO)
        {
            User newUser = userDTO.FromDTO();
            newUser.ExchangeName = $"user{Guid.NewGuid()}";

            using (UnitOfWork unit = new UnitOfWork())
            {
                ApplicationUser identityUser = unit.
                    ApplicationUserRepository.AddApplicationUser(userDTO.Username, userDTO.Email, userDTO.Password);
                if (identityUser != null)
                {
                    newUser.IdentificationUser = identityUser;
                    unit.UserRepository.Insert(newUser);
                    if (unit.Save())
                    {
                        RabbitMQ.RabbitMQService.DeclareExchange(newUser.ExchangeName);
                    }
                }

            }
            return newUser.UserId;
        }

        public bool UpdateUser(UpdateUserDTO userDTO, string username)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                User user = unit.UserRepository.GetUserByUsername(username);
                if (user != null)
                {
                    user.FirstName = userDTO.FirstName;
                    user.LastName = userDTO.LastName;
                    unit.UserRepository.Update(user);
                    ret = unit.Save();
                }

            }

            return ret;
        }
       
    }
}
