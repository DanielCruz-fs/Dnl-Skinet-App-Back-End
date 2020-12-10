using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skinet.Api.Helpers;
using Skinet.Core.Interfaces;
using Skinet.Infrastructure.Data;
using AutoMapper;
using Skinet.Api.Middleware;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Skinet.Api.Errors;
using Skinet.Api.Extensions;
using StackExchange.Redis;

namespace Skinet.Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // In this method the order is not important but there is one exception

            services.AddControllers();

            services.AddDbContext<StoreContext>(options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            // Redis: the connection is designed to be shared and reused between callers and is fully thread safe and ready
            // for less particular usage
            services.AddSingleton<IConnectionMultiplexer>(c => {
                var config = ConfigurationOptions.Parse(this.configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(config);
            });

            // Injecting automapper, it is different 'cause its version 8.1
            services.AddAutoMapper(typeof(MappingProfiles));

            // Call custom class to clean up ConfigureServices
            services.AddApplicationServices();

            // Cors
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // In this method the order is important

            // USING OUR OWN MIDDLEWARE TO HANDLE CUSTOME ERRORS AND EXCEPTIONS
            app.UseMiddleware<ExceptionMiddleware>();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            // Handle a custom error response when we do not have that end point
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            //Serving static content from API
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
