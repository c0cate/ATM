using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace ATM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string cardNumber = "", balance = "", dateCard = "", cvc = "", password = "", ownerName = "";
        private bool isBalanceShowed = true;
        public MainWindow(string cN, string balance, string datecard, string cvc, string pass, string owner)
        {
            try
            {
                InitializeComponent();
                //
                this.cardNumber = cN;
                this.balance = ClientHelper.getBalance(cardNumber);
                this.dateCard = datecard;
                this.cvc = cvc;
                this.password = pass;
                this.ownerName = owner;
                //
                this.cardNumberLabel.Content = "**** **** **** " + this.cardNumber.Substring(12, 4);
                this.balanceLabel.Content = balance = $"{ClientHelper.getBalance(cardNumber)} BYN";
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        #region
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
        #endregion

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HideBalance_Click(object sender, RoutedEventArgs e)
        {
           
            if (isBalanceShowed)
            {
                this.balanceLabel.Content = "* *** BYN";
                this.hideBalanceLabel.Content = "Показать остаток";
                this.hideBalanceLabel.FontSize -= 1;
            }
            else
            {
                this.balanceLabel.Content = balance = $"{ClientHelper.getBalance(cardNumber)} BYN";
                this.hideBalanceLabel.Content = "Скрыть остаток";
                this.hideBalanceLabel.FontSize = 20;

            }
            this.isBalanceShowed = !isBalanceShowed;
        }





        private void transferButton_Click(object sender, RoutedEventArgs e)
        {
            TransferForm expo = new TransferForm(isBalanceShowed, cardNumber, balance);
            expo.ShowDialog();
        }

        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            infoForm expo = new infoForm(cardNumber, dateCard, cvc);
            expo.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string helpFilePath = "D:\\!studying\\!C#\\ATM — копия\\Справка\\index.htm"; Process.Start(new ProcessStartInfo(helpFilePath) { UseShellExecute = true });
        }

        private void withdrawButton_Click(object sender, RoutedEventArgs e)
        {
            WithdrawForm expo = new WithdrawForm(isBalanceShowed, balance, cardNumber);
            expo.ShowDialog();
        }

        private void paymentButton_Click(object sender, RoutedEventArgs e)
        {
            PaymentForm expo = new PaymentForm(isBalanceShowed, cardNumber);
            expo.ShowDialog();
        }

        private void offerButton_Click(object sender, RoutedEventArgs e)
        {
            offerForm expo = new offerForm(isBalanceShowed, cardNumber);
            expo.ShowDialog();
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            settingsForm expo = new settingsForm(isBalanceShowed, cardNumber, this);
            expo.ShowDialog();
        }

        private void partnersButton_Click(object sender, RoutedEventArgs e)
        {
            partnersForm expo = new partnersForm(isBalanceShowed, cardNumber);
            expo.ShowDialog();
        }
    }
}
