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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для AWindow.xaml
    /// </summary>
    public partial class AWindow : Window
    {
        public AWindow()
        {
            InitializeComponent();
            ConfigHelper.Instance.SetLang("en");
        }

        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == "+")
    && (!Phone.Text.Contains("+")
    &&  Phone.Text.Length == 0)))
            {
                e.Handled = true;
            }
        }

        

        private void Reg_Click(object sender, RoutedEventArgs e)
        {

            {
                using (var con = new SqlConnection("Data Source=PLABSQLw19s1,49172;Initial Catalog=PerekupKV;Integrated Security=True"))
                {
                    con.Open();
                    var cmd = new SqlCommand($"INSERT INTO [Userss] ([Roles], [Login], [Password], [Name], [Surname], [Phone_Number],[ApartamentsID]) VALUES ('User', '{Log.Text}', '{Pass.Text}', '{Name.Text}', '{SurName.Text}', '{Phone.Text}', '1')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Пользователь успешно создан.Пройдите авторизацию.");
                    this.Close();
                    
                }
            }

           
        }

    }
 }






/*
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

    */
