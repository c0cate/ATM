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
    /// Логика взаимодействия для transferPage2.xaml
    /// </summary>
    public partial class transferPage2 : Page
    {
        Window mainForm = null;
        bool isBalanceShowed = true;
        string recieverCardNumber = "";
        string cardNumber_ = "";

        public transferPage2(Window mainForm, bool isBalanceShowed, string recieverCardNumber, string cardNumber)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.isBalanceShowed = isBalanceShowed;
            this.recieverCardNumber = recieverCardNumber;
            this.cardNumber_ = cardNumber;

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
        public transferPage2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Helper.isStringNumeric(this.transferAmountTBox.Text))
            {
                NavigationService.Navigate(new transferPage3(mainForm, isBalanceShowed, recieverCardNumber, cardNumber_, $"{transferAmountTBox.Text} USD ({decimal.Parse(transferAmountTBox.Text) * 3.28m} BYN)",decimal.Parse(transferAmountTBox.Text)*3.28m));
            }
            else
            {
                MessageBox.Show("вхожная строка должна быть числовой!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Helper.isStringNumeric(this.transferAmountTBox.Text))
            {
                NavigationService.Navigate(new transferPage3(mainForm, isBalanceShowed, recieverCardNumber, cardNumber_, $"{transferAmountTBox.Text} BYN",decimal.Parse(transferAmountTBox.Text)));
            }
            else
            {
                MessageBox.Show("вхожная строка должна быть числовой!");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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

        private void keyboard_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            this.transferAmountTBox.Text += btn.Content.ToString();
        }
    }
}
