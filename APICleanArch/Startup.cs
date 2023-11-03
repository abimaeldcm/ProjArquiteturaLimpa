using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CleanArch.Infra.IoC;
using APICleanArch.MappingConfig;
using System.Reflection;
using System.IO;
using System;
using System.Net.Http.Headers;

namespace APICleanArch
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

            services.AddControllers();
            services.AddInfrastructure(Configuration);
            services.AddAutoMapperConfiguration();
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Clean Arch",
                    Version = "v1",
                    Description = "Este projeto é uma API .NET desenvolvida para gerenciar produtos em um mercado. A aplicação oferece operações CRUD (Criar, Ler, Atualizar, Excluir) para manipular os produtos no banco de dados. A arquitetura Clean Architecture é usada para garantir uma separação clara de responsabilidades, incluindo camadas de Aplicação, Domínio, Dados e IoC. O código segue princípios de Clean Code para manutenção e legibilidade.",
                    Contact = new OpenApiContact
                    {
                        Name = "Abimael Mendes",
                        Url = new Uri("https://github.com/abimaeldcm"),
                        Email = "abimaelmens@hotmail.com",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Linkedin",
                        Url = new Uri("https://www.linkedin.com/in/abimaelmends/")
                    }
                }
                );
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Insira o token JWT da solicitação no campo",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                c.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] {}
                    }
                };

                c.AddSecurityRequirement(securityRequirement);
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APICleanArch v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
