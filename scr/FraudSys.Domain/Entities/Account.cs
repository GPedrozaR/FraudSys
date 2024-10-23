namespace FraudSys.Domain.Entities
{
	public class Account
	{
		public Account() { }

		public Account(
			string document, 
			string branchNumber, 
			string number, 
			decimal pixTransactionLimit)
		{
			Document = document;
			BranchNumber = branchNumber;
			Number = number;
			PixTransactionLimit = pixTransactionLimit;
		}

		public string Document { get; private set; }
		public string BranchNumber { get; private set; }
		public string Number { get; private set; }
		public decimal PixTransactionLimit { get; private set; }

		public void SetBranchNumber(string branchNumber)
		{
			if (string.IsNullOrWhiteSpace(branchNumber))
				throw new ArgumentException("Branch number cannot be null or empty.");

			BranchNumber = branchNumber;
		}

		public void SetNumber(string number)
		{
			if (string.IsNullOrWhiteSpace(number))
				throw new ArgumentException("Account number cannot be null or empty.");

			Number = number;
		}

		public void SetPixTransactionLimit(decimal limit)
		{
			if (limit < 0)
				throw new ArgumentException("PIX transaction limit cannot be negative.");

			PixTransactionLimit = limit;
		}
	}
}
