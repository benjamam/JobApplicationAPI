using JobApplicationAPI.Models;
using JobApplicationAPI.Services;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public ActionResult<List<JobApplicationDTO>> Get()
        {
            try
            {
                var jobApps = _jobApplicationService.GetApplications(false);
                var jobAppsDTO = new List<JobApplicationDTO>();
                mapJobApplications(jobApps, jobAppsDTO);

                return jobAppsDTO;
            }
            catch
            {
                // log here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred when trying to GET Job Applications. It has been logged and is being looked into.");
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody] JobApplication application)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _jobApplicationService.CreateApplication(application);
                    var jobAppDTO = new JobApplicationDTO();
                    mapJobApplication(application, jobAppDTO);
                    return Created("", jobAppDTO);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch
            {
                // log here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred when trying to POST the Job Application. It has been logged and is being looked into");
            }

        }

        // this would be handled by a mapper like AutoMapper or custom Mapper implementation if I had more time
        private void mapJobApplications(List<JobApplication> jobApps, List<JobApplicationDTO> jobAppsDTO)
        {
            foreach (var app in jobApps)
            {
                jobAppsDTO.Add(new JobApplicationDTO()
                {
                    Name = app.Name,
                    Questions = app.Questions
                });
            }
        }

        private void mapJobApplication(JobApplication jobApp, JobApplicationDTO jobAppDTO)
        {
            jobAppDTO.Name = jobApp.Name;
            jobAppDTO.Questions = jobApp.Questions;
        }
    }
}
