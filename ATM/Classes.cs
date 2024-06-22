using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1.X509;

namespace ATM
{
    public class PdfCreator
    {
        public static void CreatePdf(string content, string fileName)
        {
            // Создаем новый PDF-документ
            iTextSharp.text.Document document = new iTextSharp.text.Document();

            // Создаем новый PDF-писатель
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream($"D:\\!studying\\!C#\\ATM — копия\\ATM\\checks\\{fileName}.pdf", FileMode.Create));

            // Открываем документ для записи
            document.Open();

            // Создаем новый параграф, который мы будем добавлять в документ
            var baseFont = BaseFont.CreateFont(@"D:\!studying\!C#\ATM — копия\ATM\Fonts\arialmt.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new Font(baseFont, 12, Font.NORMAL);
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(content, font);

            // Добавляем параграф в документ
            document.Add(paragraph);

            // Закрываем документ
            document.Close();
        }
    }
    public class Helper
    {
        public static bool isStringNumeric(string input) { return new Regex(@"^[0-9]*\,?[0-9]{1,2}$").IsMatch(input); }
        public static bool checkByPattern(string input, string pattern) { return Regex.IsMatch(input, pattern); }
        public static string removeAllSPaces(string input) {
            StringBuilder str = new StringBuilder(input);
            for(int i=0; i<str.Length-1; i++)
            {
                if (Regex.IsMatch(str[i].ToString(), @"[. /\:,]")) str = str.Remove(i, 1);
            }
            return str.ToString();
        }
        public static string FormatString(string input)
        {
            if (input.Length % 4 != 0)
                throw new ArgumentException("Длина входной строки должна быть кратна 4.");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (i > 0 && i % 4 == 0)
                    sb.Append(' ');

                sb.Append(input[i]);
            }

