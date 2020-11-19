using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationAPI.Models
{
    public class JobApplication
    {
        public JobApplication()
        {
            IsQualified = false;
            Questions = new List<QuestionResponse>();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public bool IsQualified { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public List<QuestionResponse> Questions { get; set; }

        /// <summary>
        /// To be valid: 
        /// - All questions must be answered
        /// - All questions must be answered correctly
        /// </summary>
        /// <param name="questions"></param>
        public void ValidateQualifications(List<QuestionContent> questions)
        {
            // optimize Questions by turning them into a Dict, saving O(Questions.Length) runtime
            var questionDict = new Dictionary<string, QuestionResponse>();
            foreach (var questionResp in Questions)
            {
                questionDict.TryAdd(questionResp.Id, questionResp);
            }

            foreach (var question in questions)
            {
                // potential performance improvement: turn AcceptanceAnswers into Dict to save O(N) runtime here
                // current runtime of this method: O(questions.Length^2)
                if (questionDict.ContainsKey(question.Id) && question.AcceptedAnswers.Contains(questionDict[question.Id].Answer))
                {
                    // question is valid
                    continue;
                }
                else
                {
                    IsQualified = false;
                    return;
                }
            }

            IsQualified = true;
            return;
        }
    }

    public class QuestionResponse
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
