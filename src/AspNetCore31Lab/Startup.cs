using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AspNetCore31Lab.Logic;
using AspNetCore31Lab.Middlewares;
using AspNetCore31Lab.Physical;
using AspNetCore31Lab.Protocol.Logic;
using AspNetCore31Lab.Protocol.Model.Data;
using AspNetCore31Lab.Protocol.Physical;
using Autofac;
using Autofac.Features.AttributeFilters;
using Chef.Extensions.DbAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCore31Lab
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
            services.AddControllersWithViews().AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSignature("<!--Made by Johnny-->");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(Assembly.Load("AspNetCore31Lab.Logic")).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.Load("AspNetCore31Lab.Physical")).AsImplementedInterfaces();
            builder.RegisterInstance(AppConfig.Instance).As<IConfig>();

            builder.RegisterType<DataService>().UsingConstructor(typeof(IDataAccess<Member>)).Named<IDataService>("default");
            builder.RegisterType<DataService>().UsingConstructor(typeof(IDataAccess<Member>), typeof(IConfig)).Named<IDataService>("withConfig");

            var controllers = typeof(Startup).Assembly.GetTypes()
                .Where(t => t.BaseType == typeof(Controller) || t.BaseType == typeof(ControllerBase))
                .ToArray();

            builder.RegisterTypes(controllers).WithAttributeFiltering();
        }
    }
}
