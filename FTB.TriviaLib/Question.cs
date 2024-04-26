using System.Collections.Generic;
using Newtonsoft.Json;

namespace FTB.TriviaLib
{
    public class Question
    {
        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("difficulty")]
        public Difficulty Difficulty { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; } = string.Empty;

        [JsonProperty("question")]
        public string QuestionText { get; set; } = string.Empty;

        [JsonProperty("correct_answer")]
        public string CorrectAnswer { get; set; } = string.Empty;

        [JsonProperty("incorrect_answers")]
        public List<string> IncorrectAnswers { get; set; } = new List<string>();
    }
}