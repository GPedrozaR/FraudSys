using FraudSys.Application.Abstractions;
using FraudSys.Application.InputModels;
using FraudSys.Application.Interfaces;
using FraudSys.Domain.Entities;
using FraudSys.Domain.Repositories;

namespace FraudSys.Application.Services
{
	public class PixTransactionService : IPixTransactionService
	{
		private readonly IPixTransactionRepository _pixTransactionRepository;
		private readonly IAccountRepository _accountRepository;

		public PixTransactionService(IPixTransactionRepository pixTransactionRepository, IAccountRepository accountRepository)
		{
			_pixTransactionRepository = pixTransactionRepository;
			_accountRepository = accountRepository;
		}

		public async Task<Result<PixTransaction>> MakePixTransaction(CreatePixTransactionInputModel pixTransactionInputModel)
		{
			if (pixTransactionInputModel == null)
				return Result<PixTransaction>.Fail("PixTransaction cannot be null.");

			var account = await _accountRepository.GetAccountByDocumentAsync(pixTransactionInputModel.Document);
			if (account == null)
				return Result<PixTransaction>.Fail("Account cannot be null.");

			if (pixTransactionInputModel.Amount > account.PixTransactionLimit)
				return Result<PixTransaction>.Fail("Pix transaction limit exceeded.");

			var pixTransaction = new PixTransaction(
				pixTransactionInputModel.Document,
				pixTransactionInputModel.Amount);

			await _pixTransactionRepository.SavePixTransactionAsync(pixTransaction);

			account.SetPixTransactionLimit(account.PixTransactionLimit - pixTransactionInputModel.Amount);

			await _accountRepository.UpdateAccountAsync(account.Document, account);

			return Result<PixTransaction>.Ok(pixTransaction);
		}
	}
}
