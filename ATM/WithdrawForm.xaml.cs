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
using System.Windows.Shapes;

namespace ATM
{
    /// <summary>
    /// Логика взаимодействия для WithdrawForm.xaml
    /// </summary>
    public partial class WithdrawForm : Window
    {
        private bool isBalanceShowed = true;
        private string balance;
        private string cardNumber_;
        private int withdrawAmount = 0;

        public WithdrawForm()
        {
            InitializeComponent();
        }
        public WithdrawForm(bool isBalanceShowed, string balance, string cardNumber)
        {
            InitializeComponent();
            this.isBalanceShowed = isBalanceShowed;
            this.cardNumber_ = cardNumber;
            this.balance= ClientHelper.getBalance(cardNumber_);
            this.cardNumberLabel.Content = "**** **** **** " + this.cardNumber_.Substring(12, 4);

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

        private void Button5BYN_Click(object sender, RoutedEventArgs e)
        {
            this.Grid5BYN.Background =   new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9D1314"));// красный
            this.Grid10BYN.Background =  new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));//серый
            this.Grid20BYN.Background =  new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));//серый
            this.Grid50BYN.Background =  new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));//серый
            this.Grid100BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));//серый
            this.GridOther.Background =  new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));//серый
            this.textBoxOtherBYN.Text = "";
            this.withdrawAmount = 5;
        }

        private void Button10BYN_Click(object sender, RoutedEventArgs e)
        {
            this.Grid5BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid10BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9D1314"));
            this.Grid20BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid50BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid100BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.GridOther.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.textBoxOtherBYN.Text = "";
            this.withdrawAmount = 10;
        }
        private void Button20BYN_Click(object sender, RoutedEventArgs e)
        {
            this.Grid5BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid10BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid20BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9D1314"));
            this.Grid50BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid100BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.GridOther.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.textBoxOtherBYN.Text = "";
            this.withdrawAmount = 20;
        }

        private void Button50BYN_Click(object sender, RoutedEventArgs e)
        {
            this.Grid5BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid10BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid20BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid50BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9D1314"));
            this.Grid100BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.GridOther.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.textBoxOtherBYN.Text = "";
            this.withdrawAmount = 50;
        }

        private void Button100BYN_Click(object sender, RoutedEventArgs e)
        {
            this.Grid5BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid10BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid20BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid50BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid100BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9D1314"));
            this.GridOther.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.textBoxOtherBYN.Text = "";
            this.withdrawAmount = 100;
        }
        private void textBoxOtherBYN_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Grid5BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid10BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid20BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid50BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.Grid100BYN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBCBCB"));
            this.GridOther.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9D1314"));
            this.withdrawAmount = 0;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (withdrawAmount != 0)
                {
                    ComboBoxItem selectedItem = currencyComboBox.SelectedItem as ComboBoxItem;
                    if (selectedItem != null)
                    {
                        string currency = selectedItem.Content.ToString().Substring(0, 3);
                        decimal scalar = currency == "BYN" ? 1m : (currency == "USD" ? 3.28m : 3.48m);
                        if (withdrawAmount*scalar <= decimal.Parse(ClientHelper.getBalance(cardNumber_)))
                        {

                            ClientHelper balanceUpdater = new ClientHelper();
                            balanceUpdater.UpdateBalance(cardNumber_, decimal.Parse(balance) - withdrawAmount * scalar);
                            balance = ClientHelper.getBalance(cardNumber_);
                            if (isBalanceShowed) this.balanceLabel.Content = $"{ClientHelper.getBalance(cardNumber_)} BYN";
                            //MessageBoxResult res = MessageBox.Show("Средства успешно выведены со счета! Открыть чек?", "Снятие денег",MessageBoxButton.YesNo);
                            if (MessageBox.Show("Средства успешно выведены со счета! Открыть чек?", "Снятие денег", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                                string content = $"Снятие наличных\nСумма: {withdrawAmount*scalar}\nНомер карты: {Helper.FormatString(cardNumber_)}\nБаланс: {ClientHelper.getBalance(cardNumber_)}\nДата: {DateTime.Now.ToShortDateString()}";
                                string filePath = Helper.removeAllSPaces(ClientHelper.getOwnerName(cardNumber_).Trim() + DateTime.Now.ToString());
                                PdfCreator.CreatePdf(content,filePath);
                                Process.Start(new ProcessStartInfo($"D:\\!studying\\!C#\\ATM — копия\\ATM\\checks\\{filePath}.pdf") { UseShellExecute = true });
                            }
                            //this.Close();
                        }
                        else MessageBox.Show("на балансе недостаточно средств!");
                    }
                    else MessageBox.Show("выберите валюту!");
                }
                else if (!(this.textBoxOtherBYN.Text == ""))
                {
                    bool f = true;
                    foreach (char item in this.textBoxOtherBYN.Text) if (!char.IsDigit(item)) f = false;
                    if (f)
                    {
                        ComboBoxItem selectedItem = currencyComboBox.SelectedItem as ComboBoxItem;
                        if(selectedItem != null)
                        {
                            string currency = selectedItem.Content.ToString().Substring(0,3);
                            decimal scalar = currency == "BYN"? 1m: (currency == "USD"?3.28m: 3.48m);
                            if (decimal.Parse(this.textBoxOtherBYN.Text) % 5 == 0)
                            {
                                if (decimal.Parse(this.textBoxOtherBYN.Text)*scalar <= decimal.Parse(ClientHelper.getBalance(cardNumber_)))
                                {

                                    if (!Helper.isStringNumeric(balance)) balance = balance.Remove(balance.Length - 4, 4);
                                    
                                    ClientHelper balanceUpdater = new ClientHelper();                                    
                                    balanceUpdater.UpdateBalance(cardNumber_, decimal.Parse(balance) - decimal.Parse(this.textBoxOtherBYN.Text) * scalar);
                                    balance = ClientHelper.getBalance(cardNumber_);
                                    if (isBalanceShowed) this.balanceLabel.Content = $"{ClientHelper.getBalance(cardNumber_)} BYN";
                                    if (MessageBox.Show("Средства успешно выведены со счета! Открыть чек?", "Снятие денег", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                    {
                                        string content = $"Снятие наличных\nСумма: {withdrawAmount * scalar}\nНомер карты: {Helper.FormatString(cardNumber_)}\nБаланс: {ClientHelper.getBalance(cardNumber_)}\nДата: {DateTime.Now.ToShortDateString()}";
                                        string filePath = Helper.removeAllSPaces(ClientHelper.getOwnerName(cardNumber_).Trim() + DateTime.Now.ToString());
                                        PdfCreator.CreatePdf(content, filePath);
                                        Process.Start(new ProcessStartInfo($"D:\\!studying\\!C#\\ATM — копия\\ATM\\checks\\{filePath}.pdf") { UseShellExecute = true });
                                    }
                                    //this.Close();
                                }
                                else MessageBox.Show("на балансе недостаточно средств!");
                            }
                            else
                            {
                                MessageBox.Show("сумма вывода должна быть кратна 5!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("выберите валюту!");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("введите число для вывода наличных!");
                        this.textBoxOtherBYN.Text = "";
                        return;
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
