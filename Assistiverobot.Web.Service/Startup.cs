using System;
using System.Linq;
using System.Text;
using AssistiveRobot.Web.Service.Core;
using AssistiveRobot.Web.Service.Domains;
using AssistiveRobot.Web.Service.Middlewares;
using AssistiveRobot.Web.Service.Repositories;
using AssistiveRobot.Web.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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

            // Register services - Application settings
            services.Configure<AppSettings>(Configuration.GetSection("WebServiceSettings"));

            // Add database context.
            services.AddDbContext<AssistiveRobotContext>(opts => opts.UseSqlServer(
                Configuration["WebServiceSettings:Database"]));

            // Register services
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IUserTokenRepository, UserTokenInMemoryRepository>();

            // Register services
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IGoalService, GoalService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IAuthenService, AuthenService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.Audience = Configuration.GetValue<string>("WebServiceSettings:OAuth:Issuer");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            Configuration.GetValue<string>("WebServiceSettings:OAuth:SecretKey"))),
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}