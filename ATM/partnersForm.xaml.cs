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

namespace ATM
{
    /// <summary>
    /// Логика взаимодействия для partnersForm.xaml
    /// </summary>
    public partial class partnersForm : Window
    {
        private bool isBalanceShowed = true;
        private string cardNumber_ = "";
        public partnersForm()
        {
            InitializeComponent();
        }
        public partnersForm(bool isshowed, string cardNumber_)
        {
            InitializeComponent();
            isBalanceShowed = isshowed;
            this.cardNumber_ = cardNumber_;
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
            this.cardNumber.Content = "**** **** **** " + cardNumber_.Substring(12, 4);
        }


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
                this.balanceLabel.Content = $"{ClientHelper.getBalance(cardNumber_)} BYN";
                this.hideBalanceLabel.Content = "Скрыть остаток";
                this.hideBalanceLabel.FontSize = 20;

            }
            this.isBalanceShowed = !isBalanceShowed;
        }
    }
}
