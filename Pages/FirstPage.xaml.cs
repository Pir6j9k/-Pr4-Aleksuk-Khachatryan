using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        public FirstPage()
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
                if (tb.SelectionStart != 0 || text.Contains("-"))
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
                if (string.IsNullOrWhiteSpace(XTextBox.Text) ||
                    string.IsNullOrWhiteSpace(YTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ZTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!double.TryParse(XTextBox.Text.Replace('.', ','), out double x))
                {
                    MessageBox.Show("Некорректное значение X!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(YTextBox.Text.Replace('.', ','), out double y))
                {
                    MessageBox.Show("Некорректное значение Y!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(ZTextBox.Text.Replace('.', ','), out double z))
                {
                    MessageBox.Show("Некорректное значение Z!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                double denominator = Math.Abs(x - y) * z + Math.Pow(x, 2);
                if (denominator == 0)
                {
                    MessageBox.Show("Знаменатель равен нулю! Деление на ноль невозможно.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                double arctgX = Math.Atan(x);
                double absXY = Math.Abs(x - y);

                double numerator = x + 3 * absXY + Math.Pow(x, 2);

                double fraction = numerator / denominator;

                double result = 5 * arctgX - (1.0 / 4.0) * arctgX * fraction;

                ResultTextBox.Text = result.ToString("F4");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при вычислении: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CleanBtn_Click(object sender, RoutedEventArgs e)
        {
            XTextBox.Clear();
            YTextBox.Clear();
            ZTextBox.Clear();
            ResultTextBox.Clear();

            XTextBox.Focus();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}