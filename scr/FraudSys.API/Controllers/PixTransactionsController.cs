using FraudSys.Application.InputModels;
using FraudSys.Application.Interfaces;
using FraudSys.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FraudSys.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PixTransactionsController : ControllerBase
	{
		private readonly IPixTransactionService _service;
		public PixTransactionsController(IPixTransactionService service)
		{
			_service = service;
		}

		[HttpPost]
		[ProducesResponseType(typeof(PixTransaction), 201)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreatePixTransaction([FromBody] CreatePixTransactionInputModel pixTransactionInputModel)
		{
			var result = await _service.MakePixTransaction(pixTransactionInputModel);

			return !result.IsSuccess
				? BadRequest(result.Error)
				: CreatedAtAction(nameof(CreatePixTransaction), result.Value);
		}
	}
}
