using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentCenterAuthApi.src.Application.Interfaces;
using StudentCenterAuthApi.src.Application.Services;
using StudentCenterAuthApi.src.Infrastructure.Data.Context;
using StudentCenterAuthApi.src.Infrastructure.Dependency_Injection;
using StudentCenterAuthApi.src.Infrastructure.Utils;
using System.Text;

namespace StudentCenterAuthApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("https://studentcenterweb-f9bucggxcjcrescj.brazilsouth-01.azurewebsites.net", 
                                        "https://studentcenteracademic-b8b6ajakhcg4dsbx.brazilsouth-01.azurewebsites.net",
                                        "https://localhost:7260",
                                        "https://localhost:7236",
                                        "http://localhost:5001",
                                        "https://localhost:7291")
                           .AllowCredentials()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentAuthApi", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Enter 'Bearer' [space] and your token!",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In= ParameterLocation.Header
                        },
                        new List<string> ()
                    }
                 });
            });

            builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();

            builder.Services.AddSingleton<AuthContext>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddInterfaces(builder.Configuration);

            var key = Encoding.ASCII.GetBytes(Util.GetSecretWithCacheAsync().Result);            

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = "https://studentcenterauthapi-gna4fkhdgmbyg8cc.brazilsouth-01.azurewebsites.net",
                    ValidIssuer = "https://localhost:7048",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Urls.Add("http://0.0.0.0:80");

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
