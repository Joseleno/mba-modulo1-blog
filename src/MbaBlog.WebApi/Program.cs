
using MbaBlog.Infrastructure;
using MbaBlog.WebApi.Data.Dtos;
using MbaBlog.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace MbaBlog.WebApi;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddInfrastructure(builder.Configuration);

        builder.Services.AdicionarRepositorio();
        builder.Services.AdicionarUtils();

        builder.Services
            .AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = false;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>  
        {
            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
            {
                Scheme = "Bearer",
                Name = "Authorization",
                Description = "Insira aqui o token: 'Bearer  {token}'",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header
            });

            x.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            }
            );
        });

        var jwtSettingsOptions = builder.Configuration.GetSection("JwtSettings");
        builder.Services.Configure<JwtSettings>(jwtSettingsOptions);

        var jwtSettings = jwtSettingsOptions.Get<JwtSettings>();
        var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

        builder.Services.AddAuthentication(op =>
        {
            op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(op =>
        {
            op.RequireHttpsMetadata = true;
            op.SaveToken = true;
            op.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidIssuer = jwtSettings.Issuer
            };
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.UseDbMigrationHelper();

        app.Run();
    }
}