using Bitinvest.Domain.Entities.ContaReais;
using Bitinvest.Domain.Repositories;
using Bitinvest.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Bitinvest.Infra.Repositories
{
    public class RepositoryContaReais : Repository<ContaCorrenteReais>, IRepositoryContaReais
    {
        public RepositoryContaReais(BitInvestDbContext db) : base(db)
        {
        }

        public async Task<decimal> ObterSaldo(Guid clienteId)
        {
            var ultimaMovimentacao = await DbSet.Where(x => x.ClienteId == clienteId)
                .OrderByDescending(x => x.DataMovimento)
                .OrderByDescending(x => x.CriadoEm)
                .FirstOrDefaultAsync();

            decimal saldo = ultimaMovimentacao?.Saldo ?? 0;

            return saldo;
        }
    }
}
