using FraudSys.Application.Abstractions;
using FraudSys.Application.InputModels;
using FraudSys.Domain.Entities;

namespace FraudSys.Application.Interfaces
{
	public interface IPixTransactionService
	{
		Task<Result<PixTransaction>> MakePixTransaction(CreatePixTransactionInputModel pixTransactionInputModel);
	}
}
