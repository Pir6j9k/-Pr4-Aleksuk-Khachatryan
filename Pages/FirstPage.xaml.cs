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
using Практическая_работа_4_Алексюк_Хачатрян.Logic;

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
            Count(XTextBox.Text, YTextBox.Text, ZTextBox.Text);
        }

        public bool Count(string X, string Y, string Z)
        {
            try
            {
                if (!FirstPageCalculator.TryParseValues(X, Y, Z, out double x, out double y, out double z))
                {
                    MessageBox.Show("Введите числовые значения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                double result = FirstPageCalculator.Calculate(x, y, z);
                ResultTextBox.Text = result.ToString();
                return true;
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Знаменатель равен нулю! Деление на ноль невозможно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при вычислении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
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