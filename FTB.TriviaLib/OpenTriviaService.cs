using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FTB.TriviaLib
{
    public class OpenTriviaService
    {
        private readonly HttpClient httpClient;
        private const string BaseUrl = "https://opentdb.com/";
        private const string encoding = "base64";

        //https://opentdb.com/api.php?amount=10&category=21&difficulty=easy&type=multiple

        public OpenTriviaService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<OpenTriviaModel> GetQuestions(int amount = 10, string categoryId = "", Difficulty? difficulty = null, TypeEnum? type = null)
        {
            StringBuilder url = new StringBuilder("api.php?");
            url.Append($"amount={amount}");
            if (!string.IsNullOrEmpty(categoryId))
            {
                url.Append($"&category={categoryId}");
            }

            if (difficulty != null)
            {
                url.Append($"&difficulty={difficulty.ToString().ToLower()}");
            }

            if (type != null)
            {
                url.Append($"&type={type.ToString().ToLower()}");
            }

            string? trivia = await this.httpClient.GetStringAsync(url.ToString());
            if (string.IsNullOrEmpty(trivia))
            {
                return new OpenTriviaModel();
            }

            OpenTriviaModel triviaToReturn = Newtonsoft.Json.JsonConvert.DeserializeObject<OpenTriviaModel>(trivia) ?? new OpenTriviaModel();
            return triviaToReturn;
        }

        public async Task<CategoryModel> GetCategories()
        {
            string? categories = await this.httpClient.GetStringAsync("api_category.php");
            if (string.IsNullOrEmpty(categories))
            {
                return new CategoryModel();
            }

            CategoryModel categoriesToReturn = Newtonsoft.Json.JsonConvert.DeserializeObject<CategoryModel>(categories) ?? new CategoryModel();
            return categoriesToReturn;
        }
    }
}
