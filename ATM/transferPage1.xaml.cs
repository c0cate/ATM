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
    /// Логика взаимодействия для transferPage1.xaml
    /// </summary>
    public partial class transferPage1 : Page
    {
        Window mainForm = null;
        bool isBalanceShowed = true;
        string cardNumber_ = "";


        public transferPage1(Window mainForm, bool isBalanceShowed, string cardNumber_)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.isBalanceShowed = isBalanceShowed;
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
            this.cardNumber.Content = "**** **** **** " + this.cardNumber_.Substring(12, 4);
        }
        public transferPage1()
        {
            InitializeComponent();
        }


        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientHelper.IsCardNumberExists(cardNumberTBox.Text))
                {
                    NavigationService.Navigate(new transferPage2(mainForm, isBalanceShowed, cardNumberTBox.Text, cardNumber_));
                }
                else
                {
                    MessageBox.Show("не найдена карта с таким номером!");
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.mainForm.Close();
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
            this.cardNumberTBox.Text += btn.Content.ToString();
        }
    }
}
