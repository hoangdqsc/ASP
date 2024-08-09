using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure_Temp.Data;
using Application_Temp.Helpers;
using Core_Temp.Interfaces;
using Infrastructure_Temp.Repositories;
using Application_Temp.Services;

namespace Infrastructure_Temp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Cấu hình DbContext với chuỗi kết nối từ appsettings.json
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("chuoiketnoi")));

            // Thêm các dịch vụ khác của Infrastructure nếu có
           services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IPasswordHelper, PasswordHelper>(); // Nếu bạn đang sử dụng PasswordHelper trong Infrastructure
                        
            // services.AddScoped<IUserService, UserService>(); // Thêm UserService
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Đăng ký các dịch vụ Application
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
        }
    }
}
