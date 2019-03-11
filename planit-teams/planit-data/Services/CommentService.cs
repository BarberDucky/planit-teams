using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_data.DTOs;
using planit_data.Repository;
using planit_data.Entities;
using planit_data.RabbitMQ;

namespace planit_data.Services
{
    public class CommentService
    {
        public ReadCommentDTO GetCommentById(int id)
        {
            ReadCommentDTO dto;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Comment comment = uw.CommentRepository.GetById(id);
                if (comment == null) return null;
                dto = new ReadCommentDTO(comment);
            }
            return dto;
        }

        //Treba get all za 1 karticu
        public List<ReadCommentDTO> GetAllComments()
        {
            List<ReadCommentDTO> listDTO;
            using (UnitOfWork uw = new UnitOfWork())
            {
                List<Comment> retList = uw.CommentRepository.GetAll();
                listDTO = ReadCommentDTO.FromEntityList(retList);
            }
            return listDTO;
        }

        public int InsertComment(CreateCommentDTO dto)
        {
            Comment comment = CreateCommentDTO.FromDTO(dto);
            using (UnitOfWork uw = new UnitOfWork())
            {
                Card card = uw.CardRepository.GetById(dto.CardId);
                User user = uw.UserRepository.GetById(dto.UserId);
                if (card != null && user != null)
                {
                    comment.Card = card;
                    comment.User = user;
                    uw.CommentRepository.Insert(comment);

                    if (uw.Save())
                    {
                        NotificationService notif = new NotificationService();
                        notif.CreateChangeNotification(new CreateNotificationDTO()
                        {
                            CardId = dto.CardId,
                            UserId = dto.UserId,
                            NotificationType = NotificationType.Change
                        });

                        BasicCommentDTO commentDTO = new BasicCommentDTO(comment);
                        RabbitMQService.PublishToExchange(card.List.Board.ExchangeName,
                            new MessageContext(new CommentMessageStrategy(commentDTO)));

                        BoardNotificationService.ChangeBoardNotifications(card.List.Board.BoardId);
                    }

                }
               
            }
            return comment.CommentId;
        }

        #region Should Delete
        //TODO Zasto smo stavili da moze da se edituje komentar?? 
        public bool UpdateComment(UpdateCommentDTO dto)
        {
            bool ret = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Comment c = uw.CommentRepository.GetById(dto.CommentId);
                if (c != null)
                {
                    c.Text = dto.Text;
                    uw.CommentRepository.Update(c);
                    ret = uw.Save();
                }
            }
            return ret;
        }

        //TODO Zasto smo stavili da se brise komentar??
        public bool DeleteComment(int id)
        {
            bool success = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                uw.CommentRepository.Delete(id);
                success = uw.Save();
            }
            return success;
        }
        #endregion
    }
}
