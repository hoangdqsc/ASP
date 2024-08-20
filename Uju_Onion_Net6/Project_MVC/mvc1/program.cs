using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using mvc1.Services;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/myapp.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Đăng ký ModbusTcpClient với ILogger
builder.Services.AddSingleton<ModbusTcpClient>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<ModbusTcpClient>>();
    return new ModbusTcpClient("192.168.1.10", 8500, logger);
});



/*
// Đăng ký dịch vụ ModbusTcpClient trong DI container
builder.Services.AddSingleton<ModbusTcpClient>(provider =>
{
    // Thay đổi các tham số kết nối nếu cần
    return new ModbusTcpClient("192.168.1.10", 8500);
});
*/

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=plc}/{action=Index}/{id?}");

app.Run();
