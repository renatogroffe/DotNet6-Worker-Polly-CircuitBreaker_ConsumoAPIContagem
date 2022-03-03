using WorkerConsumoAPIContagem;
using WorkerConsumoAPIContagem.Resilience;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton(CircuitBreakerExtensions.CreatePolicy(
            Convert.ToInt32(context.Configuration["CircuitBreaker:NumberOfExceptionsBeforeBreaking"]),
            Convert.ToInt32(context.Configuration["CircuitBreaker:DurationOfBreakInSeconds"])));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();