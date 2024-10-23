using Amazon;
using Amazon.DynamoDBv2;
using FraudSys.Domain.Entities;
using FraudSys.Infrastructure.Persistence;
using FraudSys.Infrastructure.Persistence.Repositories;

namespace FraudSys.Tests
{
	public class InfrastructureTest
	{

		private readonly AccountDbContext _context;
		public InfrastructureTest()
		{
			var accessKey = "accessKey";
			var secretKey = "secretKey";
			var region = RegionEndpoint.USEast2;

			_context = new AccountDbContext(new AmazonDynamoDBClient(accessKey, secretKey, region));

		}

		[Fact]
		public async Task TestDynamoDbConnection()
		{
			try
			{
				var response = await _context.GetDbClient().ListTablesAsync();

				Assert.NotNull(response);
				Assert.True(response.TableNames.Count > 0);
			}
			catch (Exception ex)
			{
				Assert.Null(ex);
			}
		}

		[Fact]
		public async Task TestDynamoDbConnection2()
		{
			try
			{
				await _context.CreateTableIfNotExistsAsync("Account");

				var test = new Account("1111", "11111", "111", 1000);

				var repository = new AccountRepository(_context);
				await repository.SaveAccountAsync(test);
			}
			catch (Exception ex)
			{
				Assert.Null(ex);
			}
		}
	}
}
