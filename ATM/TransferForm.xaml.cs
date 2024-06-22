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
    /// Логика взаимодействия для TransferForm.xaml
    /// </summary>
    public partial class TransferForm : Window
    {
        public TransferForm()
        {
            InitializeComponent();
            this.transferMainFrame.Content = new transferPage1(this, true, "");
        }
        public TransferForm(bool balanceShowed, string cardNumber, string balance)
        {
            InitializeComponent();
            this.transferMainFrame.Content = new transferPage1(this, balanceShowed, cardNumber);
        }
    }
}
