using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDCA.Utils
{
    public class AnswerANBComparer : IComparer<(string Answer, int AnswerNumber)>
    {
        public int Compare((string Answer, int AnswerNumber) x, (string Answer, int AnswerNumber) y)
        {
            // Define the phrases to check for
            string[] phrases = { "All of these", "None of these", "Both of these" };

            // Check if x and y contain any of the phrases (case-insensitive)
            bool xContainsPhrase = phrases.Any(phrase => x.Answer.StartsWith(phrase, StringComparison.OrdinalIgnoreCase));
            bool yContainsPhrase = phrases.Any(phrase => y.Answer.StartsWith(phrase, StringComparison.OrdinalIgnoreCase));

            // Handle the presence of "All of these" or "Both of these"
            if (xContainsPhrase && (x.Answer.StartsWith("All of these", StringComparison.OrdinalIgnoreCase) || x.Answer.StartsWith("Both of these", StringComparison.OrdinalIgnoreCase)))
            {
                return 1; // x comes after y when "All of these" or "Both of these" is present in x
            }
            else if (yContainsPhrase && (y.Answer.StartsWith("All of these", StringComparison.OrdinalIgnoreCase) || y.Answer.StartsWith("Both of these", StringComparison.OrdinalIgnoreCase)))
            {
                return -1; // y comes after x when "All of these" or "Both of these" is present in y
            }
            // Handle the presence of "None of these"
            if (xContainsPhrase && x.Answer.StartsWith("None of these", StringComparison.OrdinalIgnoreCase))
            {
                return 1; // x comes after y when "None of these" is present in x
            }
            else if (yContainsPhrase && y.Answer.StartsWith("None of these", StringComparison.OrdinalIgnoreCase))
            {
                return -1; // y comes after x when "None of these" is present in y
            }

            // If neither x nor y contain any of the specified phrases or options, maintain the original order
            return 0; // x.AnswerNumber.CompareTo(y.AnswerNumber);
        }
    }
}
