using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDCA.Utils
{
    public class AnswerYNComparer : IComparer<(string Answer, int AnswerNumber)>
    {
        public int Compare((string Answer, int AnswerNumber) x, (string Answer, int AnswerNumber) y)
        {
            // Handle answers starting with "Yes" or "yes"
            bool xStartsWithYes = x.Answer.StartsWith("Yes", StringComparison.OrdinalIgnoreCase);
            bool yStartsWithYes = y.Answer.StartsWith("Yes", StringComparison.OrdinalIgnoreCase);

            // Handle answers starting with "No" or "no"
            bool xStartsWithNo = x.Answer.StartsWith("No", StringComparison.OrdinalIgnoreCase);
            bool yStartsWithNo = y.Answer.StartsWith("No", StringComparison.OrdinalIgnoreCase);

            // Compare answers starting with "Yes" and "No"
            if (xStartsWithYes && !yStartsWithYes)
            {
                return -1; // x comes before y when x starts with "Yes" and y does not
            }
            else if (!xStartsWithYes && yStartsWithYes)
            {
                return 1; // y comes before x when y starts with "Yes" and x does not
            }

            if (xStartsWithNo && !yStartsWithNo)
            {
                return -1; // x comes before y when x starts with "No" and y does not
            }
            else if (!xStartsWithNo && yStartsWithNo)
            {
                return 1; // y comes before x when y starts with "No" and x does not
            }

            // If neither x nor y start with "Yes" or "No," maintain the original order
            return 0;
        }
    }
}