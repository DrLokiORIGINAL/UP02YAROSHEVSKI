using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UP02YAROSHEVSKI.ClassFolder
{
    class DGClass
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
            Initial Catalog=UP02YAROSHEVSKI;
            Integrated Security=True");
        SqlDataAdapter dataAdapter;
        DataGrid dataGrid;
        DataTable dataTable;

        //конструктор класс
        public DGClass(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
        }
        public void LoadDG(string sqlCommand)
        {
            try
            {
                sqlConnection.Open();
                //работа с БД на sql-команды и подключения
                dataAdapter = new SqlDataAdapter(sqlCommand, sqlConnection);
                //объявляется новая пустая виртуальная таблица
                dataTable = new DataTable();
                //заполняется виртуальная табблица на основе
                //sql-команды и подключения
                dataAdapter.Fill(dataTable);
                //из виртуальной таблицы данные загружаются
                //в DataGrid
                dataGrid.ItemsSource = dataTable.DefaultView;
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

        public string SelectId()
        {
            //объявляем пустой массива тип которого object
            object[] mass = null;
            //объявляем пустую переменную типа string,
            //которую будем возвращать в методе
            string id = "";
            try
            {
                //если DataGrid не пустой
                if (dataGrid != null)
                {
                    //Объявляем переменную типа DataRowView и
                    //записываем строку из dataGrid, который входит в DataView.
                    //DataView представляет связанное представление
                    //соответствующего DataTable
                    DataRowView dataRowView = dataGrid.SelectedItem
                    as DataRowView;
                    //если dataRowView не пустой
                    if (dataRowView != null)
                    {
                        //объявляем переменную типа DataRow, который
                        //необходим для извлечения и оценки данных
                        DataRow dataRow = (DataRow)dataRowView.Row;
                        //записываем в массив полученную строку
                        mass = dataRow.ItemArray;
                    }
                }
                //получаем из массива нулевую ячейку
                id = mass[0].ToString();
            }
            catch (Exception ex)
            {
                MessageBoxClass.ErrorMB(ex);
            }
            //возвращаем полученные данные при обращении к массиву
            return id;
        }
    }
}
