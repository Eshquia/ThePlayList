using AutoMapper;
using EventBusRabbitMQ.Events;

namespace ThePlayList.Work.Mapper
{
    public class WorkMapper:Profile
    {
        public WorkMapper()
        {
            CreateMap<PlayWorkEvent, Entities.Work>().ReverseMap();
        }
    }
}
