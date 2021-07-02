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
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
             
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IPhotoService,PhotoService>();
            services.AddScoped<IUserRepository,UserRespository>();
            services.AddScoped<LogUserActivity>(); //add the user login active record

            services.AddScoped<ILikeRepository,LikeRepository>(); // For Like Respository

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);//used to declare the auto mapper

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}