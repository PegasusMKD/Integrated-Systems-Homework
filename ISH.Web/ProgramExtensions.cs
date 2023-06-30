﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ISH.Repository;
using ISH.Repository.Core;
using ISH.Repository.Implementations;
using ISH.Service;
using ISH.Service.Implementations;
using Microsoft.OpenApi.Models;

namespace Integrated_Systems_Homework
{
    public static class ProgramExtensions
    {

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            return services;
        }

        public static IServiceCollection AddStripeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //StripeConfiguration.ApiKey = configuration.GetValue<string>("StripeSettings:SecretKey");

            //return services
            //    .AddScoped<CustomerService>()
            //    .AddScoped<ChargeService>()
            //    .AddScoped<TokenService>()
            //    .AddScoped<IStripeService, StripeService>();
            return services;
        }

        public static IServiceCollection Configure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            return services;
        }


        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ITicketService, TicketService>();
            return services;
        }

        //public static IServiceCollection AddJwt(this IServiceCollection services, ConfigurationManager configuration)
        //{
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    }).AddJwtBearer(options =>
        //    {
        //        options.SaveToken = true;
        //        options.RequireHttpsMetadata = false;
        //        options.TokenValidationParameters = new TokenValidationParameters()
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidAudience = configuration["JWT:Audience"],
        //            ValidIssuer = configuration["JWT:Issuer"],
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        //        };
        //    });

        //    return services;
        //}

        public static IServiceCollection AddDbContextAndIdentity(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(x => x.UseSqlServer(connectionString));
            //services.AddIdentity<BaseUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection AddSwaggerSecurity(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "LernDeutscheApi", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }
                    }
                });
            });

            return services;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(name: "FrontendOrigins", policy =>
            {
                policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
            }));

            return services;
        }
    }
}
