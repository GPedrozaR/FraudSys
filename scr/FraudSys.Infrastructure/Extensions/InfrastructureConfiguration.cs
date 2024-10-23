using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using FraudSys.Application.Interfaces;
using FraudSys.Application.Sevices;
using FraudSys.Domain.Repositories;
using FraudSys.Infrastructure.Persistence;
using FraudSys.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FraudSys.Infrastructure.Extensions
{
	public static class InfrastructureConfiguration
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


			services.AddScoped<IAccountRepository, AccountRepository>();

			services.AddScoped<IAccountService, AccountService>();

			services.AddSingleton<AccountDbContext>();

			return services;
		}

	}
}
