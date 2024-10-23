using Amazon.DynamoDBv2.DataModel;
using FraudSys.Domain.Entities;
using FraudSys.Domain.Repositories;

namespace FraudSys.Infrastructure.Persistence.Repositories
{
	public class AccountRepository : IAccountRepository
	{

		private readonly AccountDbContext _dbContext;
		public AccountRepository(AccountDbContext context)
		{
			_dbContext = context;
		}

		public async Task DeleteAccountAsync(string document)
		{
			await _dbContext.CreateTableIfNotExistsAsync(nameof(Account));

			await _dbContext.GetDbContext().DeleteAsync(document);
		}

		public async Task<Account> GetAccountByDocumentAsync(string document)
		{
			await _dbContext.CreateTableIfNotExistsAsync(nameof(Account));

			return await _dbContext.GetDbContext().LoadAsync<Account>(document);
		}

		public async Task<IEnumerable<Account>> GetAllAccountsAsync()
		{
			await _dbContext.CreateTableIfNotExistsAsync(nameof(Account));

			var scanConditions = new List<ScanCondition>();

			var accounts = await _dbContext.GetDbContext().ScanAsync<Account>(scanConditions).GetRemainingAsync();

			return accounts;
		}

		public async Task SaveAccountAsync(Account account)
		{
			await _dbContext.CreateTableIfNotExistsAsync(nameof(Account));

			await _dbContext.GetDbContext().SaveAsync(account);
		}

		public async Task UpdateAccountAsync(string document, Account account)
		{
			var accountInDb = await GetAccountByDocumentAsync(document);
			if (accountInDb != null)
			{
				accountInDb.SetBranchNumber(account.BranchNumber);
				accountInDb.SetNumber(account.Number);
				accountInDb.SetPixTransactionLimit(account.PixTransactionLimit);
				accountInDb.SetBranchNumber(account.BranchNumber);

				await _dbContext.GetDbContext().SaveAsync(accountInDb);
			}
		}
	}
}
