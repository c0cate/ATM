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

namespace ATM.AdminsForms
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage( string login)
        {
            InitializeComponent();
            Admin admin = ATM.ClientHelper.getAdminByLogin(login);
            TB1.Text = $"Добро пожаловать, {admin.FirstName} {admin.LastName}!"; 
            TB2.Text = $"С вами можно связаться через {admin.Contact}."; 
            TB3.Text = $"Банкоматы (автоматические кассовые аппараты) стали неотъемлемой частью современной банковской системы. Они предоставляют клиентам удобный доступ к банковским услугам, таким как снятие наличных, проверка баланса счета, перевод средств и многие другие."; 
        }
    }
}
