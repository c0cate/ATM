using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для offerForm.xaml
    /// </summary>
    public partial class offerForm : Window
    {
        private bool isBalanceShowed = true;
        private string cardNumber_ = "";
        public offerForm()
        {
            InitializeComponent();
        }
        public offerForm(bool f, string cardNum)
        {
            InitializeComponent();
            isBalanceShowed = f;
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

        private void makeAnOfferButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = $@"D:\!studying\!C#\ATM — копия\ATM\offers\{nameTB.Text}.txt";

                if (!File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        using (StreamWriter sr = new StreamWriter(fs))
                        {
                            sr.WriteLine(nameTB.Text);
                            sr.WriteLine(descriptionTB.Text);
                            sr.WriteLine(birthdayTB.Text);
                            sr.WriteLine(workingXpTB.Text);
                            sr.WriteLine(phoneTB.Text);
                        }
                    }
                    MessageBox.Show("Ваша заявка успешно сохранена. Спасибо!");
                }
                else { MessageBox.Show("Заявка с данным именем уже подана!"); }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
