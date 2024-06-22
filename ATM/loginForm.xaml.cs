using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для loginForm.xaml
    /// </summary>
    public partial class loginForm : Window
    {
        private string connectionString = "Data Source=DESKTOP-C0CATE;Initial Catalog=ATMDB;Integrated Security=True";
        private string balance, cardNumber;
        public loginForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cardNumber = this.cardNumberTB.Text;
            //string storedPass = this.passwordTB.Text;
            //if (cardNumber == string.Empty || storedPass == string.Empty)
            //{
            //    MessageBox.Show("данные введены некорректно!");
            //    return;
            //}
            //else if(!ClientHelper.adminsPassCheck(cardNumber, storedPass))
            //{
            //    MessageBox.Show("данный администратор не найден!");
            //    return;
            //}
            //else
            //{
            //    AdminsForms.MainWindow2 main = new AdminsForms.MainWindow2(cardNumber);
            //    main.Show();
            //    this.Close();
            //}
            AdminsForms.MainWindow2 main = new AdminsForms.MainWindow2(cardNumber);
            main.Show();
            this.Close();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            cardNumber = this.cardNumberTB.Text;
            string storedPass = this.passwordTB.Text, date, cvc, ownerName;



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Запрос для выборки данных по номеру карты
                string query = "SELECT * FROM cardsTable WHERE number = @cardNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@cardNumber", cardNumber);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read() && storedPass == reader["passwordCard"].ToString())
                {
                    if (decimal.Parse(reader["balance"].ToString()) >= 0)
                    {
                        MainWindow expo = new MainWindow(reader["number"].ToString(), reader["balance"].ToString(), reader["dateCard"].ToString(), reader["cvc"].ToString(), reader["passwordCard"].ToString(), reader["ownerName"].ToString());
                        expo.Show();
                        this.Close();
                    }
                    else MessageBox.Show("ваш счет закрыт! обратитесь в отеделение чтобы открыть его обратно!");
                }
                else { MessageBox.Show("номер карты или пароль не верен!"); }
            }


            
        }
    }
}
