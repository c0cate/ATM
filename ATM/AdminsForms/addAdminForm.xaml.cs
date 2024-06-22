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
using System.Windows.Shapes;

namespace ATM.AdminsForms
{
    /// <summary>
    /// Логика взаимодействия для addAdminForm.xaml
    /// </summary>
    public partial class addAdminForm : Window
    {
        public addAdminForm()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientHelper.AddAdmin(nameTB.Text, surnameTB.Text, loginTB.Text, passwordTB.Text, contactTB.Text);
                MessageBox.Show("Администратор успешно добавлен!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
