using Bitinvest.Domain.Entities.ContaReais;

namespace Bitinvest.Domain.Repositories
{
    public interface IRepositoryContaReais : IRepository<ContaCorrenteReais>
    {
        public Task<decimal> ObterSaldo(Guid clienteId);
    }
}
