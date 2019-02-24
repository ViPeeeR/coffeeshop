using CoffeeShops.Session.API.Abstracts;
using CoffeeShops.Session.API.Context;
using CoffeeShops.Session.API.Infrastructure;
using CoffeeShops.Session.API.Infrustucture;
using CoffeeShops.Session.API.Infrustucture.Providers;
using CoffeeShops.Session.API.Infrustucture.Redis;
using CoffeeShops.Session.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeShops.Session.API
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
            services.AddLogging();

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "TokenInstance";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices();

            services.AddDbContext<ApplicationContext>(options =>
              options.UseSqlite(Configuration.GetConnectionString("Default")));

            services.Configure<JwtSecurityConfig>(Configuration.GetSection("JwtSecurity"));
            services.Configure<OAuthConfig>(Configuration.GetSection("OAuth"));
            services.AddTransient<ICacheManager, CacheManager>();
            services.AddTransient<IPasswordProvider, PasswordProvider>();
            services.AddTransient<IJwtAuth, JwtAuth>();
            services.AddTransient<IOAuth, OAuth>();
            services.AddTransient<IUserRepository, UserRepository>();
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

            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetService<ApplicationContext>())
                {
                    db.Database.Migrate();
                }
            }

            app.UseMvc();
        }
    }
}
