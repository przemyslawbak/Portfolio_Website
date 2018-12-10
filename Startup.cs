using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Portfolio_Strona.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Antiforgery;
namespace Portfolio_Strona
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:Portfolio_StronaProjects:ConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:Portfolio_StronaIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();
            services.AddTransient<IProjectRepository, EFProjectRepository>();
            services.AddTransient<ITechnologyRepository, EFTechnologyRepository>();
            services.AddTransient<ILiteratureRepository, EFLiteratureRepository>();
            services.AddTransient<IContactRepository, EFContactRepository>();
            services.AddTransient<ITechProjectRepository, EFTechProjectRepository>();
            services.AddTransient<ILiteratureTechRepository, EFLiteratureTechRepository>();
            services.AddTransient<ISurveyRepository, EFSurveyRepository>();
            services.AddAntiforgery(x => x.HeaderName = "X-CSRF-TOKEN");
            services.AddMvc();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStatusCodePages();
            app.Use(next => context =>
            {
                if (context.Request.Path == "/")
                {
                    //send the request token as a JavaScript-readable cookie
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("CSRF-TOKEN", tokens.RequestToken, new CookieOptions { HttpOnly = false });
                }
                return next(context);
            });
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc(routes => {
                routes.MapRoute(name: "Error", template: "Error",
                defaults: new { controller = "Error", action = "Error" });
                routes.MapRoute(
                name: "default", template: "{controller}/{action}/{id?}",
                defaults: new { controller = "MyProjects", action = "Index" });
            });
        }
    }
}
