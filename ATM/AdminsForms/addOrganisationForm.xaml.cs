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
    /// Логика взаимодействия для addOrganisationForm.xaml
    /// </summary>
    public partial class addOrganisationForm : Window
    {
        public addOrganisationForm()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientHelper.AddOrganisation(nameTB.Text, addressTB.Text, decimal.Zero);
                MessageBox.Show("Организация успешно добавлена!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
