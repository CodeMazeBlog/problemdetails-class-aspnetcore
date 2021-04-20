using ErrorHandlingProblemDetails.CustomExceptions;
using ErrorHandlingProblemDetails.Data.Context;
using ErrorHandlingProblemDetails.Services;
using ErrorHandlingProblemDetails.Services.Interfaces;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ErrorHandlingProblemDetails
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProblemDetails(setup =>
            {
                setup.IncludeExceptionDetails = (ctx, env) => CurrentEnvironment.IsDevelopment() || CurrentEnvironment.IsStaging();

                setup.Map<ProductCustomException>(exception => new ProductCustomDetails
                {
                    Title = exception.Title,
                    Detail = exception.Detail,
                    Status = StatusCodes.Status500InternalServerError,
                    Type = exception.Type,
                    Instance = exception.Instance,
                    AdditionalInfo = exception.AdditionalInfo
                });
            });
            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("test-db");
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ErrorHandlingProblemDetails", Version = "v1" });
            });

            services.AddScoped<IProductService, ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ErrorHandlingProblemDetails v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
