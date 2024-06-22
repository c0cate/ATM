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
    /// Логика взаимодействия для settingsForm.xaml
    /// </summary>
    public enum settingsType
    {
        CloseCard,
        ChangeDate,
        ChangePassword,
        ChangeCVC,
        None
    }


    public partial class settingsForm : Window
    {
        bool isBalanceShowed = true;
        string cardNumber_ = "";
        Window main = null;
        public settingsForm()
        {
            InitializeComponent();
        }
        public settingsForm(bool f, string cardNumber_, Window mainWin)
        {
            InitializeComponent();
            this.isBalanceShowed = f;
            this.cardNumber_= cardNumber_;
            this.main = mainWin;
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
            this.cardNumberLabel.Content = "**** **** **** " + this.cardNumber_.Substring(12, 4);
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
            this.Close();
        }




        private void closeCardButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("вы уверены что хотите закрыть свой счет? вы больше не сможете им пользоваться и его нельзя будет восстановить!", "Закрытие счета", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    {
                        //ClientHelper.removeUser(cardNumber_);
                        ClientHelper client = new ClientHelper();
                        client.UpdateBalance(cardNumber_, -1);
                        loginForm login = new loginForm();
                        login.Show();
                        main.Close();
                        this.Close();
                        break;
                    }
                case MessageBoxResult.Cancel: { break; }
                default: break;
            }
        }

        private void changePassButton_Click(object sender, RoutedEventArgs e)
        {
            settingsMiniForm expo = new settingsMiniForm(settingsType.ChangePassword, cardNumber_);
            expo.Show();
        }

        private void changeDateButton_Click(object sender, RoutedEventArgs e)
        {
            settingsMiniForm expo = new settingsMiniForm(settingsType.ChangeDate, cardNumber_);
            expo.Show();
        }

        private void changeCVCButton_Click(object sender, RoutedEventArgs e)
        {
            settingsMiniForm expo = new settingsMiniForm(settingsType.ChangeCVC, cardNumber_);
            expo.Show();
        }
    }
}
