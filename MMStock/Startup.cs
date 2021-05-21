using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibrary.Helpers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MMStock.Data;
using MMStock.EventBusConsumer;
using MMStock.Serivces;

namespace MMStock
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<ProductCreationConsumer>();
                config.AddConsumer<BasketUpdateConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);
                    cfg.ReceiveEndpoint(EventBusConstants.ProductCheckoutQueue, c =>
                    {
                        c.ConfigureConsumer<ProductCreationConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.BasketUpdateEvent, c =>
                    {
                        c.ConfigureConsumer<BasketUpdateConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();

            services.AddScoped<ProductCreationConsumer>();
            services.AddScoped<BasketUpdateConsumer>();
            
            services.AddDbContext<StockContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(StockContext).Assembly.FullName)));
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "MMStock", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MMStock v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}