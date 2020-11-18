namespace JobApplicationAPI.Models
{
    public class JobApplicationsDatabaseSettings : IJobApplicationsDatabaseSettings
    {
        public string JobApplicationsCollectionName { get; set; }
        public string QuestionsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IJobApplicationsDatabaseSettings
    {
        public string JobApplicationsCollectionName { get; set; }
        public string QuestionsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
