IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<WorkerServiceRPA>();
    })
    .Build();

await host.RunAsync();
