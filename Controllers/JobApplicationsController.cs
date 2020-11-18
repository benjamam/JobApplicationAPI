using JobApplicationAPI.Models;
using JobApplicationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace JobApplicationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly JobApplicationService _jobApplicationService;
        public JobApplicationsController(JobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        // GET: api/<JobApplicationsController>
        [HttpGet]
        public List<JobApplication> Get()
        {

            return _jobApplicationService.GetApplications(true);
        }

        // GET api/<JobApplicationsController>/5
        //[HttpGet("{id}")]
        //public AcceptedApplications Get(int id)
        //{
        //    var acceptedApplications = new AcceptedApplications();
        //    acceptedApplications.Applications.Add(new JobApplication()
        //    {
        //        Name = "Adam",
        //        Questions = new List<QuestionContent>()
        //        {
        //            new QuestionContent()
        //            {
        //                Id = "1",
        //                Question = "How old are you?",
        //                Answer = "16"
        //            }
        //        }
        //    });

        //    return acceptedApplications;
        //}

        // POST api/<JobApplicationsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            var jobApp = new JobApplication()
            {
                Name = "Adam's Test 2",
                IsQualified = true,
                Questions = new List<QuestionResponse>()
                {
                    new QuestionResponse()
                    {
                        Id = "1",
                        Answer = "16"
                    }
                }
            };
            _jobApplicationService.CreateApplication(jobApp);
        }

        // PUT api/<JobApplicationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<JobApplicationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
