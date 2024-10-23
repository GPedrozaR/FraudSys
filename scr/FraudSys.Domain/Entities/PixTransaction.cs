namespace FraudSys.Domain.Entities
{
	public class PixTransaction
	{
		public PixTransaction(Guid id, string document, decimal amount)
		{
			Id = id;
			Document = document;
			Amount = amount;
		}

		public Guid Id { get; private set; }
        public string Document { get; private set; }
        public decimal Amount { get; private set; }
    }
}
