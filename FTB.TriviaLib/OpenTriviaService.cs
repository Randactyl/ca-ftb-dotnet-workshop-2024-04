using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public async Task<string> GetToken()
        {
            string? token = await this.httpClient.GetStringAsync("api_token.php?command=request");
            if (string.IsNullOrEmpty(token))
            {
                return string.Empty;
            }

            TokenModel tokenModel = JsonConvert.DeserializeObject<TokenModel>(token) ?? new TokenModel();
            return tokenModel.Token;
        }

        public async Task ResetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            _ = await this.httpClient.GetStringAsync($"api_token.php?command=reset&token={token}");
        }

        public async Task<OpenTriviaModel> GetQuestions(string? token, int amount = 10, string categoryId = "", Difficulty? difficulty = null, TypeEnum? type = null)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (amount <= 0 || amount > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            StringBuilder url = new StringBuilder("api.php?");
            url.Append($"token={token}&amount={amount}");
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

            OpenTriviaModel triviaToReturn = JsonConvert.DeserializeObject<OpenTriviaModel>(trivia) ?? new OpenTriviaModel();
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
