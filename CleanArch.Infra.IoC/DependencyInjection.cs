using CleanArch.Application.InputModes;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.Data.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
                      IConfiguration  Configuration)
        {
               services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DataBase"),
               b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => 
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IValidator<ProductViewModel>, Validators>();


             return services;
        }
        
    }
}
