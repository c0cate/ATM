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

namespace ATM.AdminsForms
{
    public partial class MainWindow2 : Window
    {
        public MainWindow2(string login)
        {
            InitializeComponent();
            this.mainFrame.Navigate(new StartPage(login));
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Title = "Часть администратора";
        }

        /*
        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            // Реализуйте логику при нажатии на кнопку "Клиенты"
        }

        private void OrganisationsButton_Click(object sender, RoutedEventArgs e)
        {
            // Реализуйте логику при нажатии на кнопку "Организации"
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            // Реализуйте логику при нажатии на кнопку "Сотрудники"
        }
        */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try { this.mainFrame.Navigate(new infoPage(((Button)sender).Tag as string ?? throw new Exception("Передана пустая ссылка"))); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void ApplicationsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.mainFrame.Navigate(new applicationPage());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}

