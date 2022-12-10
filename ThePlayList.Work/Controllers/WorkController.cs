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
        public async Task<ActionResult<IEnumerable<Entities.Work>>> GetWorks()
        {
            var works = await _workRepository.GetAllWorks();
            return Ok(works);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Entities.Work>>> CreateWork([FromBody] Entities.Work work)
        {
            await _workRepository.Create(work);
            return Ok();
        }
        [Route("PlayWork")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Entities.Work>>> PlayWork([FromBody] Entities.Work work)
        {
            var playwork =await _workRepository.GetWork(work.Id);
           
            PlayWorkEvent playevent = _mapper.Map<PlayWorkEvent>(work);
            if (playwork != null)
            {
                try
                {
                    _eventBusRabbitMQProducer.Publish(EventBusStatics.PlayWork, playevent);
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
