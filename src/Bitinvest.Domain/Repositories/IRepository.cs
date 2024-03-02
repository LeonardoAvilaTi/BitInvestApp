using Bitinvest.Domain.Entities;

namespace Bitinvest.Domain.Repositories
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(T t);
        Task Atualizar(Guid id,T t);
        Task Remover(Guid id);
        Task<T> ObterPorId(Guid id);
        Task<IEnumerable<T>> ObterTodos();
    }
}
