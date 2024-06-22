using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATM
{
	/// <summary>
	/// Логика взаимодействия для phonePaymentPage.xaml
	/// </summary>
	public partial class phonePaymentPage : Page
	{
		bool isBalanceShowed = true;
		string cardNumber_ = "";

        public phonePaymentPage(bool isShowed, string cardNumber_)
		{
			InitializeComponent();
			this.isBalanceShowed = isShowed;
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
        private void currencyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBoxItem item = this.currencyComboBox.SelectedItem as ComboBoxItem;
			if (item != null)
			{
				switch (item.Content)
				{
					case "Life :)":
						{
							this.adressTB.Text = "+375 (25) ";
                            #region
                            //// Создайте новый объект BitmapImage
                            //BitmapImage bitmap = new BitmapImage();

                            //// Установите UriSource на путь к вашему изображению
                            //bitmap.BeginInit();
                            //bitmap.UriSource = new Uri(@"D:\!studying\!C#\ATM — копия\ATM\Resources\Life_logo.png");
                            //bitmap.EndInit();

                            //// Установите Source вашего элемента Image
                            //logoImage.Source = bitmap;
                            #endregion
                            break;
						}
					case "Velcom":
						{
                            this.adressTB.Text = "+375 (29) ";
                            break;
						}
					case "MTC":
						{
                            this.adressTB.Text = "+375 (33) ";
                            break;
						}
					case "A1":
						{
                            this.adressTB.Text = "+375 (44) ";
                            break;
						}
				}
			}
		}

        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Regex.IsMatch(this.adressTB.Text, @"^(\+375)\s\((29|25|44|33)\)\s\d{3}(\s|-)?\d{2}(\s|-)?\d{2}$"))
                {
                    if (Helper.isStringNumeric(this.amountTB.Text))
                    {
                        decimal amount = decimal.Parse(this.amountTB.Text);
                        if (amount <= decimal.Parse(ClientHelper.getBalance(cardNumber_)))
                        {
                            ClientHelper balanceUpdater = new ClientHelper();
                            balanceUpdater.UpdateBalance(cardNumber_, decimal.Parse(ClientHelper.getBalance(cardNumber_)) - amount);
                            //balance = ClientHelper.getBalance(cardNumber_);
                            if (isBalanceShowed) this.balanceLabel.Content = $"{ClientHelper.getBalance(cardNumber_)} BYN";
                            MessageBox.Show("Средства успешно выведены со счета!");
                        }
                        else MessageBox.Show("на балансе недостаточно средств!");
                    }
                    else MessageBox.Show("значение перевода должно быть числовым!");
                }
                else MessageBox.Show("номер введет неккорректно!");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
			NavigationService.GoBack();
        }
    }
}
