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
        public ReadUserDTO GetUser(int id)
        {
            ReadUserDTO userDTO = null;
            using (UnitOfWork unit = new UnitOfWork())
            {
                User u = unit.UserRepository.GetById(id);

                if (u != null)
                {
                    userDTO = new ReadUserDTO(u);
                }

            }

            return userDTO;
        }

        public List<ReadNotificationDTO> GetUserNotifications(int userId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                User u = uw.UserRepository.GetById(userId);

                if (u == null)
                    return null;

                return ReadNotificationDTO.FromList(u.Notifications.ToList());
            }
        }

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

        //Ako se nije kreirao user Id ce biti 0
        public int InsertUser(CreateUserDTO userDTO)
        {
            User newUser = userDTO.FromDTO();

            using (UnitOfWork unit = new UnitOfWork())
            {
                ApplicationUser identityUser = unit.
                    ApplicationUserRepository.AddApplicationUser(userDTO.Username, userDTO.Email, userDTO.Password);
                if (identityUser != null)
                {
                    newUser.IdentificationUser = identityUser;
                    unit.UserRepository.Insert(newUser);
                    unit.Save();
                }

            }
            return newUser.UserId;
        }

        public bool UpdateUser(UpdateUserDTO userDTO)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                User user = unit.UserRepository.GetById(userDTO.UserID);
                user.FirstName = userDTO.FirstName;
                user.LastName = userDTO.LastName;

                unit.UserRepository.Update(user);
                ret = unit.Save();
            }

            return ret;
        }

        public bool DeleteUser(int id)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.UserRepository.Delete(id);
                ret = unit.Save();
            }

            return ret;
        }


    }
}
