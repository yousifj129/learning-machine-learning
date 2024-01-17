using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learning_machine_learning.Simple.Chatbot_stuff
{
    internal class ChatbotLevenshtein : SimpleChatbot
    {
        public string[] questions;
        public string[] answers;

        public ChatbotLevenshtein(string[] questions1, string[] answers1)
        {
            questions = questions1;
            answers = answers1;
        }
        public ChatbotLevenshtein()
        {
            questions = new string[0];
            answers = new string[0];
        }
        public void LoadDataFromCSV(string location)
        {
            List<string> loadedQuestions = new List<string>();
            List<string> loadedAnswers = new List<string>();

            using (var reader = new StreamReader(location))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length >= 2)
                    {
                        loadedQuestions.Add(values[0]);
                        loadedAnswers.Add(values[1]);
                    }
                }
            }

            questions = loadedQuestions.ToArray();
            answers = loadedAnswers.ToArray();
        }
        public string generate(string c)
        {
            double maxSimilarity = 0;
            int mostSimilarIndex = -1;

            for (int i = 0; i < questions.Length; i++)
            {
                double similarity = StringOperations.LevenshteinSimilarity(c, questions[i]);
                if (similarity > maxSimilarity)
                {
                    maxSimilarity = similarity;
                    mostSimilarIndex = i;
                }
            }

            if (mostSimilarIndex != -1)
            {
                return answers[mostSimilarIndex];
            }

            // If no similar question was found, return a default response
            return "I'm sorry, I don't have an answer to that question.";
        }
    }
}
