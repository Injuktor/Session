using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task
{
    public partial class DriversForm : Form
    {
        public DriversForm()
        {
            InitializeComponent();

            UpdateDataGrid();

            //Слушатель для бездействия
            this.MouseMove += AuthForm.DriverAddForm_MouseMove;
        }

        //Обработка нажатия на кнопку "Добавить водителя"
        private void addDriverButton_Click(object sender, EventArgs e)
        {
            if(AuthForm.DriverAdd == null || AuthForm.DriverAdd.IsDisposed)
                AuthForm.DriverAdd = new DriverAddForm();

            AuthForm.DriverAdd.Show();
        }

        //Обновления списка водителей
        public void UpdateDataGrid()
        {
            //Заполнение DataGrid
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM drivers", Database.Connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            driversGrid.DataSource = dataSet.Tables[0];
        }

        private void driversGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (AuthForm.DriverCard == null || AuthForm.DriverCard.IsDisposed)
                    AuthForm.DriverCard = new DriverCardForm();

                AuthForm.DriverCard.Id = Convert.ToInt32(driversGrid.Rows[e.RowIndex].Cells["id"].Value);

                AuthForm.DriverCard.Show();
            }
            catch { return; }
        }

        private void licencesButton_Click(object sender, EventArgs e)
        {
            if (AuthForm.Licences == null || AuthForm.Licences.IsDisposed)
                AuthForm.Licences = new LicencesForm();

            AuthForm.Licences.Show();
        }
    }
}
