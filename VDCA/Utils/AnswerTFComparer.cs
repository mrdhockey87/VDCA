using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDCA.Utils
{
    public class AnswerTFComparer : IComparer<(string Answer, int AnswerNumber)>
    {
        public int Compare((string Answer, int AnswerNumber) x, (string Answer, int AnswerNumber) y)
        {
            // Handle answers equal to "True" or "true"
            bool xIsTrue = string.Equals(x.Answer, "True", StringComparison.OrdinalIgnoreCase);
            bool yIsTrue = string.Equals(y.Answer, "True", StringComparison.OrdinalIgnoreCase);

            // Handle answers equal to "False" or "false"
            bool xIsFalse = string.Equals(x.Answer, "False", StringComparison.OrdinalIgnoreCase);
            bool yIsFalse = string.Equals(y.Answer, "False", StringComparison.OrdinalIgnoreCase);

            // Compare answers equal to "True" and "False"
            if (xIsTrue && !yIsTrue)
            {
                return -1; // x comes before y when x is "True" and y is not
            }
            else if (!xIsTrue && yIsTrue)
            {
                return 1; // y comes before x when y is "True" and x is not
            }

            if (xIsFalse && !yIsFalse)
            {
                return -1; // x comes before y when x is "False" and y is not
            }
            else if (!xIsFalse && yIsFalse)
            {
                return 1; // y comes before x when y is "False" and x is not
            }

            // If neither x nor y are "True" or "False," maintain the original order
            return 0;
        }
    }
}
