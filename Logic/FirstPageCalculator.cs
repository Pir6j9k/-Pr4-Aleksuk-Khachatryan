using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Практическая_работа_4_Алексюк_Хачатрян.Logic
{
    public static class FirstPageCalculator
    {
        public static bool TryParseValues(string X, string Y, string Z, out double x, out double y, out double z)
        {
            x = y = z = 0;
            if (string.IsNullOrWhiteSpace(X) ||
                string.IsNullOrWhiteSpace(Y) ||
                string.IsNullOrWhiteSpace(Z))
                return false;

            if (!double.TryParse(X.Replace('.', ','), out x))
            {
                return false;
            }

            if (!double.TryParse(Y.Replace('.', ','), out y))
            {
                return false;
            }

            if (!double.TryParse(Z.Replace('.', ','), out z))
            {
                return false;
            }
            return true;
        }

        public static double Calculate(double x, double y, double z)
        {
            double denominator = Math.Abs(x - y) * z + Math.Pow(x, 2);
            if (denominator == 0)
            {
                throw new DivideByZeroException("Знаменатель равен нулю! Деление на ноль невозможно.");
            }

            double arctgX = Math.Atan(x);
            double absXY = Math.Abs(x - y);

            double numerator = x + 3 * absXY + Math.Pow(x, 2);

            double fraction = numerator / denominator;

            double result = 5 * arctgX - (1.0 / 4.0) * arctgX * fraction;
            return result;
        }
    }
}