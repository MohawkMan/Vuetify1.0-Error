﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using VBL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AutoMapper;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using VBL.Data.Mapping;
using VBL.Core;

namespace VBL.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Db Context
            services.AddDbContext<VBLDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<VBLDbContext>()
            .AddDefaultTokenProviders();

            ////External Logins
            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //    facebookOptions.SaveTokens = true;
            //});

            // ===== Add Jwt Authentication ========
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["JwtIssuer"],
                    ValidAudience = Configuration["JwtIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"]))
                };
            });

            // AutoMapper
            services.AddAutoMapper(typeof(ApplicationUserDTO).Assembly);

            // MVC
            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());

                var mustBeLoggedIn = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(mustBeLoggedIn));
            })
            .AddJsonOptions(options => {
                options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
            });

            //Versioning
            services.AddApiVersioning(o => o.AssumeDefaultVersionWhenUnspecified = true);

            //CORs
            services.AddCors();

            //Swagger/Swashbuckle
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "VBL API", Version = "v1", Description = "API used for Volleyball Life" });
                c.OperationFilter<AddAuthTokenHeaderParameter>();

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "VBL.Api.xml");
                c.IncludeXmlComments(xmlPath);
                c.CustomSchemaIds(SwaggerSchemaIdStrategy);
            });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<ApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<TournamentManager, TournamentManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            //CORS
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });

            app.UseAuthentication();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VBL API V1");
            });

            EnsureSeeded(app, false);
        }

        private static string SwaggerSchemaIdStrategy(Type currentClass)
        {
            string returnedValue = currentClass.Name;
            if (returnedValue.EndsWith("DTO"))
                returnedValue = returnedValue.Replace("DTO", string.Empty);
            return returnedValue;
        }

        private static void EnsureSeeded(IApplicationBuilder applicationBuilder, bool autoMigrateDatabase)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<VBLDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
                if (autoMigrateDatabase)
                {
                    db.Database.Migrate();
                }
                db.EnsureSeedData(userManager, roleManager).Wait();
            }
        }
    }
}