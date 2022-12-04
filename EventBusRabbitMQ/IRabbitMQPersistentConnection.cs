using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection :IDisposable
    {
        public bool isConnected { get; }
        public bool TryConnect();
        IModel CreateModel();
    }
}
