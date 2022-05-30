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

namespace UP02YAROSHEVSKI.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
            Initial Catalog=UP02YAROSHEVSKI;
            Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxClass.ExitMB();
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MessageBoxClass.ErrorMB("Вы не ввели логин");
                LoginTb.Focus();
            }    
            else if(string.IsNullOrWhiteSpace(PasswordPsb.Password))
            {
                MessageBoxClass.ErrorMB("Вы не ввели пароль");
                    PasswordPsb.Focus();
            }
            else
            {
                try
                {
                  sqlConnection.Open();
                  sqlCommand=new SqlCommand("Select Email" + 
                      "Password,RoleID " +
                        "From dbo.[User] " +
                        $"Where Email='{LoginTb.Text}'",
                        sqlConnection);
                    dataReader=sqlCommand.ExecuteReader();
                    dataReader.Read();
                    if(dataReader[1].ToString()!=
                        PasswordPsb.Password)
                    {
                       MessageBoxClass.ErrorMB("Вы ввели не верный пароль");
                        PasswordPsb.Focus();
                    }
                    else
                    {
                        switch(dataReader[2].ToString())
                        {
                            case "1":
                                new AdminFolder.
                                    MenuAdminWindow().ShowDialog();
                                break;
                                case "2":
                                MessageBoxClass.
                                    InfoMB("Пользователь");
                                break;
                                case "3":
                                MessageBoxClass.InfoMB("Менеджер");
                                break;
                        }
                    }
                }
                catch(Exception ex)
                {

                    MessageBoxClass.ErrorMB(ex);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        private void RegistrationBtn_Click(object sender,
            RoutedEventArgs e)
        {
            new RegistrationWindow().ShowDialog();
        }
    }
}
