using System;
using System.Text;
using AutoMapper;
using EventAppCore.Repositories;
using EventAppCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using EventAppCore.Models;
using EventAppCore.Models.View;
using EventAppCore.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EventAppCore
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public Startup(IHostingEnvironment env)
        {
            _environment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            if (!_environment.IsEnvironment("LocalMac"))
            {
                services.AddApplicationInsightsTelemetry(Configuration);
            }

            // Include interfaces here as well, along with derived class?
            services.AddScoped<UserRepository>();
            services.AddScoped<RefreshTokenRepository>();
            services.AddScoped<EventRepository>();
            services.AddScoped<LocationRepository>();

            services.AddScoped<EncryptionService>();
            services.AddScoped<AccessTokenService>();

            services.AddDbContext<MainContext>();

            // Automatic migration on startup
            // ** Will not run when only published, applies after first request.
            // ** Trying to run it in the postPublish instead
            /*using (var context = new MainContext(_environment))
            {
                context.Database.Migrate();
            }*/

            services.AddMvc().AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                config.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (_environment.IsEnvironment("LocalMac"))
            {
                loggerFactory.AddConsole();
            }
            app.UseStatusCodePages();
            // TODO
            // This may be a bad idea, no clue how much unnecessary stuff this does
            // Would prefer to use dependency injection for this?
            Mapper.Initialize(config =>
            {
                config.CreateMap<User, SignUpUser>().ReverseMap();
                config.CreateMap<User, ViewUser>().ReverseMap();

                config.CreateMap<Event, CreateEvent>().ReverseMap();
                config.CreateMap<Event, ViewEvent>().ReverseMap();

                config.CreateMap<Location, CreateLocation>().ReverseMap();
                config.CreateMap<Location, ViewLocation>().ReverseMap();

                config.CreateMap<RefreshToken, ViewRefreshToken>().ReverseMap();
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (!_environment.IsEnvironment("LocalMac"))
            {
                app.UseApplicationInsightsRequestTelemetry();
                app.UseApplicationInsightsExceptionTelemetry();
            }
            if (_environment.IsEnvironment("LocalMac"))
            {

            }

            var secretKey = Environment.GetEnvironmentVariable("JWT_SIGNING_KEY");
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "https://eventappcore.azurewebsites.net/",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "https://eventappcore.azurewebsites.net/",

                // Validate the token expiry≤
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseMvc();
        }
    }
}