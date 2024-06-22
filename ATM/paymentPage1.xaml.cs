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

namespace ATM
{
    /// <summary>
    /// Логика взаимодействия для paymentPage1.xaml
    /// </summary>
    public partial class paymentPage1 : Page
    {
        private Window mainForm = null;
        private bool isBalanceShowed = true;
        string cardNumber_ = "";

        public paymentPage1()
        {
            InitializeComponent();
        }
        public paymentPage1(Window mainWin, bool isBalance, string cardNum)
        {
            InitializeComponent();
            mainForm= mainWin;
            this.isBalanceShowed = isBalance;
            this.cardNumber_ = cardNum;

            if (!this.isBalanceShowed)
            {
                this.balanceLabel.Content = "* *** BYN";
                this.hideBalanceLabel.Content = "Показать остаток";
                this.hideBalanceLabel.FontSize -= 1;
            }
            else
            {
                this.balanceLabel.Content = $"{ClientHelper.getBalance(cardNumber_)} BYN";
                this.hideBalanceLabel.Content = "Скрыть остаток";
                this.hideBalanceLabel.FontSize = 20;
            }
            this.cardNumber.Content = "**** **** **** " + this.cardNumber_.Substring(12, 4);

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
                this.balanceLabel.Content = $"{ClientHelper.getBalance(cardNumber_)} BYN";
                this.hideBalanceLabel.Content = "Скрыть остаток";
                this.hideBalanceLabel.FontSize = 20;

            }
            this.isBalanceShowed = !isBalanceShowed;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.mainForm.Close();
        }
        
        private void buttonSelect_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new paymentPage2(isBalanceShowed, cardNumber_));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new phonePaymentPage(isBalanceShowed, cardNumber_));
        }
    }
}
