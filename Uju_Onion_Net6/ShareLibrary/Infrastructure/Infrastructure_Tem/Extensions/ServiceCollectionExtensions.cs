using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure_Temp.Data;
using Application_Temp.Helpers;
using Core_Temp.Interfaces;
using Infrastructure_Temp.Repositories;
using Application_Temp.Services;
using Application_Temp.Interfaces;

namespace Infrastructure_Temp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Cấu hình DbContext với chuỗi kết nối từ appsettings.json
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("chuoiketnoi")));

            // Đăng ký các repository của tầng Infrastructure
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            // Đăng ký các helper nếu có (vd: PasswordHelper)
            services.AddScoped<IPasswordHelper, PasswordHelper>(); 
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Đăng ký các service của tầng Application
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();

            // Đăng ký thêm các helper nếu cần thiết (ví dụ: TokenHelper)
            services.AddScoped<ITokenHelper, TokenHelper>(); 
        }
    }
}
