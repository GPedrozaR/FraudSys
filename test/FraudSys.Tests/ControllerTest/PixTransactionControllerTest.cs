using FluentAssertions;
using FraudSys.API.Controllers;
using FraudSys.Application.Abstractions;
using FraudSys.Application.InputModels;
using FraudSys.Application.Interfaces;
using FraudSys.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FraudSys.Tests.ControllerTest
{
	public class PixTransactionControllerTest
	{
		private readonly Mock<IPixTransactionService> _serviceMock;
		private readonly PixTransactionsController _controller;
		private readonly HttpClient _client;


		public PixTransactionControllerTest()
		{
			_serviceMock = new Mock<IPixTransactionService>();
			_controller = new PixTransactionsController(_serviceMock.Object);
			_client = new WebApplicationFactory<API.Program>().CreateClient();
		}

		[Fact]
		public async Task CreatePixTransaction_ShouldReturnBadRequest_WhenMockTransactionFails()
		{
			var inputModel = new CreatePixTransactionInputModel
			{
			};

			var serviceResult = Result<PixTransaction>.Fail("Transaction Error");

			_serviceMock.Setup(s => s.MakePixTransaction(inputModel))
				.ReturnsAsync(serviceResult);

			var result = await _controller.CreatePixTransaction(inputModel);

			var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
			badRequestResult.StatusCode.Should().Be(400);
			badRequestResult.Value.Should().Be("Transaction Error");
		}

		[Fact]
		public async Task CreatePixTransaction_ShouldReturnCreated_WhenTransactionIsSuccessful()
		{
			var inputModel = new CreatePixTransactionInputModel
			{
				Document = "11193300410",
				Amount = 1,
			};

			var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");

			var response = await _client.PostAsync("https://localhost:7246/api/PixTransactions", jsonContent);

			response.StatusCode.Should().Be(HttpStatusCode.Created);

			var responseBody = await response.Content.ReadAsStringAsync();
			var transaction = JsonConvert.DeserializeObject<dynamic>(responseBody);

			var success = Guid.TryParse((string)transaction.id, out Guid id);

			success.Should().BeTrue();
		}

		[Fact]
		public async Task CreatePixTransaction_ShouldReturnBadRequest_WhenTransactionFails()
		{
			var inputModel = new CreatePixTransactionInputModel
			{
				Document = "00000000000",
				Amount = 1,
			};

			var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");

			var response = await _client.PostAsync("https://localhost:7246/api/PixTransactions", jsonContent);

			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			var responseBody = await response.Content.ReadAsStringAsync();
			responseBody.Should().Contain("null");
		}
	}
}
