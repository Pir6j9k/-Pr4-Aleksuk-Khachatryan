using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Практическая_работа_4_Алексюк_Хачатрян.Pages
{
    /// <summary>
    /// Логика взаимодействия для SecondPage.xaml
    /// </summary>
    public partial class SecondPage : Page
    {
        public SecondPage()
        {
            InitializeComponent();
            CountBtn.Click += CountBtn_Click;
            CleanBtn.Click += CleanBtn_Click;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            char ch = e.Text[0];
            var tb = (TextBox)sender;
            string text = tb.Text;
            int caret = tb.SelectionStart;


            if (!char.IsDigit(ch) && ch != '-' && ch != '.' && ch != ',')
            {
                e.Handled = true;
                return;
            }


            if (ch == '-')
            {
                if (tb.SelectionStart != 0 || text.Contains("-") )
                {
                    e.Handled = true;
                    return;
                }
            }

            if (ch == '.' || ch == ',')
            {
                if (string.IsNullOrEmpty(text) && caret == 0)
                {
                    e.Handled = true;
                    return;
                }

                if (text.Contains("."))
                {
                    e.Handled = true;
                    return;
                }
            }

            e.Handled = false;
        }

        private void CountBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(XTextBox.Text) ||
                    string.IsNullOrWhiteSpace(MTextBox.Text))
                {
                    MessageBox.Show("Заполните поля x и m.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var ci = CultureInfo.InvariantCulture;
                string xStr = XTextBox.Text.Replace(',', '.');
                string mStr = MTextBox.Text.Replace(',', '.');

                if (!double.TryParse(xStr, NumberStyles.Float, ci, out double x))
                {
                    MessageBox.Show("Некорректное значение x.", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(mStr, NumberStyles.Float, ci, out double m))
                {
                    MessageBox.Show("Некорректное значение m.", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                double fx = CalculateFx(x);

                double xb = x * m;
                double s;

                if (xb > 1 && xb < 10)
                {
                    s = Math.Exp(fx);
                }
                else if (xb > 12 && xb < 40)
                {
                    double underSqrt = Math.Abs(fx) + 4 * m;
                    if (underSqrt < 0)
                    {
                        MessageBox.Show("Подкоренное выражение отрицательно.", "Математическая ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    s = Math.Sqrt(underSqrt);
                }
                else
                {
                    s = m * fx * fx;
                }

                ResultTextBox.Text = s.ToString("G6", ci);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private double CalculateFx(double x)
        {
            if (ShRadio.IsChecked == true)
            {
                return Math.Sinh(x);
            }
            if (SquareRadio.IsChecked == true)
            {
                return x * x;
            }
            if (ExpRadio.IsChecked == true)
            {
                return Math.Exp(x);
            }

            throw new InvalidOperationException("Не выбрана функция f(x).");
        }

        private void CleanBtn_Click(object sender, RoutedEventArgs e)
        {
            XTextBox.Clear();
            MTextBox.Clear();
            ResultTextBox.Clear();
            ShRadio.IsChecked = true; 
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
