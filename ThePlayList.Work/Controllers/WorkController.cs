using AutoMapper;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ThePlayList.Work.Entities;
using ThePlayList.Work.Repositories;

namespace ThePlayList.Work.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IWorkRepository _workRepository;
        private readonly IMapper _mapper;
        public EventBusRabbitMQProducer _eventBusRabbitMQProducer;
        public WorkController(IWorkRepository workRepository,IMapper mapper, EventBusRabbitMQProducer eventBusRabbitMQProducer)
        {
            _workRepository = workRepository;
            _mapper = mapper;
            _eventBusRabbitMQProducer = eventBusRabbitMQProducer;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Entities.Work>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetWorks()
        {
            var works = await _workRepository.GetAllWorks();
            return Ok(works);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateWork([FromBody] Entities.Work work)
        {
            await _workRepository.Create(work);
            return Ok();
        }
        [Route("PlayWork")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> PlayWork(string Id)
        {
            var playwork =await _workRepository.GetWork(Id);
                    
            if (playwork != null)
            {
                try
                {
                    _eventBusRabbitMQProducer.Publish(EventBusStatics.PlayWork, playwork);
                    return Ok();
                }
                catch (Exception e)
                {
                    throw;
                }
            }

          return NotFound();
    }
}
}
