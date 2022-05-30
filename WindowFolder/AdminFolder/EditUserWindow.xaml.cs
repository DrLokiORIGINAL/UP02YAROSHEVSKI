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
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
            Initial Catalog=UP02YAROSHEVSKI;
            Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        CBClass cBClass;
        public EditUserWindow()
        {
            InitializeComponent();
            cBClass=new CBClass();
        }

        private void EditUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand =
                new SqlCommand("Update " +
                "dbo.[User] " +
                $"Set [Email] ='{EmailTb.Text}'," +
                $"[Password]='{PasswordPsb.Password}'," +
                $"FirstName='{FirstNameTb.Text}'," +
                $"LastName='{LastNameTb.Text}'," +
                $"RoleId='{RoleCb.SelectedValue.ToString()}' " +
                $"Where UserId='{VariableClass.UserId}'",
                sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MessageBoxClass.InfoMB($"Данные пользователя" +
                $" {LastNameTb.Text} {FirstNameTb.Text} " +
                $"успешно отредактированы");
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.RoleCBLoad(RoleCb);
            try
            {
                sqlConnection.Open();
                sqlCommand =
                new SqlCommand("Select * from dbo.[User] " +
                $"Where UserId='{VariableClass.UserId}'",
                sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                EmailTb.Text = dataReader[1].ToString();
                PasswordPsb.Password = dataReader[2].ToString();
                FirstNameTb.Text = dataReader[3].ToString();
                LastNameTb.Text = dataReader[4].ToString();
                RoleCb.SelectedValue = dataReader[5].ToString();
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
    }
}
