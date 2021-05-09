using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DocumentProcessingService.app.Infrastructure.Extensions;

namespace DocumentProcessingService.app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddHostedService<DocumentProcessingWorker>()
                        .RegisterServices()
                        .AddRepositories();
                })
                .ConfigureLogging((host, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                });
    }
}
