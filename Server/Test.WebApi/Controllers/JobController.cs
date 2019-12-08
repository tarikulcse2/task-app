using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test.Entities.Models;
using Test.Service;

namespace Test.WebApi.Controllers
{
    public class JobController : BaseController
    {
        private readonly IJobService jobService;
        public JobController(IJobService jobService)
        {
            this.jobService = jobService;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await jobService.GetAll());
        }

        [HttpGet(nameof(GetByCurrentUser))]
        public ActionResult GetByCurrentUser()
        {
            return Ok(jobService.GetAllByUser(currentUserId));
        }

        [HttpGet(nameof(GetById) + "/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await jobService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Job job)
        {
            job.CreatorId = currentUserId;
            job.CreateDate = DateTime.Now;
            if (await jobService.Add(job) > 0)
            {
                return Ok(new { status = true, Data = job, Message = "Save Success!" });
            }
            return Ok(new { status = false, Data = (string)null, Message = "Error Success!" });
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody]Job job)
        {
            job.ModifierId = currentUserId;
            job.ModifyDate = DateTime.Now;
            if (await jobService.Modify(job) > 0)
            {
                return Ok(new { status = true, Data = job, Message = "Update Success!" });
            }
            return Ok(new { status = false, Data = (string)null, Message = "Uupdate Error!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await jobService.Delete(id) > 0)
            {
                return Ok(new { status = true, Data = jobService.GetAllByUser(currentUserId), Message = "Delete Success!" });
            }
            return Ok(new { status = false, Data = (string)null, Message = "Delete Error!" });
        }
    }
}