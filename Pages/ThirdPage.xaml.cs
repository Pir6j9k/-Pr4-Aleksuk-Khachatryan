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

namespace Практическая_работа_4_Алексюк_Хачатрян.Pages
{
    /// <summary>
    /// Логика взаимодействия для ThirdPage.xaml
    /// </summary>
    public partial class ThirdPage : Page
    {
        private const double X0 = 2.4;
        private const double Xk = 4.0;
        private const double Dx = 0.2;
        private const double B = 2.3;
        public ThirdPage()
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
                if (caret != 0 || text.Contains("-"))
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

                if (text.Contains(".") || text.Contains(","))
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
                if (string.IsNullOrWhiteSpace(XTextBox.Text))
                {
                    MessageBox.Show("Введите значение x.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var ci = CultureInfo.InvariantCulture;
                string xStr = XTextBox.Text.Replace(',', '.');

                if (!double.TryParse(xStr, NumberStyles.Float, ci, out double x))
                {
                    MessageBox.Show("Некорректное значение x.", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (x < X0 || x > Xk)
                {
                    MessageBox.Show(
                        $"x должен быть в диапазоне от {X0} до {Xk}.",
                        "Выход за пределы",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                double inner = Math.Pow(x, 3) + Math.Pow(B, 3);
                if (inner < 0)
                {
                    MessageBox.Show("Подкоренное выражение отрицательно.",
                        "Математическая ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                double y = 9 * (x + 15 * Math.Sqrt(inner));
                ResultTextBox.Text = y.ToString("G6", ci);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void CleanBtn_Click(object sender, RoutedEventArgs e)
        {
            XTextBox.Clear();
            ResultTextBox.Clear();
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
