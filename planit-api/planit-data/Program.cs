using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using planit_data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationContext context = new ApplicationContext();

            //User Ana = new User()
            //{
            //    FirstName = "novaAna",
            //    LastName = "novaAna"
            //};

            //context.UserList.Add(Ana);
            //context.SaveChanges();
            //int a = 5;
            //var store = new UserStore<ApplicationUser>(context);
            //var manager = new UserManager<ApplicationUser>(store);

            //var user = new ApplicationUser() { Email = "ana@stojanovic", UserName = "ana@stojanovic" };
            //var usmanger = manager.Create(user, "Ana123!");

            //User newUser = new User()
            //{
            //    FirstName = "Damjan",
            //    LastName = "Trifunovic",
            //    IdentificationUser = user
            //};

            //context.UserList.Add(newUser);
            //context.SaveChanges();

            //User Damjan = context.UserList.Where(x => x.UserId == 2).FirstOrDefault<User>();
            //Board b = new Board()
            //{
            //    Name = "Prvi board"
            //};

            //Permission p = new Permission()
            //{
            //    IsAdmin = true,
            //    Board = b,
            //    User = Damjan
            //};

            //context.Permissions.Add(p);
            //context.SaveChanges();

            //CardList list = new CardList()
            //{
            //    Name = "To do",
            //    Color ="Blue"
            //};

            //list.Board = context.Boards.Where(x => x.BoardId == 1).FirstOrDefault();
            //context.CardLists.Add(list);
            //context.SaveChanges();

            CardList list = context.CardLists.Where(x => x.ListId == 1).First();
            Card card = new Card()
            {
                Name = "Kartice"
            };

            list.Cards.Add(card);

            context.Entry(list).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
                

        }
    }
}
