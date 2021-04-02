using Lab06.BL.Services.Concrete;
using Lab06.BL.Services.Interfaces;
using Lab06.DAL.DbContext;
using Lab06.DAL.Entities;
using Lab06.DAL.UOW.Concrete;
using Lab06.DAL.UOW.Interfaces;
using Lab06.MVC.Validation.AccountValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lab06.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //DAL
            services.AddDbContext<ApplicationContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("ConnectionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //BL
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IBusService, BusService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITripService, TripService>();
            services.AddTransient<IDatetimeService, DatetimeService>();

            //PL
            services.AddScoped<IUserValidator<ApplicationUser>, CustomUserValidator>();
            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.User.AllowedUserNameCharacters = "0123456789abcdefghijklmnopqrstuvwxyz";
                opts.Password.RequiredLength = 6;
                opts.Password.RequireDigit = false;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationContext>();

            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup).Assembly, typeof(AppUserService).Assembly);
        }

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

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Info",
                    pattern: "Info",
                    defaults: new { controller = "Home", action = "StationInfo" }
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Register}/{id?}");
            });
        }
    }
}
