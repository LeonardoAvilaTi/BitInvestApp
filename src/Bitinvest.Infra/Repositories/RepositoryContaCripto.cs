using Bitinvest.Domain.Entities.ContaCripto;
using Bitinvest.Domain.Repositories;
using Bitinvest.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Bitinvest.Infra.Repositories
{
    public class RepositoryContaCripto : Repository<ContaCripto>, IRepositoryContaCripto
    {
        public RepositoryContaCripto(BitInvestDbContext db) : base(db)
        {
        }

        public async Task<decimal> ObterSaldo(Guid clienteId, string criptoMoeda)
        {
            var ultimaMovimentacao = await DbSet.Where(x => x.ClienteId == clienteId && x.CriptoMoeda == criptoMoeda)
                .OrderByDescending(x => x.DataMovimento)
                .OrderByDescending(x => x.CriadoEm)
                .FirstOrDefaultAsync();

            decimal saldo = ultimaMovimentacao?.Saldo ?? 0;

            return saldo;
        }
    }
}
