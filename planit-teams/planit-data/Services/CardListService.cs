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
    public class CardListService
    {
        public ReadCardListDTO GetCardList(int id)
        {
            ReadCardListDTO cardListDTO = null;
            using (UnitOfWork unit = new UnitOfWork())
            {
                CardList c = unit.CardListRepository.GetById(id);
                if(c!=null)
                {
                    cardListDTO = new ReadCardListDTO(c);
                }
 
            }

            return cardListDTO;
        }

        public int InsertCardList(CreateCardListDTO cardListDto)
        {
            CardList list = cardListDto.FromDTO();
            using (UnitOfWork unit = new UnitOfWork())
            {
                Board board = unit.BoardRepository.GetById(cardListDto.BoardId);
                list.Board = board;

                unit.CardListRepository.Insert(list);
                unit.Save();
            }

            return list.ListId;
        }

        public bool UpdateCardList(UpdateCardListDTO cardListDTO)
        {
            bool ret;
            using (UnitOfWork unit = new UnitOfWork())
            {
                CardList cardList = unit.CardListRepository.GetById(cardListDTO.ListId);
                cardList.Name = cardListDTO.Name;
                cardList.Color = cardListDTO.Color;

                unit.CardListRepository.Update(cardList);
                ret = unit.Save();
            }

            return ret;
        }

        public bool DeleteCardList(int id)
        {
            bool ret = false;
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.CardListRepository.Delete(id);
                ret = unit.Save();
            }

            return ret;
        }
    }
}
