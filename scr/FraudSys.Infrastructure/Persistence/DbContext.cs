using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;

namespace FraudSys.Infrastructure.Persistence
{
	public class DbContext
	{
		private readonly IDynamoDBContext _dbContext;
		private readonly IAmazonDynamoDB _dbClient;

		public DbContext(IAmazonDynamoDB dynamoDbClient)
		{
			_dbClient = dynamoDbClient;

			_dbContext = new DynamoDBContext(_dbClient);
		}

		public IDynamoDBContext GetDbContext()
		{
			return _dbContext;
		}

		public IAmazonDynamoDB GetDbClient()
		{
			return _dbClient;
		}

		public async Task CreateTableIfNotExistsAsync(string tableName)
		{
			var tables = await _dbClient.ListTablesAsync();
			if (tables != null && tables.TableNames.Exists(x => x == tableName))
				return;

			CreateTableRequest request;

			if (tableName == "Account")
			{
				request = new CreateTableRequest
				{
					TableName = tableName,
					AttributeDefinitions = 
					[
						new AttributeDefinition
						{
							AttributeName = "Document",
							AttributeType = "S"
						}
					],
					KeySchema =
					[
						new KeySchemaElement
						{
							AttributeName = "Document",
							KeyType = "HASH"
						}
					],
					ProvisionedThroughput = new ProvisionedThroughput
					{
						ReadCapacityUnits = 5,
						WriteCapacityUnits = 5
					}
				};
			}
			else
			{
				request = new CreateTableRequest
				{
					TableName = tableName,
					AttributeDefinitions =
					[
						new AttributeDefinition
						{
							AttributeName = "Id",
							AttributeType = "S"
						}
					],
					KeySchema =
					[
						new KeySchemaElement
						{
							AttributeName = "Id",
							KeyType = "HASH"
						}
					],
					ProvisionedThroughput = new ProvisionedThroughput
					{
						ReadCapacityUnits = 5,
						WriteCapacityUnits = 5
					}
				};
			}

			await _dbClient.CreateTableAsync(request);
		}
	}
}
