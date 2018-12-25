using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using planit_data.DTOs;
using planit_data.Entities;
using planit_data.Repository;
using planit_data.Services;
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
            //ApplicationContext context = new ApplicationContext();

            ////User Ana = new User()
            ////{
            ////    FirstName = "novaAna",
            ////    LastName = "novaAna"
            ////};

            ////context.UserList.Add(Ana);
            ////context.SaveChanges();
            ////int a = 5;
            ////var store = new UserStore<ApplicationUser>(context);
            ////var manager = new UserManager<ApplicationUser>(store);

            ////var user = new ApplicationUser() { Email = "ana@stojanovic", UserName = "ana@stojanovic" };
            ////var usmanger = manager.Create(user, "Ana123!");

            ////User newUser = new User()
            ////{
            ////    FirstName = "Damjan",
            ////    LastName = "Trifunovic",
            ////    IdentificationUser = user
            ////};

            ////context.UserList.Add(newUser);
            ////context.SaveChanges();

            ////User Damjan = context.UserList.Where(x => x.UserId == 2).FirstOrDefault<User>();
            ////Board b = new Board()
            ////{
            ////    Name = "Prvi board"
            ////};

            ////Permission p = new Permission()
            ////{
            ////    IsAdmin = true,
            ////    Board = b,
            ////    User = Damjan
            ////};

            ////context.Permissions.Add(p);
            ////context.SaveChanges();

            ////CardList list = new CardList()
            ////{
            ////    Name = "To do",
            ////    Color ="Blue"
            ////};

            ////list.Board = context.Boards.Where(x => x.BoardId == 1).FirstOrDefault();
            ////context.CardLists.Add(list);
            ////context.SaveChanges();
            //ApplicationContext newContext = new ApplicationContext();

            //CardList list = context.CardLists.Where(x => x.ListId == 1).First();
            //Card card = new Card()
            //{
            //    Name = "Nova nova kartica"
            //};

            //list.Cards.Add(card);

            //context.Entry(list).State = System.Data.Entity.EntityState.Modified;
            //context.SaveChanges();


            ////List<Card> cards = newContext.Cards.ToList();
            //Card c;
            //using (UnitOfWork unit = new UnitOfWork())
            //{
            //    c = unit.CardRepository.GetById(3);
            //    Comment com = new Comment()
            //    {
            //        Text = "Novi komentar",
            //        Card = c
            //    };
            //    unit.CommentRepository.Insert(com);
            //    unit.Save();
            //}
            //int a = 4;

            //UserService service = new UserService();
            //int id = service.InsertUser(new DTOs.CreateUserDTO()
            //{
            //    FirstName = "test",
            //    LastName = "test",
            //    Username = "test123",
            //    Email = "test@gmail.com",
            //    Password = "Test123!"

            //});
            //service.UpdateUser(new DTOs.UpdateUserDTO()
            //{
            //    UserID = 7,
            //    FirstName = "Milica",
            //    LastName = "Todorovic"
            //});

            //List<ReadUserDTO> dto = service.GetAllUsers();

            //BoardService service = new BoardService();
            //service.InsertBoard(new CreateBoardDTO()
            //{
            //    Name = "Milica board",
            //    CreatedByUser = 8
            //});
            // ReadBoardDTO board =  service.GetBoard(2);

            //  CardListService service = new CardListService();
            //service.InsertCardList(new CreateCardListDTO()
            //{
            //    BoardId = 1,
            //    Name = "Test lista"
            //});
            //service.UpdateCardList(new UpdateCardListDTO()
            //{
            //    ListId = 2,
            //    Name = "Test lista",
            //    Color = "Red"
            //});

            //PermissionService p = new PermissionService();
            //p.UpdatePermission(new UpdatePermissionDTO()
            //{
            //    IsAdmin = false,
            //    BoardId = 1,
            //    UserId = 2
            //});
            //service.GetBoardsByUser(8);
            //ReadBoardDTO b = service.GetBoard(1);
            //CardListService c = new CardListService();
            //c.GetCardList(1);
            BoardService bs = new BoardService();
            CardListService cs = new CardListService();
            CardService cards = new CardService();
            CommentService coms = new CommentService();
            NotificationService ns = new NotificationService();
            PermissionService ps = new PermissionService();
            UserService us = new UserService();

            //CreateBoardDTO boarddto = new CreateBoardDTO()
            //{
            //    Name = "TestBoard"
            //};
            //CreateUserDTO createuser = new CreateUserDTO()
            //{
            //    FirstName = "Milica",
            //    LastName = "Todorovic",
            //    Username = "mimi.nish",
            //    Email = "mimi.nish@gmail.com",
            //    Password = "Mimi96hehe!"

            //};
            //int id = us.InsertUser(createuser);
            //boarddto.CreatedByUser = id;
            //bs.InsertBoard(boarddto);

            //CreateCardListDTO cdto = new CreateCardListDTO()
            //{
            //    Name = "Listaa2",
            //    //BoardId = 1,

            //};
            //cs.InsertCardList(cdto);

            //CreateCardDTO card = new CreateCardDTO()
            //{
            //    Name = "Karticaa",
            //    ListId = 1,
            //    UserId = 1
            //};
            //cards.InsertCard(card);

            //CreateCommentDTO com = new CreateCommentDTO()
            //{
            //    Text = "Teeeest",
            //    CardId = 1,
            //    //UserId = 1
            //};
            //coms.InsertComment(com);

            //CreateNotificationDTO notif = new CreateNotificationDTO()
            //{
            //    UserId = 1,
            //    CardId = 1,
            //};
            //ns.CreateNotification(notif);
        }
    }
}
