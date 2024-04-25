using System;
using System.Net.Http;

namespace FTB.TriviaLib
{
    public class OpenTriviaService
    {
        private readonly HttpClient httpClient;
        private const string BaseUrl = "https://opentdb.com/";

        public OpenTriviaService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public OpenTriviaModel GetQuestions()
        {
            throw new NotImplementedException();
        }

        public OpenTriviaModel GetQuestions(int amount, string category, string difficulty, string type)
        {
            throw new NotImplementedException();
        }

        public OpenTriviaModel GetQuestions(int amount, string category, string difficulty, string type, string token)
        {
            throw new NotImplementedException();
        }

        public OpenTriviaModel GetQuestions(int amount, string category, string difficulty, string type, string token, string encode)
        {
            throw new NotImplementedException();
        }

        public CategoryModel GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}
