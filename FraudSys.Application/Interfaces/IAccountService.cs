using FraudSys.Application.Abstractions;
using FraudSys.Application.InputModels;
using FraudSys.Domain.Entities;

namespace FraudSys.Application.Interfaces
{
	public interface IAccountService
	{
		Task<ResultBase> DeleteAccountAsync(string document);
		Task<ResultBase> UpdateAccountAsync(string document, CreateOrUpdateAccountInputModel account);
		Task<Result<Account>> SaveAccountAsync(CreateOrUpdateAccountInputModel accountInputModel);
		Task<Result<Account>> GetAccountByDocumentAsync(string document);
	}
}
