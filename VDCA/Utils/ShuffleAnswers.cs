using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDCA.Models;

namespace VDCA.Utils
{
    public static class ShuffleAnswers
    {
        public static List<Questions> SortAnswers(List<Questions> In)
        {
            foreach (Questions questions in In)
            {
                int numAnswers = 0;
                if (questions.Answer1.Length > 0) numAnswers++;
                if (questions.Answer2.Length > 0) numAnswers++;
                if (questions.Answer3.Length > 0) numAnswers++;
                if (questions.Answer4.Length > 0) numAnswers++;
                questions.Answers = new List<(string, int)>(numAnswers); //Pre-allocate
                if (questions.Answer1.Length > 0) questions.Answers.Add((questions.Answer1, 1));
                if (questions.Answer2.Length > 0) questions.Answers.Add((questions.Answer2, 2));
                if (questions.Answer3.Length > 0) questions.Answers.Add((questions.Answer3, 3));
                if (questions.Answer4.Length > 0) questions.Answers.Add((questions.Answer4, 4));
                questions.NumberOfAnswer = numAnswers;
                RandomizeList(questions.Answers);
                //When Checking for All of the above or None of the above there is no need to check the last item in the array as if that one
                //contains the string then it's all ready in the right place mdail 6-8-23
                //Changed from using contains so I could make sure the comparison match even if the case didn't match mdail 9-20-23               
                questions.Answers.Sort(new AnswerYNComparer());
                questions.Answers.Sort(new AnswerANBComparer());
                questions.Answers.Sort(new AnswerTFComparer());
            }
            return In;
        }

        private static void RandomizeList<T>(List<T> list)
        {
            Random rng = new();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            int y = list.Count;
        }
    }
}
