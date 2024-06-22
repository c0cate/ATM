using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    /// Логика взаимодействия для transferPage3.xaml
    /// </summary>
    public partial class transferPage3 : Page
    {
        Window mainForm = null;
        bool isBalanceShowed = true;
        string recieverCardNumber = "";
        string cardNumber_ = "";
        decimal amount = 0;


        public transferPage3(Window mainForm, bool isBalanceShowed, string receiverNum, string cardNum, string amount, decimal number)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.isBalanceShowed = isBalanceShowed;
            this.recieverCardNumber = receiverNum;
            this.cardNumber_ = cardNum;
            this.amount = number;


            this.ownercardNumLabel.Content = cardNum;
            this.receiverCardNumLabel.Content = receiverNum;
            this.amountLabel.Content = amount;


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
        public transferPage3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //кнопка подтверждения
            try
            {
                if (decimal.Parse(ClientHelper.getBalance(cardNumber_)) >= amount)
                {
                    ClientHelper balanceUpdater = new ClientHelper();
                    balanceUpdater.UpdateBalance(cardNumber_, decimal.Parse(ClientHelper.getBalance(cardNumber_)) - amount);
                    balanceUpdater.UpdateBalance(recieverCardNumber, decimal.Parse(ClientHelper.getBalance(recieverCardNumber)) + amount);
                    //balance = ClientHelper.getBalance(cardNumber_);
                    if (isBalanceShowed) this.balanceLabel.Content = $"{ClientHelper.getBalance(cardNumber_)} BYN";
                    MessageBox.Show("Средства успешно переведены!");
                    Window.GetWindow(this).Close();
                }
                else
                {
                    MessageBox.Show("недостаточно средств на счете!");
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //кнопка отмены
            mainForm.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //кнопка перехода на прошу
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
    }
}
