namespace ArivalBankServices.Core.Base
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}
