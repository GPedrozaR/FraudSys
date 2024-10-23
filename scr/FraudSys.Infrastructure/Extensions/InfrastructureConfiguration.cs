using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using FraudSys.Application.Interfaces;
using FraudSys.Application.Services;
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
				var accessKey = "";
				var secretKey = "";
				var region = RegionEndpoint.SAEast1;

				return new AmazonDynamoDBClient(accessKey, secretKey, region);
			});

			services.AddSingleton<IDynamoDBContext, DynamoDBContext>(sp =>
			{
				var client = sp.GetRequiredService<IAmazonDynamoDB>();
				return new DynamoDBContext(client);
			});


			services.AddScoped<IAccountRepository, AccountRepository>();

			services.AddScoped<IPixTransactionRepository, PixTransactionRepository>();

			services.AddScoped<IAccountService, AccountService>();

			services.AddScoped<IPixTransactionService, PixTransactionService>();

			services.AddSingleton<DbContext>();

			return services;
		}

	}
}
