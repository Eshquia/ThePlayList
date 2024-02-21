using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using WorkerService;
using static WorkerService.Models.BaseModel;

public class WorkerServiceRPA : BackgroundService
{
    private readonly ILogger<WorkerServiceRPA> _logger;
    private readonly IConfiguration _configuration;

    public WorkerServiceRPA(ILogger<WorkerServiceRPA> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var eventBusHostname = _configuration["EventBus:Hostname"];
        var eventBusUserName = _configuration["EventBus:UserName"];
        var eventBusPassword = _configuration["EventBus:Password"];
        var eventBusRetryCount = _configuration.GetValue<int>("EventBus:RetryCount");

        var factory = new ConnectionFactory()
        {
            HostName = eventBusHostname,
            UserName = eventBusUserName,
            Password = eventBusPassword,
            // Diðer baðlantý ayarlarý...
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Kuyruðu tanýmla
            channel.QueueDeclare(queue: "playWork", durable: false, exclusive: false, autoDelete: false, arguments: null);

            // EventingBasicConsumer oluþtur
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Manager.Process(JsonConvert.DeserializeObject<Node>(message));
                _logger.LogInformation("Gelen Veri: {message}", message);
            };

            // Kuyruðu dinle
            channel.BasicConsume(queue: "playWork", autoAck: true, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}