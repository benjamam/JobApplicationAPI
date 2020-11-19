using System.Collections.Generic;

namespace JobApplicationAPI.Models
{
    public class JobApplicationDTO
    {
        public JobApplicationDTO()
        {
            Questions = new List<QuestionResponse>();
        }
        public string Name { get; set; }
        public List<QuestionResponse> Questions { get; set; }
    }
}
