using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace ATM
{
    /// <summary>
    /// Логика взаимодействия для settingsMiniForm.xaml
    /// </summary>
    public partial class settingsMiniForm : Window
    {
        settingsType type = settingsType.None;
        string cardNumber_ = "";
        public settingsMiniForm(settingsType type, string cardNumber)
        {
            InitializeComponent();
            this.type = type;
            this.cardNumber_ = cardNumber;

            switch (type)
            {
                case settingsType.ChangeDate:
                    {
                        this.titleLabel.Content = "введите новый срок";
                        this.numberTB.Text = "";
                        break;
                    }
                case settingsType.ChangeCVC:
                    {
                        this.titleLabel.Content = "введите пароль карточки";
                        this.numberTB.Text = "";
                        break;
                    }
                case settingsType.ChangePassword:
                    {
                        this.titleLabel.Content = "введите новый пароль";
                        this.numberTB.Text = "";
                        break;
                    }
            }
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            ClientHelper updater = new ClientHelper();
            switch (type)
            {
                case settingsType.ChangeDate:
                    {
                        if (Regex.IsMatch(this.numberTB.Text, @"[0-5]+"/*^(0[1-9]|1[0-2])\/?([0-9]{2})$*/))
                        {
                            switch(MessageBox.Show("стоимость продления будет состовлять 15 рублей. Вы согласны?", "Оплата", MessageBoxButton.OKCancel))
                            {
                                case MessageBoxResult.OK:
                                    {
                                        if (decimal.Parse(ClientHelper.getBalance(cardNumber_)) >= 15)
                                        {
                                            updater.UpdateBalance(cardNumber_, decimal.Parse(ClientHelper.getBalance(cardNumber_)) - 15);
                                            int dateInt = int.Parse(ClientHelper.getDate(cardNumber_))+int.Parse(this.numberTB.Text);
                                            ClientHelper.UpdateDate(cardNumber_, dateInt.ToString());
                                            //string date = $"{this.numberTB.Text.ToString().Substring(0, 2)}/{this.numberTB.Text.ToString().Substring(2, 2)}";
                                            MessageBox.Show($"Срок действия карты успешно изменен на {Helper.formatStringDate(ClientHelper.getDate(cardNumber_))}");
                                            this.Close();
                                        }
                                        else
                                        {
                                            MessageBox.Show("на балансе недостаточно средств!");
                                            this.Close();
                                        }
                                        break;
                                    }
                                case MessageBoxResult.Cancel:
                                    {
                                        this.Close();
                                        break;
                                    }
                                default: break;
                            }
                        }
                        else MessageBox.Show("срок действия указан некорректно!");
                        break;
                    }
                    
                case settingsType.ChangeCVC:
                    {
                        string password = this.numberTB.Text;
                        if (ClientHelper.chackPassword(password, cardNumber_))
                        {
                            MessageBox.Show($"Ваш номер платежной системы: {ClientHelper.getCVC(cardNumber_)}.\nНе сообщайте его никому!");
                        }
                        else MessageBox.Show("пароль введен неверно!");
                        
                        break;
                    }
                case settingsType.ChangePassword:
                    {
                        if (Regex.IsMatch(this.numberTB.Text, @"^\d{4}$"))
                        {
                            switch (MessageBox.Show("стоимость смены будет состовлять 15 рублей. Вы согласны?", "Оплата", MessageBoxButton.OKCancel))
                            {
                                case MessageBoxResult.OK:
                                    {
                                        if (decimal.Parse(ClientHelper.getBalance(cardNumber_)) >= 15)
                                        {
                                            updater.UpdateBalance(cardNumber_, decimal.Parse(ClientHelper.getBalance(cardNumber_)) - 15);
                                            ClientHelper.UpdatePassword(cardNumber_, this.numberTB.Text);
                                            MessageBox.Show($"пароль успешно изменен на {this.numberTB.Text}");
                                            this.Close();
                                        }
                                        else
                                        {
                                            MessageBox.Show("на балансе недостаточно средств!");
                                            this.Close();
                                        }
                                        break;
                                    }
                                case MessageBoxResult.Cancel:
                                    {
                                        this.Close();
                                        break;
                                    }
                            }
                        }
                        else MessageBox.Show("пароль указан некорректно!");
                        break;
                    }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
