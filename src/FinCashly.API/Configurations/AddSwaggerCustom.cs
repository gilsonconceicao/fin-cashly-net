using FinCashly.API.Extensions;
using Microsoft.OpenApi.Models;

namespace FinCashly.API.Configurations;

public static class Swagger
{
    public static IServiceCollection AddSwaggerSetting(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "FinCashly API",
                Description = "API REST para gerenciamento financeiro pessoal. Autenticação via Firebase, autorização por roles, arquitetura Clean Architecture + CQRS.",

            });

            options.SchemaFilter<SchemeFilterSwashbuckle>();

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Entre com o token JWT. Exemplo: Bearer {token aqui}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var xmlFile in xmlFiles)
            {
                options.IncludeXmlComments(xmlFile, includeControllerXmlComments: true);
            }
        });

        return services;
    }
}