            return sb.ToString();
        }
        public static string deformatString(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach(char item in input)
            {
                if(item!=' ')
                {
                    sb.Append(item);
                }
            }
            return sb.ToString();
        }
        public static string formatStringDate(string date)
        {
            if (date[2]=='/') return date;
            else return $"{date.Substring(0,2)}/{date.Substring(2,2)}";
        }
    }

    public class ClientHelper
    {
        private static string connectionString = "Data Source=DESKTOP-C0CATE;Initial Catalog=ATMDB;Integrated Security=True";


        public static void removeUser(string cardNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM cardsTable WHERE number = @cardNumber"; 

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cardNumber", cardNumber);
                    command.ExecuteNonQuery();
                }
            }
        }

        //обновить значение баланса карты
        public void UpdateBalance(string cardNumber, decimal newBalance)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE cardsTable SET balance = @newBalance WHERE number = @cardNumber";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@newBalance", newBalance);
                    command.Parameters.AddWithValue("@cardNumber", cardNumber);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Баланс успешно обновлен.");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось обновить баланс.");
                    }
                }
            }
        }
        //обновить значение срока действия карты
        public static void UpdateDate(string cardNumber, string date)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE cardsTable SET dateCard = @date WHERE number = @cardNumber";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    if (date[2]=='/') date = date.Remove(2,1);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@cardNumber", cardNumber);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Баланс успешно обновлен.");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось обновить баланс.");
                    }
                }
            }
        }
        //обновить значение код CVC карты
        public static void UpdateCVC(string cardNumber, string cvc)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE cardsTable SET cvc = @cvc WHERE number = @cardNumber";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@cvc", cvc);
                    command.Parameters.AddWithValue("@cardNumber", cardNumber);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Баланс успешно обновлен.");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось обновить баланс.");
                    }
                }
            }
        }
        //обновить значение пароля карты
        public static void UpdatePassword(string cardNumber, string pass)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE cardsTable SET passwordCard = @pass WHERE number = @cardNumber";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@pass", pass);
                    command.Parameters.AddWithValue("@cardNumber", cardNumber);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Баланс успешно обновлен.");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось обновить баланс.");
                    }
                }
            }
        }
        

        //обновить значение баланса для организации
        public bool UpdateBalanceORG(string cardNumber, decimal newBalance)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE organisationsTable SET balance = @newBalance WHERE organizationAdress = @adress";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@newBalance", newBalance);
                    command.Parameters.AddWithValue("@adress", cardNumber);
                    int rowsAffected = command.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
        }

        //получить значение баланса карты
        public static string getBalance(string cardNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Запрос для выборки данных по номеру карты
                string query = "SELECT * FROM cardsTable WHERE number = @cardNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@cardNumber", cardNumber);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string returned = reader["balance"].ToString().Remove(reader["balance"].ToString().Length - 3, 2);
                        return returned;
                    }
                    else
                    {
                        MessageBox.Show("не удалось прочитать баланс!");
                        return "";
                    }
                }
            }
        }//получить значение баланса карты
        public static string getPassword(string cardNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Запрос для выборки данных по номеру карты
                string query = "SELECT * FROM cardsTable WHERE number = @cardNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@cardNumber", cardNumber);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["passwordCard"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("не удалось прочитать баланс!");
                        return "";
                    }
                }
            }
        }
        //получить значение CVC
        public static string getCVC(string cardNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Запрос для выборки данных по номеру карты
                string query = "SELECT * FROM cardsTable WHERE number = @cardNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@cardNumber", cardNumber);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["cvc"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("не удалось прочитать CVC!");
                        return "";
                    }
                }
            }
        }//получить значение даты действия
        public static string getDate(string cardNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Запрос для выборки данных по номеру карты
                string query = "SELECT * FROM cardsTable WHERE number = @cardNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@cardNumber", cardNumber);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["dateCard"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("не удалось прочитать дату!");
                        return "";
                    }
                }
            }
        }

        //получить значение даты действия
        public static string getOwnerName(string cardNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Запрос для выборки данных по номеру карты
                string query = "SELECT * FROM cardsTable WHERE number = @cardNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@cardNumber", cardNumber);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["ownerName"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("не удалось прочитать дату!");
                        return "";
                    }
                }
            }
        }


        //получить значение баланса оганизации
        public static string getBalanceORG(string cardNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Запрос для выборки данных по номеру карты
                string query = "SELECT * FROM organisationsTable WHERE organizationAdress = @cardNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@cardNumber", cardNumber);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string returned = reader["balance"].ToString().Remove(reader["balance"].ToString().Length - 3, 2);
                        return returned;
                    }
                    else
                    {
                        MessageBox.Show("не удалось прочитать баланс!");
                        return "";
                    }
                }
            }
        }


        //проверка, существует ли данный номер карты
        public static bool IsCardNumberExists(string cardNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM cardsTable WHERE number = @cardNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@cardNumber", cardNumber);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        //проверка, существует ли данный адрес организации
        public static bool IsOrganisationAdressExists(string adress)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM organisationsTable WHERE organizationAdress = @adress";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@adress", adress); // Используйте "@adress", а не "@cardNumber"
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public static bool chackPassword(string pass, string cardNumber)
        {
            return ClientHelper.getPassword(cardNumber) == pass;
        }

        // РАБОТА С АДМИНИСТРАТОРАМИ
        public static bool adminsPassCheck(string login, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Запрос для выборки данных по номеру карты
                    string query = "SELECT COUNT(*) FROM Administrators WHERE Login = @login AND Password = @password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            return false;
        }

        public static AdminsForms.Admin getAdminByLogin(string login)
        {
            try
            {
                AdminsForms.Admin admin = new AdminsForms.Admin();
                string query = $"SELECT * FROM Administrators WHERE Login = @login";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                admin = new AdminsForms.Admin()
                                {
                                    ID = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Login = reader.GetString(3),
                                    Password = reader.GetString(4),
                                    Contact = reader.GetString(5)
                                };
                            }
                        }
                    }
                }
                return admin;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            return null;
        }

        public static ObservableCollection<AdminsForms.Admin> getAllAdmins()
        {
            try
            {
                string query = $"SELECT * FROM Administrators";
                ObservableCollection<AdminsForms.Admin> laws = new ObservableCollection<AdminsForms.Admin>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int facultyId = reader.GetInt32(0);
                                string facultyName = reader.GetString(1);
                                laws.Add(new AdminsForms.Admin()
                                {
                                    ID = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Login = reader.GetString(3),
                                    Password = reader.GetString(4),
                                    Contact = reader.GetString(5)
                                });
                            }
                        }
                    }
                }
                return laws;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            return null;
        }

        public static ObservableCollection<AdminsForms.CardTable> getAllClients()
        {
            try
            {
                string query = $"SELECT * FROM CardsTable";
                ObservableCollection<AdminsForms.CardTable> laws = new ObservableCollection<AdminsForms.CardTable>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int facultyId = reader.GetInt32(0);
                                string facultyName = reader.GetString(1);
                                laws.Add(new AdminsForms.CardTable()
                                {
                                    ID = reader.GetInt32(0),
                                    number = Helper.FormatString(reader.GetString(1)),
                                    dateCard = Helper.formatStringDate(reader.GetString(2)),
                                    cvc = reader.GetString(3),
                                    passwordCard = reader.GetString(4),
                                    ownerName = reader.GetString(5),
                                    balance = reader.GetDecimal(6)
                                });
                            }
                        }
                    }
                }
                return laws;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            return null;
        }
        public static ObservableCollection<AdminsForms.Organisation> getAllOrganisations()
        {
            try
            {
                string query = $"SELECT * FROM organisationsTable";
                ObservableCollection<AdminsForms.Organisation> laws = new ObservableCollection<AdminsForms.Organisation>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int facultyId = reader.GetInt32(0);
                                string facultyName = reader.GetString(1);
                                laws.Add(new AdminsForms.Organisation()
                                {
                                    ID = reader.GetInt32(0),
                                    organisationName = reader.GetString(1),
                                    organizationAdress = reader.GetString(2),
                                    balance = reader.GetDecimal(3)
                                });
                            }
                        }
                    }
                }
                return laws;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            return null;
        }

        public static void AddClient(string number, string dateCard, string cvc, string passwordCard, string ownerName, decimal balance)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string maxQuery = "Select MAX(id) from CardsTable";
                object result = 0;
                int id = 0;
                using (SqlCommand command = new SqlCommand(maxQuery, connection))
                {
                    result = command.ExecuteScalar(); 
                    if (result != DBNull.Value) id = Convert.ToInt32(result);
                    else Console.WriteLine("Таблица пуста или все id равны NULL");
                }
                MessageBox.Show(id.ToString());


                string sqlQuery = "INSERT INTO CardsTable (id, number, dateCard, cvc, passwordCard, ownerName, balance) VALUES (@id, @number, @dateCard, @cvc, @passwordCard, @ownerName, @balance)";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id+1);
                    command.Parameters.AddWithValue("@number", number);
                    command.Parameters.AddWithValue("@dateCard", dateCard);
                    command.Parameters.AddWithValue("@cvc", cvc);
                    command.Parameters.AddWithValue("@passwordCard", passwordCard);
                    command.Parameters.AddWithValue("@ownerName", ownerName);
                    command.Parameters.AddWithValue("@balance", balance);
                    //
                    command.ExecuteNonQuery();
                }
            }
        }









        public static void deleteClient(int ID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string sqlQuery1 = "DELETE FROM OrganisationToCard WHERE ID_card = @ID";
                using (SqlCommand command = new SqlCommand(sqlQuery1, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    command.ExecuteNonQuery();
                }

                string sqlQuery = "DELETE FROM CardsTable WHERE id = @ID";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void deleteOrganisation(int ID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string sqlQuery1 = "DELETE FROM OrganisationToCard WHERE ID_organisation = @ID";
                using (SqlCommand command = new SqlCommand(sqlQuery1, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    command.ExecuteNonQuery();
                }

                string sqlQuery = "DELETE FROM organisationsTable WHERE id = @ID";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void deleteAdministrators(int ID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "DELETE FROM Administrators WHERE ID = @ID";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddClient(string number, string dateCard, string cvc, string passwordCard, string owner, int balance)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "select MAX(id) from CardsTable";
                int id = 0;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    id = (int)command.ExecuteScalar();
                }


                string sqlQuery = "INSERT INTO CardsTable (id, number, dateCard, cvc, passwordCard, ownerName, balance) VALUES (@id, @number, @dateCard, @cvc, @passwordCard, @ownerName, @balance)";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id + 1);
                    command.Parameters.AddWithValue("@number", number);
                    command.Parameters.AddWithValue("@dateCard", dateCard);
                    command.Parameters.AddWithValue("@cvc", cvc);
                    command.Parameters.AddWithValue("@passwordCard", passwordCard);
                    command.Parameters.AddWithValue("@ownerName", owner);
                    command.Parameters.AddWithValue("@balance", balance);
                    command.ExecuteNonQuery();
                }
            }
        }


        public static void AddOrganisation(string name, string address, decimal balance)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "INSERT INTO organisationsTable (organisationName, organizationAdress, balance) VALUES (@organisationName, @organizationAdress, @balance)";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@organisationName", name);
                    command.Parameters.AddWithValue("@organizationAdress", address);
                    command.Parameters.AddWithValue("@balance", balance);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddAdmin(string name, string surname, string login, string password, string contacts)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "INSERT INTO Administrators (FirstName, LastName, Login, Password, Contact) VALUES (@FirstName, @LastName, @Login, @Password, @Contact)";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", name);
                    command.Parameters.AddWithValue("@LastName", surname);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Contact", contacts);
                    
                    command.ExecuteNonQuery();
                }
            }
        }



    }
}
