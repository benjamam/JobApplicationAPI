using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationAPI.Models
{
    public class QuestionContent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoId { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public List<string> AcceptedAnswers { get; set; }
    }
}
