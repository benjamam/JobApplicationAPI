using JobApplicationAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace JobApplicationAPI.Services
{
    public class JobApplicationService
    {
        private readonly IMongoCollection<JobApplication> _jobApplications;
        private readonly QuestionService _questionService;
        public JobApplicationService(IJobApplicationsDatabaseSettings settings, QuestionService questionService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _jobApplications = database.GetCollection<JobApplication>(settings.JobApplicationsCollectionName);

            _questionService = questionService;
        }

        public List<JobApplication> GetApplications(bool qualifiedOnly)
        {
            if (qualifiedOnly)
            {
                return _jobApplications.Find(a => a.IsQualified == true).ToList();
            }

            return _jobApplications.Find(a => true).ToList();

        }

        public JobApplication GetApplication(string id)
        {
            return (JobApplication)_jobApplications.Find(a => a.Id == id);
        }

        public JobApplication CreateApplication(JobApplication jobApplication)
        {
            jobApplication.ValidateQualifications(_questionService.GetQuestions());

            _jobApplications.InsertOne(jobApplication);
            return jobApplication;
        }
    }
}
