using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadU.Interfaces.Auth;
using CadU.Services;
using CadU.Tests.Fakes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CadU
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
      services.AddHttpContextAccessor();
      //services.AddSingleton<Interfaces.Auth.IAuthenticationService, FakeAuthenticationService>();
      services.AddSingleton<Interfaces.Auth.IAuthenticationService, JwtIdentityAuthenticationService>();
      services.AddSingleton<ILoggedUserService, IdentityLoggedUserService>();
      services.AddSingleton<Interfaces.Auth.IAuthorizationService, FakeAuthorizationService>();


      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CadU", Version = "v1" });
      });

      var key = Encoding.ASCII.GetBytes(Settings.Secret);

      services.AddAuthentication(opt =>
      {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(opt =>
      {
        opt.RequireHttpsMetadata = false;
        opt.SaveToken = true;
        opt.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(key);
        opt.TokenValidationParameters.ValidAudience = "FSL";
        opt.TokenValidationParameters.ValidIssuer = "FSL";
        opt.TokenValidationParameters.ValidateIssuerSigningKey = true;
        opt.TokenValidationParameters.ValidateLifetime = true;
        opt.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
      });

      services.AddAuthorization(auth => 
      {
        auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
          .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
          .RequireAuthenticatedUser().Build());
      });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CadU v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
