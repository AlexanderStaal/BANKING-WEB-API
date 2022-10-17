using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using BankingWebAPI.Context;
using BankingWebAPI.Data;
using BankingWebAPI.Repositories;
using BankingWebAPI.Controllers;
using BankingWebAPI.Models;
using BankingWebAPI.Helper;
using BankingWebAPI.Models;

namespace BankingWebAPI
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
            services.AddControllers();


            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddDbContext<UsersDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BankingDB")));

            services.AddDbContext<AccountDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BankingDB")));

            services.AddDbContext<TransactionsHistoryDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BankingDB")));

            services.AddDbContext<TransferFundsDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BankingDB")));

            services.AddDbContext<TransferSourceDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BankingDB")));

            services.AddDbContext<CreteAccountDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BankingDB")));

            var _userContext = services.BuildServiceProvider().GetService<UsersDBContext>();
            services.AddSingleton<IRefreshTokenGenerator>(provider => new RefreshTokenGenerator(_userContext));


            var _jwtsetting = Configuration.GetSection("JWTSetting");
            services.Configure<JWTSetting>(_jwtsetting);

            var authkey = Configuration.GetValue<string>("JWTSetting:securitykey");

            services.AddAuthentication(item =>
            {
                item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(item =>
            {

                item.RequireHttpsMetadata = true;
                item.SaveToken = true;
                item.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authkey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.MaxValue
                };
            });

            services.AddScoped<ISPRepoitory, SPRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowedAnyOrigin",
                b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Banking API", Version = "v1.0" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowedAnyOrigin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(ui =>
            {
                ui.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Banking API Endpoint");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
