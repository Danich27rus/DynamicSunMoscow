using Serilog;

namespace DynamicSunMoscow;

public class Program
{
    public async static Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();

        var host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();

        Log.Warning("Application Starting");

        host.Run();

        Log.CloseAndFlush();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Start>();
            });
}