using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практическая_работа_4_Алексюк_Хачатрян.Logic
{
    public static class ThirdPageCalculator
    {
        public const double X0 = 2.4;
        public const double Xk = 4.0;
        public const double B = 2.3;

        public static bool TryParseValue(string X, out double x)
        {
            x = 0;

            if (string.IsNullOrWhiteSpace(X))
                return false;

            var ci = CultureInfo.InvariantCulture;

            X = X.Replace(',', '.');

            if (!double.TryParse(X, NumberStyles.Float, ci, out x))
                return false;

            return true;
        }

        public static bool IsInRange(double x)
        {
            return x >= X0 && x <= Xk;
        }

        public static double Calculate(double x)
        {
            if (!IsInRange(x))
                throw new ArgumentOutOfRangeException("x вне допустимого диапазона");

            double inner = Math.Pow(x, 3) + Math.Pow(B, 3);

            if (inner < 0)
                throw new ArithmeticException("Подкоренное выражение отрицательно.");

            double y = 9 * (x + 15 * Math.Sqrt(inner));

            return y;
        }
    }
}
