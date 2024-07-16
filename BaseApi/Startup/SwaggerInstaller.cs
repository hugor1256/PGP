using Microsoft.OpenApi.Models;
using PGP.Startup.Base;

namespace PGP.Startup;

public class SwaggerInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            OpenApiInfo info = new OpenApiInfo()
            {
                Title = configuration.GetSection("Swagger:Info:Title").Value,
                Version = configuration.GetSection("Swagger:Info:Version").Value,
                Description = configuration.GetSection("Swagger:Info:Description").Value,
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(info.Version, info);

                c.CustomSchemaIds(i => i.FullName);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autorização JWT no cabeçalho usando esquema Bearer. Exemplo: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            //Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                c.AddSecurityDefinition("Custom-Authorization", new OpenApiSecurityScheme
                {
                    Description = "Autorização JWT no cabeçalho usando esquema Bearer. Exemplo: \"Bearer {token}\"",
                    Name = "Custom-Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Custom-Authorization"
                            },
                            Name = "Custom-Authorization",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                c.DescribeAllParametersInCamelCase();

                c.IncludeXmlComments(configuration.GetSection("Swagger:FileXML").Value);

            });
        }
}