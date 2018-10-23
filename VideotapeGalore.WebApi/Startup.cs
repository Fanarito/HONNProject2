using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Implementations;
using VideotapeGalore.Services.Interfaces;
using VideotapeGalore.WebApi.ExceptionHandlerExtensions;

namespace VideotapeGalore.WebApi
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
            
            // Add application db
            var connectionString = Configuration.GetConnectionString("Application");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlite(connectionString));
            
            // DI services
            services.AddScoped<IFriendsService, FriendsService>();
            services.AddScoped<ITapesService, TapesService>();
            services.AddScoped<IReviewsService, ReviewsService>();
            services.AddScoped<IRecommendationService, RecommendationService>();
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

            app.UseGlobalExceptionHandler();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
