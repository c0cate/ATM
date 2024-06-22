using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATM.AdminsForms
{
	/// <summary>
	/// Логика взаимодействия для applicationPage.xaml
	/// </summary>
	/// 
	public class Application
	{
		public string fio { get; set; }
		public string bio { get; set; }
		public string date { get; set; }
		public string exp { get; set; }
		public string phone { get; set; }

		public override string ToString()
		{
			string[] ss = fio.Split(' ');
			return $"{ss[0]} {ss[1]} {ss[2]} - {exp}";
		}
	}
	public partial class applicationPage : Page
	{
		private ObservableCollection<Application> applicationList;
		public applicationPage()
		{
			InitializeComponent();
			string dirName = "D:\\!studying\\!C#\\ATM — копия\\ATM\\offers";
			// если папка существует
			if (Directory.Exists(dirName))
			{
				applicationList = new ObservableCollection<Application>();
				string[] files = Directory.GetFiles(dirName);
				foreach(string file in files)
				{
					using (FileStream fs = new FileStream(file, FileMode.Open))
					{
						using (StreamReader sr = new StreamReader(fs))
						{
							applicationList.Add(new Application()
							{
								fio = sr.ReadLine(),
								bio = sr.ReadLine(),
								date = sr.ReadLine(),
								exp = sr.ReadLine(),
								phone = sr.ReadLine()
							});
						}
					}
				}
                appsListBox.ItemsSource = applicationList;
			}
		}

        private void appsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			string[] names = appsListBox.SelectedItem.ToString().Split("-");
			string name = names[0].TrimEnd(), path = $"D:\\!studying\\!C#\\ATM — копия\\ATM\\offers\\{name}.txt";
			using (FileStream fs = new FileStream(path, FileMode.Open))
			{
				using (StreamReader sr = new StreamReader(fs))
				{
					string[] strs = sr.ReadToEnd().Split("\n");
					fioTB.Text = strs[0];
                    informationTB.Text = strs[1];
					brthTB.Text = strs[2];
					expTB.Text = strs[3];
					phoneTB.Text = strs[4];
				}
			}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			//MessageBox.Show(fioTB.Text.Substring(0, fioTB.Text.Length - 1));
			try {
				
                Application current = applicationList.Where(e => e.fio == fioTB.Text.Substring(0, fioTB.Text.Length - 1)).First();

                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-C0CATE;Initial Catalog=ATMDB;Integrated Security=True"))
                {
                    connection.Open();
                    string sqlQuery = "INSERT INTO ConfirmedApplications (Name, Information, BirthDay, Experience, PhoneNumber) VALUES (@Name, @Information, @BirthDay, @Experience, @PhoneNumber)";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Name", current.fio);
                        command.Parameters.AddWithValue("@Information", current.bio);
                        command.Parameters.AddWithValue("@BirthDay", current.date);
                        command.Parameters.AddWithValue("@Experience", current.exp);
                        command.Parameters.AddWithValue("@PhoneNumber", current.phone);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Предложение принято!");
                string filePath = $"D:\\!studying\\!C#\\ATM — копия\\ATM\\offers\\{fioTB.Text.Substring(0, fioTB.Text.Length - 1)}.txt";
                if (File.Exists(filePath)) File.Delete(filePath);
                else throw new Exception("Файл не существует.");
				NavigationService.Navigate(new applicationPage());
            }
			catch(Exception ex) { MessageBox.Show(ex.Message); }
		}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
			try
			{
                string filePath = $"D:\\!studying\\!C#\\ATM — копия\\ATM\\offers\\{fioTB.Text.Substring(0, fioTB.Text.Length - 1)}.txt";

                if (File.Exists(filePath)) File.Delete(filePath);
                else throw new Exception("Файл не существует.");

                MessageBox.Show("Предложение отклонено!");
                NavigationService.Navigate(new applicationPage());

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
