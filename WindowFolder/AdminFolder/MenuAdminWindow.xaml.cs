using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MenuAdminWindow.xaml
    /// </summary>
    public partial class MenuAdminWindow : Window
    {
        DGClass dGClass;
        public MenuAdminWindow()
        {
            InitializeComponent();
            dGClass=new DGClass(ListUserDG);
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[User]" +
                $"Where FirstName LIKE '%{SearchTb.Text}'" +
                $"Where LastName LIKE '%{SearchTb.Text}'");
        }

        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(ListUserDG.SelectedItem==null)
            {
                MessageBoxClass.ErrorMB("Вы не выбрали строку");
            }
            else
            {
                VariableClass.UserId=dGClass.SelectId();
                try
                {
                    new EditUserWindow().ShowDialog();
                    dGClass.LoadDG("Selected * From dbo.[User]");
                }
                catch (Exception ex)
                {
                    MessageBoxClass.ErrorMB(ex);
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[User]");
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().ShowDialog();
            dGClass.LoadDG("Select * From dbo.[User]");
        }
    }
}
