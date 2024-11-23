using Business.Services;

namespace WebApi.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
             
            services.AddScoped<ILaundryService>(provider =>
                new LaundryService(configuration.GetConnectionString("DefaultConnection")));
             
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            return services;
        }
    }
}
