using FraudSys.Domain.Entities;

namespace FraudSys.Domain.Repositories
{
	public interface IAccountRepository
	{
		Task<Account> GetAccountByDocumentAsync(string document);
		Task<IEnumerable<Account>> GetAllAccountsAsync();
		Task SaveAccountAsync(Account account);
		Task UpdateAccountAsync(string document, Account account);
		Task DeleteAccountAsync(string document);
	}
}
