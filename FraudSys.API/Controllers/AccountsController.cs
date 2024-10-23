using FraudSys.Application.InputModels;
using FraudSys.Application.Interfaces;
using FraudSys.Application.Sevices;
using FraudSys.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FraudSys.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountsController : ControllerBase
	{

		private readonly IAccountService _service;
		public AccountsController(IAccountService service)
		{
			_service = service;
		}

		[HttpPost]
		[ProducesResponseType(typeof(Account), 201)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateAccount([FromBody] CreateOrUpdateAccountInputModel accountInputModel)
		{
			var result = await _service.SaveAccountAsync(accountInputModel);
			
			return !result.IsSuccess
				? BadRequest(result.Error)
				: CreatedAtAction(nameof(GetAccountByDocument), new { document = accountInputModel.Document }, null);
		}

		[HttpGet("{document}")]
		[ProducesResponseType(typeof(Account), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetAccountByDocument(string document)
		{
			var result = await _service.GetAccountByDocumentAsync(document);

			return !result.IsSuccess
				? NotFound(result.Error)
				: Ok(result);
		}

		[HttpPut("{document}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> UpdateAccount(string document, [FromBody] CreateOrUpdateAccountInputModel accountInputModel)
		{
			var result = await _service.UpdateAccountAsync(document, accountInputModel);

			if (!result.IsSuccess)
			{
				return result.Error == "Account not found." 
					? NotFound(result.Error) 
					: BadRequest(result.Error);
			}

			return NoContent();
		}

		[HttpDelete("{document}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> DeleteAccount(string document)
		{
			var result = await _service.DeleteAccountAsync(document);

			return !result.IsSuccess 
				? NotFound(result.Error) 
				: NoContent();
		}
	}
}
