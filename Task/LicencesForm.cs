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
    public partial class LicencesForm : Form
    {
        public LicencesForm()
        {
            InitializeComponent();

            UpdateDataGrid();

            //Слушатель для бездействия
            this.MouseMove += AuthForm.DriverAddForm_MouseMove;
        }

        //Обновления списка ВУ
        public void UpdateDataGrid()
        {
            //Заполнение DataGrid
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM licences", Database.Connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            licencesGrid.DataSource = dataSet.Tables[0];

            //Перебор всех строк для индикатора
            foreach (DataGridViewRow row in licencesGrid.Rows)
            {
                //Проверка клетки со статусом
                if(row.Cells["status"].Value != null && Convert.ToString(row.Cells["status"].Value).Length > 0)
                {
                    string status = Convert.ToString(row.Cells["status"].Value);

                    DataGridViewImageCell dataGridViewImageCell = (DataGridViewImageCell) row.Cells["Indicator"];

                    /*
                     * Тоже игнорирует любмые изменения
                     */
                    {
                        /*
                        Bitmap bitmap = new Bitmap(25, 25);

                        for (int x = 0; x < bitmap.Width; ++x)
                        {
                            for (int y = 0; y < bitmap.Height; ++y)
                            {
                                bitmap.SetPixel(x, y, Color.Green);
                            }
                        }

                        dataGridViewImageCell.ValueIsIcon = true;
                        dataGridViewImageCell.Value = Icon.FromHandle(bitmap.GetHbitmap());
                        */
                    }

                    /*
                     * Не работают стили (Все значения, которые ниже пытался установить - просто игнорируются
                     */
                    {
                       /* System.Windows.Forms.DataGridViewCellStyle cellImageStyle = new System.Windows.Forms.DataGridViewCellStyle();

                        cellImageStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                        cellImageStyle.BackColor = System.Drawing.Color.Green;
                        cellImageStyle.ForeColor = System.Drawing.Color.Green;
                        cellImageStyle.SelectionBackColor = System.Drawing.Color.Green;
                        cellImageStyle.SelectionForeColor = System.Drawing.Color.Green;

                        row.Cells["status"].Style = cellImageStyle;

                        if (status.Equals("активен"))
                        {
                            row.Cells["Indicator"].Style.BackColor = Color.Green;
                            row.Cells["Indicator"].Style.ForeColor = Color.Green;
                            row.Cells["Indicator"].Style.SelectionForeColor = Color.Green;
                            row.Cells["Indicator"].Style.SelectionBackColor = Color.Green;

                            row.Cells["Indicator"].Style = cellImageStyle;
                        }*/
                    }
                }
            }
        }

        //Кнопка добавления водителя, для перевода на модальное окно
        private void addDriverLicenceButton_Click(object sender, EventArgs e)
        {
            if (AuthForm.DriverLicenceAdd == null || AuthForm.DriverLicenceAdd.IsDisposed)
                AuthForm.DriverLicenceAdd = new DriverLicenceAddForm();

            AuthForm.DriverLicenceAdd.Show();
        }

        //Отображение конкретного ВУ
        private void licencesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (AuthForm.LicenceCard == null || AuthForm.LicenceCard.IsDisposed)
                    AuthForm.LicenceCard = new LicenceCardForm();

                AuthForm.LicenceCard.Id = Convert.ToInt32(licencesGrid.Rows[e.RowIndex].Cells["id"].Value);
                AuthForm.LicenceCard.Show();
            } catch
            {
                return;
            }
            
        }
    }
}
