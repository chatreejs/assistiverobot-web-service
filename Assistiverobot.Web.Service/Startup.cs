using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssistiveRobot.Web.Service.Domains;
using AssistiveRobot.Web.Service.Middlewares;
using AssistiveRobot.Web.Service.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace AssistiveRobot.Web.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private readonly string _allowSpecificOrigins = "_ToktakWebServiceAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => 
            {
                options.AddPolicy(_allowSpecificOrigins, builder => 
                {
                    Configuration.GetSection("WebServiceSettings:CorsPolicy").GetChildren().ToList().ForEach(corsPolicy =>
                    {
                        builder
                            .WithOrigins(corsPolicy.Value)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowedToAllowWildcardSubdomains();
                    });
                });
            });
            services.AddDbContext<AssistiveRobotContext>(opts => opts.UseSqlServer(
                Configuration["WebServiceSettings:AssistiveRobotDB"]));
            services.AddScoped<JobRepository>();
            services.AddScoped<GoalRepository>();
            services.AddScoped<LocationRepository>();

            services.AddControllers()
                .AddNewtonsoftJson(
                    options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        };
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseOptions();

            app.UseRouting();

            app.UseCors(_allowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}