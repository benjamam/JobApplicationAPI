using JobApplicationAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace JobApplicationAPI.Services
{
    public class JobApplicationService
    {
        private readonly IMongoCollection<JobApplication> _jobApplications;

        public JobApplicationService(IJobApplicationsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _jobApplications = database.GetCollection<JobApplication>(settings.JobApplicationsCollectionName);
        }

        public List<JobApplication> GetApplications(bool qualifiedOnly)
        {
            if (qualifiedOnly)
            {
                return _jobApplications.Find(a => a.IsQualified == true).ToList();
            }

            return _jobApplications.Find(a => true).ToList();

        }

        public JobApplication CreateApplication(JobApplication jobApplication)
        {
            _jobApplications.InsertOne(jobApplication);
            return jobApplication;
        }
    }
}
