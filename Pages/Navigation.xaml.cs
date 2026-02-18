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

namespace Практическая_работа_4_Алексюк_Хачатрян.Pages
{
    /// <summary>
    /// Логика взаимодействия для Navigation.xaml
    /// </summary>
    public partial class Navigation : Page
    {
        public Navigation()
        {
            InitializeComponent();
        }

        private void FirstPageBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.FirstPage());
        }

        private void SecondPageBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.SecondPage());
        }

        private void ThirdPageBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ThirdPage());
        }
    }
}
