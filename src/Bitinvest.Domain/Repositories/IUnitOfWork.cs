namespace Bitinvest.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
