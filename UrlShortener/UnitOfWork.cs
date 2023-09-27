//namespace UrlShortener.API
//{
//    public class UnitOfWork : IUnitOfWork
//    {
//        protected readonly ApiUserDbContext context;
//       public UnitOfWork() 
//       {
//          //  context = new ApiUserDbContext();
//       }
//        public bool SaveChanges()
//        {
//            bool returnValue = true;
//            using (var dbContextTransaction = context.Database.BeginTransaction())
//            {
//                try
//                {
//                    context.SaveChanges();
//                    dbContextTransaction.Commit();
//                }
//                catch(Exception)
//                {
//                    returnValue= false;
//                    dbContextTransaction.Rollback();
//                }
//            }
//            return returnValue;
//        }

//        private bool _disposedValue = false;
//        protected virtual void Dispose(bool disposing)
//        {
//            if(_disposedValue) return;
//            if (disposing) { }
//            _disposedValue = true;
//        }
//        public void Dispose()
//        {
//            Dispose(true);
//        }
//    }

//    public interface IUnitOfWork : IDisposable
//    {
//        bool SaveChanges();
//    }
   
//}
