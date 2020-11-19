using System.Collections.Generic;

namespace JobApplicationAPI.Models.DTOs
{
    public class QuestionContentDTO
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public List<string> AcceptedAnswers { get; set; }
    }
}
