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
using Практическая_работа_4_Алексюк_Хачатрян.Logic;
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
            Count(XTextBox.Text, BTextBox.Text);
        }

        public bool Count(string X, string M)
        {
            try
            {
                if(!SecondPageCalculator.TryParseValues(X, M, out double x, out double m))
                {
                    MessageBox.Show("Введите корректные значения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                FxType type;
                if (ShRadio.IsChecked == true)
                    type = FxType.Sinh;
                else if (SquareRadio.IsChecked == true)
                    type = FxType.Square;
                else if (ExpRadio.IsChecked == true)
                    type = FxType.Exp;
                else
                {
                    MessageBox.Show("Выберите функцию","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                double result = SecondPageCalculator.Calculate(x,m, type);
                     
                ResultTextBox.Text = result.ToString();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void CleanBtn_Click(object sender, RoutedEventArgs e)
        {
            XTextBox.Clear();
            BTextBox.Clear();
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
