using Microsoft.AspNetCore.Mvc;
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
        public WorkController(IWorkRepository workRepository)
        {
            _workRepository = workRepository;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Entities.Work>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Entities.Work>>> GetWorks()
        {
           var works= await _workRepository.GetAllWorks();
            return Ok(works);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Entities.Work>>> CreateWork([FromBody]Entities.Work work)
        {
            await _workRepository.Create(work);
           return Ok();
        }

    }
}
