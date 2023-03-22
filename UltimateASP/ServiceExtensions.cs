using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateASP.Data.EntityClasses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using UltimateASP.Data.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UltimateASP
{
    public static class ServiceExtensions
    {
        public static void IdentityConfiguration(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiUser>(q =>
            q.User.RequireUniqueEmail = true);

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),services);
            builder.AddEntityFrameworkStores<HotelListingDBContext>().AddDefaultTokenProviders();
        }
        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var JwtSection = configuration.GetSection("Jwt");
            var Key = Environment.GetEnvironmentVariable("SECRET");


            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = JwtSection.GetSection("Issuer").Value,
                    ValidAudience = JwtSection.GetSection("Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key))
                };
            });
        }

    }
}
