using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SignalR.Data;
using SignalR.Data.Repositories;
using SignalR.Interfaces;
using SignalR.SignalR;

namespace SignalR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSignalR();

            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddAuthentication("Bearer")
               .AddJwtBearer("Bearer", options =>
               {
                   options.Authority = "https://localhost:5002";

                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = false
                   };
               });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SignalR", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "SignalR");
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PresenceHub>("hubs/presence");
            });
        }
    }
}
