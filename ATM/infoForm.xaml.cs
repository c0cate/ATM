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
    /// Логика взаимодействия для infoForm.xaml
    /// </summary>
    public partial class infoForm : Window
    {
        public infoForm(string cardNum, string date, string cvc)
        {
            InitializeComponent();
            this.cardNumberLabel.Content += Helper.FormatString(cardNum);
            this.dateLabel.Content += $"{date.Substring(0, 2)}/{date.Substring(2, 2)}";
            this.cvcLabel.Content += cvc;
            this.balanceLabel.Content += $"{ClientHelper.getBalance(cardNum)} BYN";
        }
    }
}
