using planit_data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Repository
{
    public class CardRepository : GenericRepository<Card>
    {
        public CardRepository(ApplicationContext context) 
            : base(context)
        {
        }

        public void Delete(int boardId, User user)
        {
            List<Card> cardsOnBoard = set.Where(c => c.List.Board.BoardId == boardId)
                .ToList();

            foreach(var c in cardsOnBoard)
            {
                c.ObserverUsers.Remove(user);
            }
        }
    }
}
