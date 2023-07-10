using System;
using System.Threading.Tasks;
using AccountingBook.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AccountingBook.Web
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

      //Db setup
      services.AddDbContext<AppDbContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
      });

      services.AddScoped<Repository>();
      services.AddScoped<FinancialDimensionRepository>();
      services.AddScoped<FinancialDimensionValueRepository>();
      services.AddScoped<AccountConfigRepository>();
      services.AddScoped<MainAccountConfigRepository>();


      services.AddSwaggerGen();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "Financial Swagger UI",
          Description = "APIs for financial app",
        });
      });


      services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
      );

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      //add this at the start of Configure
      app.Use(async (HttpContext context, Func<Task> next) =>
      {
        await next.Invoke();

        if (context.Response.StatusCode == 404 && !context.Request.Path.Value.Contains("/api"))
        {
          context.Request.Path = new PathString("/index.html");
          context.Response.StatusCode = 200;
          await next.Invoke();
        }
      });
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();


      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Financial API V1");
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
