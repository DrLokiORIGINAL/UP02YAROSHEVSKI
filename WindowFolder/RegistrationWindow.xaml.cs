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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        SqlConnection SqlConnection =
            new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
            Initial Catalog=UP02YAROSHEVSKI;
            Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass=PasswordPsb.Password;
            string zagl="QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal="qwertyuiopasdfghjklzxcvbnm";
            string cif="1234567890";
            string znak="!@#$%^&*()";
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MessageBoxClass.ErrorMB("Вы не ввели логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPsb.Password))
            {
                MessageBoxClass.ErrorMB("Вы не ввели пароль");
                PasswordPsb.Focus();
            }
            else if(zagl.IndexOfAny(pass.ToCharArray())==-1)
            {
                MessageBoxClass.ErrorMB
                    ("пароль должен содержать прописную букву");
                PasswordPsb.Focus();
            }
            else if(mal.IndexOfAny(pass.ToCharArray())==-1)
            {
                MessageBoxClass.ErrorMB
                    ("пароль должен содержать заглавную букву");
                PasswordPsb.Focus();
            }    
            else if(cif.IndexOfAny(pass.ToCharArray())==-1)
            {
                MessageBoxClass.ErrorMB
                    ("пароль должен содержать цифры");
                PasswordPsb.Focus();
            }
            else if(znak.IndexOfAny(pass.ToCharArray())==-1)
            {
                MessageBoxClass.ErrorMB
                    ("Пароль должен содержать" +
                    "Один из следующиъ знаков" +znak);
                PasswordPsb.Focus();
            }

            else if(string.IsNullOrWhiteSpace
                (RepeatPasswordPsb.Password))
            {
                MessageBoxClass.ErrorMB
                    ("Вы не ввели повторно пароль");
                RepeatPasswordPsb.Focus();
            }
            else if(PasswordPsb.Password!=RepeatPasswordPsb.Password)
            {
                MessageBoxClass.ErrorMB
                    ("Пароли не совпадают");
                RepeatPasswordPsb.Focus();
            }
            else
            {
                try
                {
                    SqlConnection.Open();
                    sqlCommand=new SqlCommand("Insert Into dbo.[User]" +
                        "(Email, Password, RoleID) Values" +
                        $"('{LoginTb.Text}' + " +
                        $"'{PasswordPsb.Password}', 2)",
                        SqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MessageBoxClass.InfoMB
                        ("Вы успешно зарегестрировались");
                }
                catch (Exception ex)
                {
                    MessageBoxClass.ErrorMB(ex);
                }
                finally
                {
                    SqlConnection.Close();
                }
            }
                
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxClass.ExitMB();
        }
    }
}
