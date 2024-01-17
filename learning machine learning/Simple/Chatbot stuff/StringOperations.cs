using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learning_machine_learning.Simple
{
    public class StringOperations
    {
        #region Levenshtein
        public static int LevenshteinDistance(string source, string target)
        {
            int sourceLength = source.Length;
            int targetLength = target.Length;

            int[,] distanceMatrix = new int[sourceLength + 1, targetLength + 1];

            // Initialize the first row and column of the matrix
            for (int i = 0; i <= sourceLength; i++)
            {
                distanceMatrix[i, 0] = i;
            }

            for (int j = 0; j <= targetLength; j++)
            {
                distanceMatrix[0, j] = j;
            }

            // Calculate the minimum distance
            for (int i = 1; i <= sourceLength; i++)
            {
                for (int j = 1; j <= targetLength; j++)
                {
                    int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;

                    distanceMatrix[i, j] = Math.Min(
                        Math.Min(distanceMatrix[i - 1, j] + 1, distanceMatrix[i, j - 1] + 1),
                        distanceMatrix[i - 1, j - 1] + cost);
                }
            }

            // Return the Levenshtein distance
            return distanceMatrix[sourceLength, targetLength];
        }
        public static double LevenshteinSimilarity(string source, string target)
        {
            int distance = LevenshteinDistance(source, target);
            int maxLength = Math.Max(source.Length, target.Length);

            return 1 - (double)distance / maxLength;
        }
        #endregion
    }
}
