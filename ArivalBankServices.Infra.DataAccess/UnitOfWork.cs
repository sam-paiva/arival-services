using ArivalBankServices.Core.Base;
using ArivalBankServices.Infra.DataAccess.Context;

namespace ArivalBankServices.Infra.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;
        public UnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public Task CommitAsync()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
