using System.ComponentModel.DataAnnotations;

namespace FraudSys.Application.InputModels
{
	public class CreatePixTransactionInputModel
	{

		[Required]
		public string Document { get; set; }

		[Required]
		public decimal Amount { get; set; }
	}
}
