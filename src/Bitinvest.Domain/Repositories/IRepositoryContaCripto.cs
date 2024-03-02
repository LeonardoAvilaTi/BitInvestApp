using Bitinvest.Domain.Entities.ContaCripto;

namespace Bitinvest.Domain.Repositories
{
    public interface IRepositoryContaCripto : IRepository<ContaCripto>
    {
        public Task<decimal> ObterSaldo(Guid clienteId, string criptoMoeda);

    }
}
