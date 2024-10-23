using FraudSys.Domain.Entities;
using FraudSys.Domain.Repositories;

namespace FraudSys.Infrastructure.Persistence.Repositories
{
	public class PixTransactionRepository : IPixTransactionRepository
	{
		private readonly DbContext _dbContext;
		public PixTransactionRepository(DbContext context)
		{
			_dbContext = context;
		}

		public async Task SavePixTransactionAsync(PixTransaction pixTransaction)
		{
			await _dbContext.CreateTableIfNotExistsAsync(nameof(PixTransaction));
			await _dbContext.GetDbContext().SaveAsync(pixTransaction);
		}
	}
}
