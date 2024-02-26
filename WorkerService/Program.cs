
using WorkerService.Helpers;

class Program
{
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
             {
              // Configuration'ý al ve RedisHelper'a ver
               RedisHelper.Initialize(config.Build());
             })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<WorkerServiceRPA>();
            })
            .Build();

        host.Run();
    }
}