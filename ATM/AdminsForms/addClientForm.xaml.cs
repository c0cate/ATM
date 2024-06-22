using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для addClientForm.xaml
    /// </summary>
    public partial class addClientForm : Window
    {
        public addClientForm()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string number = numberTB.Text,
                       date = dateCardTB.Text,
                       owner = ownerNameTB.Text;
                //if (Regex.IsMatch(this.numberTB.Text, @"[0-5]+"/*^(0[1-9]|1[0-2])\/?([0-9]{2})$*/))
                if (!Regex.IsMatch(number, @"\d{16}$")) throw new Exception("некорректный ввод номера карты!");
                else if (!Regex.IsMatch(date, @"^(0[1-9]|1[0-2])(\/|-)([0-9]{2})$")) throw new Exception("некорректный срок действия карты!");
                else
                {
                    string cvc, passwordCard;
                    Random random = new Random();

                    do { cvc = random.Next(0, 999).ToString(); } 
                    while (random.Next(0, 999).ToString().Length != 3);

                    do { passwordCard = random.Next(0, 9999).ToString(); }
                    while (random.Next(0, 9999).ToString().Length != 4);

                    string[] dates = date.Split(new char[] {'\\', '/', '|', ' ', '-' });
                    date = dates[0] + dates[1];

                    ClientHelper.AddClient(number,date,cvc, passwordCard,owner, decimal.Zero);
                    MessageBox.Show("Клиент успешно добавлен!");
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
