using API.Data;
using API.Helper;
using API.Interface;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection  AddApplicationService(this IServiceCollection services,IConfiguration config)
        {
             services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IUserRepository,UserRespository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);//used to declare the auto mapper
            return services;
        }
    }
}