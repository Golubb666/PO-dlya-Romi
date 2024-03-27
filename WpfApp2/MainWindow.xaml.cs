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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Log_Click_1(object sender, RoutedEventArgs e)
        {
            string selectedRole = "";

            using (var con = new SqlConnection("Data Source=PLABSQLw19s1,49172;Initial Catalog=KandK;Integrated Security=True"))
            {
                con.Open();
                var cmd = new SqlCommand($"SELECT [Role] FROM [vvod] WHERE [Login]='{Login.Text}' AND [Pass]='{pass.Password}'", con);
                var userRole = cmd.ExecuteScalar() as string;

                if (string.IsNullOrEmpty(userRole))
                {
                    MessageBox.Show("Неверные данные");
                    return;
                }

                if (adminRadioButton.IsChecked == true && userRole == "Admin")
                {
                    AWindow adminWindow = new AWindow();
                    adminWindow.Show();
                }
                else if (managerRadioButton.IsChecked == true && userRole == "Manager")
                {
                    MWindow managerWindow = new MWindow();
                    managerWindow.Show();
                }
                else if (userRadioButton.IsChecked == true && userRole == "User")
                {
                    UWindow userWindow = new UWindow();
                    userWindow.Show();
                }
                else
                {
                    MessageBox.Show("Неверные данные");
                }
            }
        }

        private void Reg_Click_1(object sender, RoutedEventArgs e)
        {
            string selectedRole = "";

            if (adminRadioButton.IsChecked == true)
            {
                selectedRole = "Admin";
            }
            else if (userRadioButton.IsChecked == true)
            {
                selectedRole = "User";
            }
            else if (managerRadioButton.IsChecked == true)
            {
                selectedRole = "Manager";
            }

            if (string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("Пожалуйста, выберите категорию регистрации!");
                return;
            }

            try
            {
                using (var con = new SqlConnection("Data Source=PLABSQLw19s1,49172;Initial Catalog=KandK;Integrated Security=True"))
                {
                    con.Open();

                    var cmd = new SqlCommand($"INSERT INTO [vvod] ([Role],[login],[Pass]) VALUES (@Role, @Login, @Password)", con);
                    cmd.Parameters.AddWithValue("@Role", selectedRole);
                    cmd.Parameters.AddWithValue("@Login", Login.Text);
                    cmd.Parameters.AddWithValue("@Password", pass.Password);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно добавлены!");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении данных!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных в базу данных: " + ex.Message);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из приложения?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }



    }
}
