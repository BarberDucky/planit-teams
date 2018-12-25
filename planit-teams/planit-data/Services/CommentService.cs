﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_data.DTOs;
using planit_data.Repository;
using planit_data.Entities;

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

        public bool InsertComment(CreateCommentDTO dto)
        {
            bool success = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Card card = uw.CardRepository.GetById(dto.CardId);
                User user = uw.UserRepository.GetById(dto.UserId);
                if (card == null || card == null) return false;
                Comment comment = CreateCommentDTO.FromDTO(dto);
                comment.Card = card;
                comment.User = user;
                success = uw.CommentRepository.Insert(comment);
                uw.Save();
            }
            return success;
        }

        public bool UpdateComment(UpdateCommentDTO dto)
        {
            bool ret = false;
            using (UnitOfWork uw = new UnitOfWork())
            {
                Comment com = UpdateCommentDTO.FromDTO(dto);
                uw.CommentRepository.Update(com);
                ret = uw.Save();
            }
            return ret;
        }

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


    }
}
