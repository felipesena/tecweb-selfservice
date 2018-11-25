using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WebApp.Model;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Setting.ConnectionString = Configuration.GetSection("ConnectionStrings:MyDbConnection").Value;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSession();
            services.AddEntityFrameworkSqlServer();


            //services.AddDbContext<AplicacaoDbContext>(options =>
            //    options.UseSqlServer(Environment.GetEnvironmentVariable("SQLCONNSTR_MyDbConnection")));
            services.AddDbContext<AplicacaoDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.BuildServiceProvider().GetService<AplicacaoDbContext>().Database.Migrate();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "APIAlimentos", Version = "v1" });
            });
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
                app.UseHsts();
            }

            app.UseStaticFiles();
            //using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    scope.ServiceProvider.GetRequiredService<AplicacaoDbContext>().Database.Migrate();
            //}

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiAlimentos V1");
            });

            
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseMvc();
        }
    }
}
