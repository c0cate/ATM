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
    /// Логика взаимодействия для paymentPage2.xaml
    /// </summary>
    public partial class paymentPage2 : Page
    {
        bool isBalanceShowed = true;
        string cardNumber_ = "";
        public paymentPage2()
        {
            InitializeComponent();
        }
            
        public paymentPage2(bool isshowed, string cardNum)
        {
            InitializeComponent();
            isBalanceShowed= isshowed;
            cardNumber_ = cardNum;
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

        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string organisationAdress;
                decimal amount = 0;
                if ((organisationAdress = this.adressTB.Text) != "" && this.adressTB.Text.Length == 16 && ClientHelper.IsOrganisationAdressExists(this.adressTB.Text))
                {
                    if (Helper.isStringNumeric(this.amountTB.Text))
                    {
                        ComboBoxItem selectedItem = currencyComboBox.SelectedItem as ComboBoxItem;
                        if (selectedItem != null)
                        {
                            switch (selectedItem.Content.ToString().Substring(0, 3))
                            {
                                case "BYN":
                                    {
                                        amount = decimal.Parse(this.amountTB.Text);
                                        break;
                                    }
                                case "USD":
                                    {
                                        amount = decimal.Parse(this.amountTB.Text) * 3.28m;
                                        break;
                                    }
                                case "EUR":
                                    {
                                        amount = decimal.Parse(this.amountTB.Text) * 3.48m;
                                        break;
                                    }
                                default:break;
                            }
                            if (amount != 0)
                            {
                                ClientHelper cl = new ClientHelper();
                                cl.UpdateBalance(cardNumber_, decimal.Parse(ClientHelper.getBalance(cardNumber_)) - amount);
                                cl.UpdateBalanceORG(organisationAdress, decimal.Parse(ClientHelper.getBalanceORG(organisationAdress)) + amount);

                                if (isBalanceShowed) this.balanceLabel.Content = $"{ClientHelper.getBalance(cardNumber_)} BYN";
                                MessageBox.Show("Средства успешно переведены!");
                                Window.GetWindow(this).Close();
                            }
                            else MessageBox.Show("что-то пошло не так!");
                        }
                        else MessageBox.Show("выберите валюту!");
                    }
                    else MessageBox.Show("вводимая сумма должна быть числовой!");
                }
                else MessageBox.Show("адрес организации введен некорректно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
