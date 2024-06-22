using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для infoPage.xaml
    /// </summary>
    public partial class infoPage : Page
    {
        private string type;
        public infoPage(string type)
        {
            InitializeComponent();
            this.type = type;

            switch (type)
            {
                case "Employees":
                    {
                        // Создаем столбцы таблицы в коде
                        DataGridTextColumn column1 = new DataGridTextColumn();
                        column1.Header = "Номер";
                        column1.Binding = new Binding("ID");
                        column1.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);

                        DataGridTextColumn column2 = new DataGridTextColumn();
                        column2.Header = "Имя";
                        column2.Binding = new Binding("FirstName");
                        column2.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                        DataGridTextColumn column5 = new DataGridTextColumn();
                        column5.Header = "Фамилия";
                        column5.Binding = new Binding("LastName");
                        column5.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                        DataGridTextColumn column3 = new DataGridTextColumn();
                        column3.Header = "Логин";
                        column3.Binding = new Binding("Login");
                        column3.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                        DataGridTextColumn column4 = new DataGridTextColumn();
                        column4.Header = "Контакты";
                        column4.Binding = new Binding("Contact");
                        column4.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);

                        // Добавляем созданные столбцы в таблицу
                        dataGrid.Columns.Add(column1);
                        dataGrid.Columns.Add(column2);
                        dataGrid.Columns.Add(column5);
                        dataGrid.Columns.Add(column3);
                        dataGrid.Columns.Add(column4);

                        dataGrid.ItemsSource = ClientHelper.getAllAdmins();
                        break;
                    }
                case "Clients":
                    {
                        // Создаем столбцы таблицы в коде
                        DataGridTextColumn column1 = new DataGridTextColumn();
                        column1.Header = "Номер";
                        column1.Binding = new Binding("ID");
                        column1.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);

                        DataGridTextColumn column2 = new DataGridTextColumn();
                        column2.Header = "Номер карты";
                        column2.Binding = new Binding("number");
                        column2.Width = new DataGridLength(1, DataGridLengthUnitType.Star);


                        DataGridTextColumn column3 = new DataGridTextColumn();
                        column3.Header = "Срок действия";
                        column3.Binding = new Binding("dateCard");
                        column3.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                        DataGridTextColumn column4 = new DataGridTextColumn();
                        column4.Header = "СVC";
                        column4.Binding = new Binding("cvc");
                        column4.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);

                        DataGridTextColumn column5 = new DataGridTextColumn();
                        column5.Header = "Имя держателя";
                        column5.Binding = new Binding("ownerName");
                        column5.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                        DataGridTextColumn column6 = new DataGridTextColumn();
                        column6.Header = "Баланс";
                        column6.Binding = new Binding("balance");
                        column6.Binding.StringFormat = "{0:0.00}";
                        column6.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                        DataGridTextColumn column7 = new DataGridTextColumn();
                        column7.Header = "Логин";
                        column7.Binding = new Binding("Login");
                        column7.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                        dataGrid.Columns.Add(column1);
                        dataGrid.Columns.Add(column2);
                        dataGrid.Columns.Add(column3);
                        dataGrid.Columns.Add(column4);
                        dataGrid.Columns.Add(column5);
                        dataGrid.Columns.Add(column6);

                        this.dataGrid.ItemsSource = ClientHelper.getAllClients();
                        break;
                    }
                case "Organisations":
                    {
                        // Создаем столбцы таблицы в коде
                        DataGridTextColumn column1 = new DataGridTextColumn();
                        column1.Header = "Номер";
                        column1.Binding = new Binding("ID");
                        column1.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);

                        DataGridTextColumn column2 = new DataGridTextColumn();
                        column2.Header = "Название";
                        column2.Binding = new Binding("organisationName");
                        column2.Width = new DataGridLength(1, DataGridLengthUnitType.Star);


                        DataGridTextColumn column3 = new DataGridTextColumn();
                        column3.Header = "Адрес";
                        column3.Binding = new Binding("organizationAdress");
                        column3.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                        DataGridTextColumn column6 = new DataGridTextColumn();
                        column6.Header = "Баланс";
                        column6.Binding = new Binding("balance");
                        column6.Binding.StringFormat = "{0:0.00}";
                        column6.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                        dataGrid.Columns.Add(column1);
                        dataGrid.Columns.Add(column2);
                        dataGrid.Columns.Add(column3);
                        dataGrid.Columns.Add(column6);

                        dataGrid.ItemsSource = ClientHelper.getAllOrganisations();
                        break;
                    }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//ДОБАВИТЬ
        {
            try
            {
                switch (type)
                {
                    case "Employees":
                        {
                            addAdminForm form = new addAdminForm();
                            form.ShowDialog();
                            break;
                        }
                    case "Clients":
                        {
                            addClientForm form = new addClientForm();
                            form.ShowDialog();
                            break;
                        }
                    case "Organisations":
                        {
                            addOrganisationForm form = new addOrganisationForm();
                            form.ShowDialog();
                            break;
                        }

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // удаление
        {
            try
            {
                var selected = (dynamic)dataGrid.SelectedItem; 
                switch (type)
                {
                    case "Employees":
                        {
                            ClientHelper.deleteAdministrators(selected.ID);
                            break;
                        }
                    case "Clients":
                        {
                            ClientHelper.deleteClient(selected.ID);
                            break;
                        }
                    case "Organisations":
                        {
                            ClientHelper.deleteOrganisation(selected.ID);
                            break;
                        }
                }
                MessageBox.Show("Объект удален!");
                NavigationService.Navigate(new infoPage(type));
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new infoPage(type));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            searchForm form = new searchForm(type);
            form.ShowDialog();
        }
    }
}
