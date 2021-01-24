using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySQL.Data;
using MvcCorePaco.Data;
using MvcCorePaco.Helpers;
using MvcCorePaco.Repositories;

namespace MvcCore
{
    public class Startup
    {
        IConfiguration Configuration;
        public Startup(IConfiguration config)
        {
            this.Configuration = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            String cadenaSQL = Configuration.GetConnectionString("CadenaSqlHospitalCasa");
            string cadenaOracle = Configuration.GetConnectionString("CadenaOracleDeptCasa");
            string cadenaMySQL = Configuration.GetConnectionString("CadenaMySQLHospital");
            services.AddTransient<PathProvider>();
            services.AddTransient<RepositoryJoyerias>();
            services.AddTransient<RepositoryAlumnos>();
            // services.AddTransient<IRepositoryDepartamentos, RepositoryDepartamentosSQL>();
            //services.AddTransient<IRepositoryDepartamentos>(x => new RepositoryDepartamentosOracle(cadenaOracle));
            services.AddDbContext<DepartamentosContext>(options => options.UseMySQL(cadenaMySQL));
            services.AddTransient<IRepositoryDepartamentos, RepositoryDepartamentosMySQL>();
            //services.AddDbContext<DepartamentosContext>(options => options.UseSqlServer(cadenaSQL));
            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
