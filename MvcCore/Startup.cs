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
            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(15);
            });

            String cadenaSQL = Configuration.GetConnectionString("CadenaSqlHospitalCasa");
            string cadenaSQLClase = Configuration.GetConnectionString("CadenaSqlHospitalClase");
            string cadenaOracle = Configuration.GetConnectionString("CadenaOracleDeptCasa");
            string cadenaMySQL = Configuration.GetConnectionString("CadenaMySQLHospital");
            string azureSQL = Configuration.GetConnectionString("AzureSQL");

            services.AddSingleton<IConfiguration>(this.Configuration);
            services.AddSingleton<MailService>();
            services.AddSingleton<FileUploadService>();
            services.AddSingleton<PathProvider>();
            services.AddTransient<RepositoryJoyerias>();
            services.AddTransient<RepositoryAlumnos>();
            services.AddTransient<RepositoryUsuarios>();
            //SQL SERVER
            services.AddTransient<IRepositoryHospital, RepositoryHospital>();
            services.AddDbContext<HospitalContext>(options => options.UseSqlServer(cadenaSQLClase));
            //ORACLE DB
            //services.AddTransient<IRepositoryDepartamentos>(x => new RepositoryDepartamentosOracle(cadenaOracle));
            //MYSQL CON POMELO
            //services.AddDbContextPool<DepartamentosContext>(options => options.UseMySql(cadenaMySQL, ServerVersion.AutoDetect(cadenaMySQL)));
            //services.AddTransient<IRepositoryDepartamentos, RepositoryDepartamentosMySQL>();

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
            app.UseResponseCaching();
            app.UseStaticFiles();
            app.UseSession();

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
