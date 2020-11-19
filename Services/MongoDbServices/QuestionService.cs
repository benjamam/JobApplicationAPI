using JobApplicationAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace JobApplicationAPI.Services
{
    public class QuestionService
    {
        private readonly IMongoCollection<QuestionContent> _questions;

        public QuestionService(IJobApplicationsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _questions = database.GetCollection<QuestionContent>(settings.QuestionsCollectionName);
        }

        public List<QuestionContent> GetQuestions()
        {
            return _questions.Find(q => true).ToList();
        }

        public QuestionContent GetQuestion(string id)
        {
            return _questions.Find(q => q.Id == id).FirstOrDefault();
        }

        public QuestionContent CreateQuestion(QuestionContent question)
        {
            _questions.InsertOne(question);
            return question;
        }

        public void DeleteQuestion(string id)
        {
            _questions.DeleteOne(question => question.Id == id);
        }
    }
}
