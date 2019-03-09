using planit_data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Repository
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationContext context = new ApplicationContext();
        private bool disposed = false;

        private GenericRepository<User> userRepository;
        private GenericRepository<Board> boardRepository;
        private GenericRepository<Card> cardRepository;
        private GenericRepository<CardList> cardListRepository;
        private GenericRepository<Comment> commentRepository;
        private GenericRepository<Notification> notificationRepository;
        private PermissionRepository permissionRepository;
        private ApplicationUserRepository applicationUserRepository;

        #region Property 
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new GenericRepository<User>(context);
                return userRepository;
            }
        }
        public GenericRepository<Board> BoardRepository
        {
            get
            {
                if (boardRepository == null)
                    boardRepository = new GenericRepository<Board>(context);
                return boardRepository;
            }
        }
        public GenericRepository<Card> CardRepository
        {
            get
            {
                if (cardRepository == null)
                    cardRepository = new GenericRepository<Card>(context);
                return cardRepository;
            }
        }
        public GenericRepository<CardList> CardListRepository
        {
            get
            {
                if (cardListRepository == null)
                    cardListRepository = new GenericRepository<CardList>(context);
                return cardListRepository;
            }
        }
        public GenericRepository<Comment> CommentRepository
        {
            get
            {
                if (commentRepository == null)
                    commentRepository = new GenericRepository<Comment>(context);
                return commentRepository;
            }
        }
        public GenericRepository<Notification> NotificationRepository
        {
            get
            {
                if (notificationRepository == null)
                    notificationRepository = new GenericRepository<Notification>(context);
                return notificationRepository;
            }
        }
        public PermissionRepository PermissionRepository
        {
            get
            {
                if (permissionRepository == null)
                    permissionRepository = new PermissionRepository(context);
                return permissionRepository;
            }
        }

        public ApplicationUserRepository ApplicationUserRepository
        {
            get
            {
                if (applicationUserRepository == null)
                    applicationUserRepository = new ApplicationUserRepository(context);
                return applicationUserRepository;
            }
        }
        #endregion

        public bool Save()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void Dispose()
        {
            if(!this.disposed)
            {
                context.Dispose();
            }
            this.disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
