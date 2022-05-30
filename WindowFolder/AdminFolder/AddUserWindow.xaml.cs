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
using UP02YAROSHEVSKI.ClassFolder;

namespace UP02YAROSHEVSKI.WindowFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
            Initial Catalog=UP02YAROSHEVSKI;
            Integrated Security=True");
        SqlCommand sqlCommand;
        CBClass cBClass;
        public AddUserWindow()
        {
            InitializeComponent();
            cBClass=new CBClass();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.RoleCBLoad(RoleCb);
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = PasswordPsb.Password;
            string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string cif = "123457890";
            string znak = "!@#$%^&*()_+";

            if (string.IsNullOrWhiteSpace(EmailTb.Text))
            {
                MessageBoxClass.ErrorMB("Некоректный логин");
                EmailTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPsb.Password))
            {
                MessageBoxClass.ErrorMB("Некоректный пароль");
                PasswordPsb.Focus();
            }
            else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MessageBoxClass.ErrorMB("Пароль должен содержать заглавную букву");
                PasswordPsb.Focus();
            }
            else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MessageBoxClass.ErrorMB("Пароль должен содержать маленькую букву");
                PasswordPsb.Focus();
            }
            else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MessageBoxClass.ErrorMB("Пароль должен содержать цифру");
                PasswordPsb.Focus();
            }
            else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MessageBoxClass.ErrorMB("Пароль должен содержать " +
                    "один из этих знаков " + znak);
                PasswordPsb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(FirstNameTb.Text))
            {
                MessageBoxClass.ErrorMB("Имя не введено");
                FirstNameTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(FirstNameTb.Text))
            {
                MessageBoxClass.ErrorMB("Фамилия не введена");
                LastNameTb.Focus();
            }
            else if (RoleCb.SelectedIndex == -1)
            {
                MessageBoxClass.ErrorMB("Не выбрана роль");
                RoleCb.Focus();
            }
            try
            {
                sqlConnection.Open();
                sqlCommand=new SqlCommand("Insert Into dbo.[User]" +
                    "(Email, Password, FirstName, LastName, RoleID)" +
                    "Values" +
                    $"('{EmailTb.Text}', '{PasswordPsb.Password}'," +
                    $"'{FirstNameTb.Text}', '{LastNameTb.Text}'," +
                    $"'{RoleCb.SelectedValue.ToString()}')",
                    sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MessageBoxClass.InfoMB($"Пользователь {LastNameTb.Text}" +
                    $"{FirstNameTb.Text} Успешно добавлен");
            }
            catch (Exception ex)
            {
                MessageBoxClass.ErrorMB(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxClass.ExitMB();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MenuAdminWindow().ShowDialog();
        }
    }
}
