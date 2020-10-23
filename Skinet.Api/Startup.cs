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

            services.AddScoped<IProductRepository, ProductRepository>();
            // Injecting a generic service
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Injecting automapper, it is different 'cause its version 8.1
            services.AddAutoMapper(typeof(MappingProfiles));

            // order exception after add controllers in order to catch the model state validation
            services.Configure<ApiBehaviorOptions>(options => 
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                                         .SelectMany(x => x.Value.Errors)
                                                         .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
