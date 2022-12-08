using WorkersAndQueues;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<Worker>();
        services.AddSingleton<IMessageConsumer>(sp => sp.GetRequiredService<Worker>());
        services.AddSingleton<IHostedService>(sp => sp.GetRequiredService<Worker>());
        services.AddHostedService<Producer>();
    })
    .Build();

host.Run();