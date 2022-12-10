using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;

namespace EventBusRabbitMQ
{
    public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private readonly int _retryCount;
        private readonly ILogger<DefaultRabbitMQPersistentConnection> _logger;
        private bool _disposed;
        public bool isConnected
            {
               get { return _connection != null && _connection.IsOpen && !_disposed; }
            }

        public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory, int retryCount, ILogger<DefaultRabbitMQPersistentConnection> logger)
        {
            _connectionFactory = connectionFactory;
            _retryCount = retryCount;
            _logger = logger;
        }
        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ client is trying to Connect");
            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount , retryAttempt => TimeSpan.FromSeconds (Math.Pow(2, retryAttempt)), (ex, time) => 
                {
                    _logger.LogWarning(ex, "Rabbitmq try Connect");
                });
            policy.Execute(() =>
            {
                _connection = _connectionFactory.CreateConnection();
            });
            if (isConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutDown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;
                _logger.LogInformation("RabbitMq connected");
                return true;
            }
            else
            {
                _logger.LogInformation("RabbitMq connection error");
                return false;
            }
        }

        public IModel CreateModel()
        {
            if(!isConnected)
            {
                _logger.LogError("Connection not available");
            }
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            try
            {
                _connection.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
            }
        }
        private void OnConnectionBlocked(object sender,ConnectionBlockedEventArgs e)
        {
            if(_disposed) return;
            TryConnect();
        }
         void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            TryConnect();
        }
         void OnConnectionShutDown(object sender, ShutdownEventArgs e)
        {
            if (_disposed) return;
            TryConnect();
        }


    }
}
