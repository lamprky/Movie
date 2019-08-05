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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPI.Data;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("WebAPI")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200");
                });
            });

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            RegisterServices(services);
            //RegisterRepositories(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            var servicesRegistrations =
                (from type in typeof(ContributorService).Assembly.GetExportedTypes()
                 where type.Namespace == typeof(ContributorService).Namespace && type.Name.EndsWith("Service")
                 select new
                 {
                     Service = type.GetInterfaces().Single(),
                     Implementation = type
                 }).ToList();

            foreach (var reg in servicesRegistrations)
            {
                services.AddTransient(reg.Service, reg.Implementation);
            }
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            var repositoriesRegistrations =
                           (from type in typeof(ContributorRepository).Assembly.GetExportedTypes()
                            where (type.Namespace == typeof(ContributorRepository).Namespace) &&
                            type.Name.EndsWith("Repository") &&
                            type != typeof(GenericRepository<>) &&
                            type.GetInterfaces().Where(
                                i => i.Name.EndsWith("Repository") &&
                                i != typeof(IGenericRepository<>)
                                ).SingleOrDefault() != null
                            select new
                            {
                                Service = type.GetInterfaces().Where(
                                    i => i.Name.EndsWith("Repository") &&
                                    i != typeof(IGenericRepository<>)
                                    ).SingleOrDefault(),
                                Implementation = type
                            }).ToList();

            foreach (var reg in repositoriesRegistrations)
            {
                services.AddTransient(reg.Service, reg.Implementation);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}