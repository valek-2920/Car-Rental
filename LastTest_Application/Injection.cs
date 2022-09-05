using LastTest.Application.Configurations;
using LastTest.Application.Contracts.Managers;
using LastTest.Application.Entensions;
using LastTest.Application.Handlers;
using LastTest.Application.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace LastTest.Application
{
    public static class Injection
    {
        public static IServiceCollection RegisterApplicationServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            JwtConfiguration
                jwtConfiguration =
                    configuration.GetSection("JwtConfiguration")
                        .Get<JwtConfiguration>();

            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));
            services.AddSingleton<IJwtManager, JwtManager>();

            services.AddAuthentication
                (
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer
                    (
                        options =>
                        {
                            var secretKey = Encoding.UTF8.GetBytes(jwtConfiguration.SigninKey);

                            options.SaveToken = true;
                            options.TokenValidationParameters =
                                new TokenValidationParameters
                                {
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = jwtConfiguration.Issuer,
                                    ValidAudience = jwtConfiguration.Audience,
                                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                                };
                        }
                    );

            services.AddAuthorization(
                    options =>
                    {
                        foreach (var value in Enum.GetValues(typeof(PermissionTypes)))
                        {
                            PermissionTypes permissionType = (PermissionTypes)value;
                            var description = permissionType.GetDescription().Split(':');
                            var claim = new Claim(description.FirstOrDefault(), description.LastOrDefault());

                            options.AddPolicy
                            (
                                String.Join(":", description),
                                policy =>
                                {
                                    policy.Requirements.Add
                                         (new PermissionRequirement(permissionType));
                                }
                             );
                        }
                    }
                 );

            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

            return services;
        }
    }
}
