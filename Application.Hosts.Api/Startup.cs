using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*Nuget Dependancies*/
using Microsoft.OpenApi.Models;
using Application.Configuration.Modules;
using System.Text.Json.Serialization;
using Application.Hosts.Api.AuthenticationManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application.Hosts.Api
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
           
            services.AddControllers();
            services.AddMvc();
            services.AddControllers().AddJsonOptions(options => {

                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );
            });

            #region Swagger
            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Expense Management API",
                });
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.<br /> <br /> 
                      Enter 'Bearer' [space] and then your token in the text input below.<br/> <br /> 
                       Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                        }
                    });
                //options.IncludeXmlComments(string.Format(@"{0}\ExpenseManagementApi.xml", System.AppDomain.CurrentDomain.BaseDirectory));

            });
            #endregion

            #region Database Modules
            var module = new DatabaseModule();
            module.Host = ".";
            module.Password = "";
            module.DatabaseName = "ExpenseManagementSystem";
            module.UserName = "";
            module.IntegratedSecurity = true;

            module.Register(services);
            #endregion


            #region JWTAuth
            var authTokenConfig = Configuration.GetSection("ApplicationKeys:authTokenConfig").Get<AuthTokenConfig>();
            services.AddSingleton(authTokenConfig);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authTokenConfig.Secret)),
                    ValidAudience = authTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };

            });
            services.AddOptions();
            services.AddSingleton<IAuthManager, AuthManager>();
            #endregion


            #region Auth Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin")
                ));

                //options.AddPolicy("User", policy => policy.RequireAssertion(context =>
                //    context.User.IsInRole("User")
                //));

            });
            #endregion
            services.AddLogging(x => { x.AddConsole(); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Expense Management API V1");
            });

            app.UseCors("CorsPolicy");
            
            
        }
    }
}
