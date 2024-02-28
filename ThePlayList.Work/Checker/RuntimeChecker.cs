using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Producer;
using MongoDB.Driver;
using System;
using Microsoft.Extensions.DependencyInjection;
using ThePlayList.Work.Data;
using ThePlayList.Work.Repositories;
using System.Linq;

namespace ThePlayList.Work.Checker
{
    public class RuntimeChecker : IRuntimeChecker
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EventBusRabbitMQProducer _eventBusRabbitMQProducer;
        private readonly IThePlayListContext _context; // Eklendi

        public RuntimeChecker(IServiceProvider serviceProvider, EventBusRabbitMQProducer eventBusRabbitMQProducer, IThePlayListContext context)
        {
            _serviceProvider = serviceProvider;
            _eventBusRabbitMQProducer = eventBusRabbitMQProducer;
            _context = context; // Eklendi
        }

        public void CheckRuntime()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedRuntimeChecker = scope.ServiceProvider.GetRequiredService<IRuntimeChecker>();
                scopedRuntimeChecker.PerformCheck();
            }
        }

        public void PerformCheck()
        {
            DateTime currentDateTime = DateTime.Now;

            var allDocuments = _context.Works.Find(_ => true).ToList();

            var filteredDocuments = allDocuments
                .Where(x => x.RunTime.Minute == currentDateTime.Minute)
                .ToList();

            foreach (var item in filteredDocuments)
            {
                var work = _context.Works.Find(a => a.Id == item.Id).FirstOrDefault();
                _eventBusRabbitMQProducer.Publish(EventBusStatics.PlayWork, work);
            }
        }
    }

    public interface IRuntimeChecker
    {
        void CheckRuntime();
        void PerformCheck();
    }
}