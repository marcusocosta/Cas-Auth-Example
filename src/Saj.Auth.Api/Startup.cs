using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AspNetCore.Security.CAS;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Saj.Auth.Api
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/login");
                    options.AccessDeniedPath = new PathString("/access-denied");
                    options.Cookie = new CookieBuilder
                    {
                        Name = ".AspNetCore.CasSample"
                    };
                })
                .AddCAS(options =>
                {
                    options.CasServerUrlBase = Configuration["CasBaseUrl"];
                    options.CallbackPath = new PathString("/api/signin");
                    options.TicketValidator = new Cas2TicketValidator();
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                });
                                                                  
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
