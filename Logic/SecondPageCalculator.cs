using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Практическая_работа_4_Алексюк_Хачатрян.Logic
{
    public enum FxType
    {
        Sinh,
        Square,
        Exp
    }

    public static class SecondPageCalculator
    {
        public static bool TryParseValues(string X, string B, out double x, out double b)
        {
            x = b = 0;
            if (string.IsNullOrWhiteSpace(X) ||
                string.IsNullOrWhiteSpace(B))
            {
                return false;
            }

            var ci = CultureInfo.InvariantCulture;
            X = X.Replace(',', '.');
            B = B.Replace(',', '.');

            if (!double.TryParse(X, NumberStyles.Float, ci, out x))
            {
                return false;
            }

            if (!double.TryParse(B, NumberStyles.Float, ci, out b))
            {
                return false;
            }
            return true;
        }

        public static double CalculateFx(double x, FxType type)
        {
            switch(type)
            {
                case FxType.Sinh: 
                    return Math.Sinh(x);
                case FxType.Square:
                    return x * x;
                case FxType.Exp:
                    return Math.Exp(x);
                default:
                    throw new InvalidOperationException("Функция не выбрана");
            }
        }

        public static double Calculate(double x, double b, FxType type)
        {
            double fx = CalculateFx(x, type);
            double xb = x * b;
            double s;

            if (xb > 1 && xb < 10)
            {
                s = Math.Exp(fx);
            }
            else if (xb > 12 && xb < 40)
            {
                double underSqrt = Math.Abs(fx) + 4 * b;
                if (underSqrt < 0)
                {
                    throw new ArithmeticException("Подкоренное выражение отрицательно.");
                }
                s = Math.Sqrt(underSqrt);
            }
            else
            {
                s = b * fx * fx;
            }
            return s;

        }
    }
}
