using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lost.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace Lost
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication("LOGIN") // Sets the default scheme to cookies
                .AddCookie("LOGIN", options =>
                {
                    options.AccessDeniedPath = "/account/denied";
                    options.LoginPath = "/account/login";
                });

            services.AddRecaptcha(new RecaptchaOptions
            {
                SiteKey = "6LfZ7-QUAAAAAC-bw6hzjnatSo828vitMHwRDVxZ",
                SecretKey = "6LfZ7-QUAAAAAJH-HrVGttN9R6Ye67Ums7ZMpCOK"
            });

            // Example of how to customize a particular instance of cookie options and
            // is able to also use other services.
            //services.AddSingleton<IConfigureOptions<CookieAuthenticationOptions>, ConfigureMyCookie>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
