using Bitinvest.Domain.Entities;
using Bitinvest.Domain.Repositories;
using Bitinvest.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Bitinvest.Infra.Repositories
{
    public class RepositoryCliente : Repository<Cliente>, IRepositoryCliente
    {
        public RepositoryCliente(BitInvestDbContext db) : base(db)
        {
        }

        public async Task<Cliente> ObterPorCpf(string cpf)
        {
            var cliente = await DbSet.Where(X => X.Cpf.Numero == cpf).FirstOrDefaultAsync();
            return cliente;
        }
    }
}
