using FraudSys.Application.Abstractions;
using FraudSys.Application.InputModels;
using FraudSys.Application.Interfaces;
using FraudSys.Domain.Entities;
using FraudSys.Domain.Repositories;

namespace FraudSys.Application.Sevices
{
	public class AccountService : IAccountService
	{
		private readonly IAccountRepository _accountRepository;

		public AccountService(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}

		public async Task<Result<Account>> GetAccountByDocumentAsync(string document)
		{
			var account = await _accountRepository.GetAccountByDocumentAsync(document);
			return account != null
				? Result<Account>.Ok(account)
				: Result<Account>.Fail("Account not found.");
		}

		public async Task<Result<IEnumerable<Account>>> GetAllAccountsAsync()
		{
			var accounts = await _accountRepository.GetAllAccountsAsync();
			return Result<IEnumerable<Account>>.Ok(accounts);
		}

		public async Task<Result<Account>> SaveAccountAsync(CreateOrUpdateAccountInputModel accountInputModel)
		{
			if (accountInputModel == null)
				return Result<Account>.Fail("Account cannot be null.");

			var account = new Account(
				accountInputModel.Document,
				accountInputModel.BranchNumber,
				accountInputModel.Number,
				accountInputModel.PixTransactionLimit);

			await _accountRepository.SaveAccountAsync(account);
			return Result<Account>.Ok(account);
		}

		public async Task<ResultBase> UpdateAccountAsync(string document, CreateOrUpdateAccountInputModel accountInputModel)
		{
			var existingAccount = await _accountRepository.GetAccountByDocumentAsync(document);
			if (existingAccount == null)
				return ResultBase.Fail("Account not found.");

			existingAccount.SetBranchNumber(accountInputModel.BranchNumber);
			existingAccount.SetNumber(accountInputModel.Number);
			existingAccount.SetPixTransactionLimit(accountInputModel.PixTransactionLimit);

			await _accountRepository.UpdateAccountAsync(document, existingAccount);
			return ResultBase.Ok();
		}

		public async Task<ResultBase> DeleteAccountAsync(string document)
		{
			var existingAccount = await _accountRepository.GetAccountByDocumentAsync(document);
			if (existingAccount == null)
				return ResultBase.Fail("Account not found.");

			await _accountRepository.DeleteAccountAsync(document);
			return ResultBase.Ok();
		}
	}
}
