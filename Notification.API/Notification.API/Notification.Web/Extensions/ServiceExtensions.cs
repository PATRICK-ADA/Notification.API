﻿using Invoice.API.KafkaConsumerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Notification.API.Nofication.Core.Abstraction;
using RoomService.Infrastructure.Data;
using Serilog;


namespace Notification.API.Notification.Web.Extensions
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection ConfigureKafka(this IServiceCollection services)
        {


            services.AddHostedService<BidConsumer>();
            return services;    
        }


        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
        {


            builder.Host.UseSerilog((context, config) =>
            {
                config.Enrich.FromLogContext()
                    .WriteTo.Console()
                    .ReadFrom.Configuration(context.Configuration);

            });
            return builder;

        }

        public static IServiceCollection AppServices(this IServiceCollection services, IConfiguration configuration)
        {



            services.AddAuthentication();
            services.AddAuthorization();
            services.AddControllers();

            services.AddScoped<INotification_Publisher, Notification_Publisher>();

            services.AddDbContext<AppDbContext>(options =>
              options.UseNpgsql(
                 configuration.GetConnectionString("DefaultConnection"),

                  b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)), ServiceLifetime.Transient);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithMethods("GET", "PUT", "DELETE", "POST")
                    );
            });
            return services;
        }


        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddSwaggerGen
                (g =>
                {
                    g.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Auction Notification Management API",
                        Description = "Documentation for Auction Notification Management API"

                    });

                });
            return services;
        }
    }
}