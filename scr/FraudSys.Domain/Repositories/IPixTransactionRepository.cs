using FraudSys.Domain.Entities;

namespace FraudSys.Domain.Repositories
{
	public interface IPixTransactionRepository
	{
		Task SavePixTransactionAsync(PixTransaction pixTransaction);
	}
}
