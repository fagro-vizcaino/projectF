using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectF.Api.Infrastructure;
using ProjectF.EmailService;
using Microsoft.AspNetCore.Routing;
using ProjectF.EmailService.Auth;
using System;
using FluentValidation.AspNetCore;
using ProjectF.Application.Companies;
using ProjectF.Data.Entities.Auth;

namespace ProjectF.Api
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
            services.ConfigureCors();
            services.ConfigureAppServices();

            services.ConfigureAppDb(Configuration);

            services.ConfigureMvcViews();
            
            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);

            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            emailConfig.UserName = Environment.GetEnvironmentVariable("emailusername") ?? string.Empty;
            emailConfig.Password = Environment.GetEnvironmentVariable("emailpassword") ?? string.Empty;

            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            //services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();

            var authHtmlTemplate = Configuration
                .GetSection("AuthTemplates")
                .Get<AuthHtmlTemplateConfig>();

            services.AddSingleton(authHtmlTemplate);
            services.AddScoped<IGetClaimsProvider, GetClaimsFromUser>();

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CompanyValidator>());

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
                
                endpoints.MapControllerRoute(name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                .RequireAuthorization();
            });
        }
    }
}
