using Bitinvest.Domain.Entities;

namespace Bitinvest.Domain.Repositories
{
    public interface IRepositoryCliente : IRepository<Cliente>
    {
        public Task<Cliente> ObterPorCpf(string cpf);
    }
}
