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
    /// Логика взаимодействия для PaymentForm.xaml
    /// </summary>
    public partial class PaymentForm : Window
    {
        public PaymentForm()
        {
            InitializeComponent();
        }
        public PaymentForm(bool balanceShowed, string cardNum)
        {
            InitializeComponent();
            this.paymentMainFrame.Content = new paymentPage1(this, balanceShowed, cardNum);
        }
    }
}
