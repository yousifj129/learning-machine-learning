using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                        loadedQuestions.Add(Tokenize(values[0]));
                        loadedAnswers.Add(Tokenize(values[1]));
                    }
                }
            }

            questions = loadedQuestions.ToArray();
            answers = loadedAnswers.ToArray();
        }
        public static string Tokenize(string input)
        {
            // Remove special characters using regular expression
            string cleanedString = Regex.Replace(input, @"[!@?""'']", string.Empty);

            // Convert to lowercase and remove leading/trailing whitespaces
            string tokenizedString = cleanedString.ToLower().Trim();

            return tokenizedString;
        }
        public string generate(string question)
        {
            double maxSimilarity = 0;
            int mostSimilarIndex = -1;
            string inp = Tokenize(question);

            for (int i = 0; i < questions.Length; i++)
            {
                double similarity = StringOperations.LevenshteinSimilarity(inp, questions[i]);
                if (similarity > maxSimilarity)
                {
                    maxSimilarity = similarity;
                    mostSimilarIndex = i;
                }
            }

            if (mostSimilarIndex != -1)
            {
                return Tokenize( answers[mostSimilarIndex]);
            }

            // If no similar question was found, return a default response
            return "I'm sorry, I don't have an answer to that question.";
        }

        public static void sample()
        {
            ChatbotLevenshtein chatbot = new ChatbotLevenshtein();
            chatbot.LoadDataFromCSV("Test1.csv");

            while (true)
            {
                Console.WriteLine("you: ");
                string q = Console.ReadLine();
                Console.WriteLine("The Bot: ");
                string a = chatbot.generate(q);
                Console.WriteLine(a);
            }
        }
    }
}
