using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public List<QuestionResponse> Questions { get; set; }

    }
}
