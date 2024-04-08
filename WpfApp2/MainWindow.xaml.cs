using HandyControl.Tools;
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
            ConfigHelper.Instance.SetLang("en");
        }

        private void Log_Click_1(object sender, RoutedEventArgs e)
        {
            string selectedRole = "";

            using (var con = new SqlConnection("Data Source=PLABSQLw19s1,49172;Initial Catalog=PerekupKV;Integrated Security=True"))
            {
                con.Open();
                var cmd = new SqlCommand($"SELECT [Roles] FROM [Userss] WHERE [Login]='{Login.Text}' AND [Password]='{pass.Password}'", con);
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
            AWindow aWindow = new AWindow();
            aWindow.Show();

            
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
