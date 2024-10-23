namespace FraudSys.Domain.Entities
{
	public class PixTransaction
	{
		public PixTransaction() { }
		public PixTransaction(string document, decimal amount)
		{
			Id = Guid.NewGuid().ToString();
			Document = document;
			Amount = amount;
		}

		public string Id { get; private set; }
		public string Document { get; private set; }
		public decimal Amount { get; private set; }
	}
}
