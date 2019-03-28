using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherApi.Authorization;
using WeatherApi.Business;
using WeatherApi.Business.Contracts;
using WeatherApi.Configuration;

namespace WeatherApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters.ValidAudiences = new[] { "weatherapi" };
                    options.Authority = "https://localhost:5000";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AtLeast10", policy =>
                    policy.Requirements.Add(new WeatherRequirememt(minCentigrade: 10)));

                options.AddPolicy("AtMost10", policy =>
                    policy.Requirements.Add(new WeatherRequirememt(maxCentigrade: 10)));
            });

            services.AddOptions();
            services.Configure<OpenWeatherApiOptions>(c => Configuration.Bind("OpenWeatherApiOptions", c));

            services.AddHttpClient();
            services.AddSingleton<IWeatherLogic, WeatherLogic>();
            services.AddSingleton<IAuthorizationHandler, WeatherAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
