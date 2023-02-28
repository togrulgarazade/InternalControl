using Business.Validators.Account;
using Core;
using Core.Entities;
using Data.DAL;
using Data.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternalControl
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
            services.AddControllersWithViews()
                .AddFluentValidation(p => p.RegisterValidatorsFromAssemblyContaining<LoginViewModelValidator>());
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:Default"]);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(Options =>
            {
                Options.Password.RequiredLength = 8;
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireDigit = true;


                Options.User.AllowedUserNameCharacters = "abcçdeəfgğhiıjklmnopqrsştuüvyzxqwWABCÇDEƏFGĞHİIJKLMNOPQRSŞTUÜVYZXQ0123456789-._ ";
            });

            //services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Error/Problem");
            });

            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "936620971022431";
                options.AppSecret = "07edb7b9b8a1497c7a111aa1ec248bb5";
            });

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "356116869940-m9lp4v4lukcorjlliqidc95oknc942ub.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });



            //services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Welcome}/{action=Index}/{id?}");
            });
        }
    }
}
