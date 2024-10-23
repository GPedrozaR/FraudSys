using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.DependencyInjection;

namespace FraudSys.Infrastructure.Extensions
{
	public static class InfrastructureExtensions
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
			services.AddSingleton<IAmazonDynamoDB>(sp =>
			{
				var accessKey = "accessKey";
				var secretKey = "secretKey";
				var region = RegionEndpoint.SAEast1;

				return new AmazonDynamoDBClient(accessKey, secretKey, region);
			});

			services.AddSingleton<IDynamoDBContext, DynamoDBContext>(sp =>
			{
				var client = sp.GetRequiredService<IAmazonDynamoDB>();
				return new DynamoDBContext(client);
			});

			return services;
		}

	}
}
