using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySales.Product.Api.Application;
using MySales.Product.Api.Domain.Aggregates.Interfaces;
using MySales.Product.Api.Domain.Core.Notifications;
using MySales.Product.Api.Domain.Core.Validations.Argument;
using MySales.Product.Api.Domain.Interfaces.Applications;
using MySales.Product.Api.Domain.Interfaces.Repositories;
using MySales.Product.Api.Domain.Interfaces.Repositories.Query;
using MySales.Product.Api.Infrastructure;
using MySales.Product.Api.Infrastructure.Repositories;
using MySales.Product.Api.Interface.Apis;
using System;
using INotification = MySales.Product.Api.Domain.Core.Notifications.Interfaces.INotification;

namespace MySales.Product.Api.Interface
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        //public static IServiceCollection ServiceCollection { get; set; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();

            var assembly = AppDomain.CurrentDomain.Load("MySales.Product.Api.Domain");

            services.AddMediatR(assembly);

            services.AddDbContext<ProductContext>(options =>
                options.UseSqlServer(
                    _configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("MySales.Product.Api.Interface")));

            services.AddTransient<DbContext, ProductContext>();
            services.AddTransient<ProductContext>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IRepository<Domain.Aggregates.Product>, Repository<Domain.Aggregates.Product>>();
            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddScoped<IQueryBuilder<Domain.Aggregates.Product>, QueryBuilder<Domain.Aggregates.Product>>();
            services.AddTransient<IEnsure, Ensure>();
            services.AddScoped<INotification, Notification>();

            services.AddControllers();

            //services.AddRouting();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.ApplicationServices.CreateScope();

            //app.UseProductApi(mediator, notification);

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}