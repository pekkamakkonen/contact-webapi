using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ContactsWebApi.Models;
using ContactsWebApi.Repositories;
using ContactsWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ContactsWebApi
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
            services.Configure<AzureSettings>(Configuration.GetSection("AzureSettings"));
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddDbContext<ContactsDbContext>(options => {
                options.UseSqlServer(Configuration["ContactsDbAdminConnection"]);
            });
            services.AddCors(options => options.AddPolicy("AllowAnyPolicy", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

            // Configure Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.Audience = Configuration["AzureSettings:ApplicationId"];
                    options.Authority = Configuration["AzureSettings:LoginUrl"] + Configuration["AzureSettings:DirectoryId"];
                });

            services.AddMvc();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAnyPolicy");
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
