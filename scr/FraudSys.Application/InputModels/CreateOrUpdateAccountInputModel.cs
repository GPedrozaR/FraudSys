using System.ComponentModel.DataAnnotations;

namespace FraudSys.Application.InputModels
{
	public class CreateOrUpdateAccountInputModel
	{

		[Required]
		public string Document { get; set; }

		[Required]
		public string BranchNumber { get; set; }

		[Required]
		public string Number { get; set; }

		[Required]
		public decimal PixTransactionLimit { get; set; }
	}
}
