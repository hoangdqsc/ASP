// Import các dịch vụ từ Application_Temp layer
using Application_Temp.Services;

// Import các dịch vụ từ Infrastructure_Temp layer, 
// có thể bao gồm việc thiết lập và cấu hình cho DbContext, các repository, v.v.
using Infrastructure_Temp.Extensions;

// Import các lớp cần thiết cho việc cấu hình Swagger
using Microsoft.OpenApi.Models;

// Khởi tạo một WebApplicationBuilder, dùng để cấu hình và tạo ra WebApplication
var builder = WebApplication.CreateBuilder(args);

// Cấu hình các dịch vụ cơ bản của ASP.NET Core, 
// thêm các dịch vụ cần thiết để xử lý các yêu cầu HTTP và trả về JSON hoặc XML
builder.Services.AddControllers();

// Đăng ký Swagger, một công cụ để tự động sinh ra tài liệu API
builder.Services.AddEndpointsApiExplorer();

// Cấu hình Swagger với các thông tin cơ bản và bảo mật
builder.Services.AddSwaggerGen(c =>
{
    // Tạo một tài liệu API với thông tin cơ bản như tiêu đề và phiên bản
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Thêm cấu hình bảo mật cho Swagger, sử dụng Bearer Token cho API
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please enter token with Bearer scheme into text box", // Mô tả cách nhập token
        Name = "Authorization", // Tên của header chứa token
        In = ParameterLocation.Header, // Vị trí của token trong yêu cầu HTTP (header)
        Type = SecuritySchemeType.ApiKey // Loại bảo mật là API key (Bearer token)
    });

    // Thêm yêu cầu bảo mật cho API, yêu cầu người dùng phải cung cấp token
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme, // Loại tham chiếu là bảo mật
                    Id = "Bearer" // ID của bảo mật định nghĩa bên trên
                }
            },
            new string[] {} // Không yêu cầu scope cụ thể
        }
    });
});

// Đăng ký các dịch vụ tùy chỉnh và cấu hình DbContext từ Infrastructure layer
builder.Services.AddInfrastructureServices(builder.Configuration);

// Đăng ký các dịch vụ từ Application layer, ví dụ như các service business logic
builder.Services.AddApplicationServices();

// Tạo ứng dụng WebApplication dựa trên cấu hình builder đã thực hiện
var app = builder.Build();

// Cấu hình middleware (chuỗi xử lý yêu cầu) cho ứng dụng
if (app.Environment.IsDevelopment()) // Kiểm tra nếu ứng dụng đang chạy trong môi trường phát triển
{
    app.UseSwagger(); // Kích hoạt Swagger để hiển thị tài liệu API
    app.UseSwaggerUI(); // Cấu hình giao diện người dùng Swagger UI
}

// Sử dụng HTTPS cho các yêu cầu (chuyển hướng tất cả yêu cầu HTTP sang HTTPS)
app.UseHttpsRedirection();

// Thêm Middleware xác thực cho ứng dụng (bắt buộc phải có token để truy cập tài nguyên)
app.UseAuthentication(); 

// Thêm Middleware phân quyền (kiểm tra quyền truy cập dựa trên xác thực)
app.UseAuthorization();

// Định tuyến các yêu cầu tới controller thích hợp dựa trên URL
app.MapControllers();

// Seed dữ liệu vào cơ sở dữ liệu khi ứng dụng khởi động
using (var scope = app.Services.CreateScope()) // Tạo một scope mới để sử dụng dịch vụ DI
{
    var services = scope.ServiceProvider; // Lấy provider của dịch vụ

    try
    {
        // Khởi tạo dữ liệu ban đầu vào cơ sở dữ liệu
        SeedData.Initialize(services).Wait();
    }
    catch (Exception ex) // Bắt lỗi nếu có vấn đề xảy ra trong quá trình seed
    {
        // Lấy Logger và ghi lại lỗi
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Chạy ứng dụng (sau khi đã cấu hình tất cả các dịch vụ và middleware)
app.Run();
