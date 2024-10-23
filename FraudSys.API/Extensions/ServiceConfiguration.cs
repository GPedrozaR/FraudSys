using FraudSys.API.Middleware;
using FraudSys.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

namespace FraudSys.API.Extensions
{
	public static class ServiceConfiguration
	{
		public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddControllers();

			builder.Services.AddSingleton<GlobalExceptionHandler>();

			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddSwaggerGen(s =>
			{
				s.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "FraudSys.API",
					Version = "v1",
					Contact = new OpenApiContact
					{
						Name = "Gabriel Pedroza",
						Email = "gpedrozarodrigues@gmail.com",
						Url = new Uri("https://github.com/GPedrozaR")
					}
				});
			});

			builder.Services.AddInfrastructure();

			return builder;
		}
	}
}
