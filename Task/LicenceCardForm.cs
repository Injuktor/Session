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
    public partial class LicenceCardForm : Form
    {
        private int _id;

        public LicenceCardForm()
        {
            InitializeComponent();

            //Слушатель для бездействия
            this.MouseMove += AuthForm.DriverAddForm_MouseMove;
        }

        //Удаление водительского удостоверения
        private void saveDriverLicenceButton_Click(object sender, EventArgs e)
        {
            //Диалог подтверждения удаления
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить водителя?", "Подтверждение", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteCommand = $"DELETE FROM licences WHERE id={_id}";
                MySqlCommand command = Database.Connection.CreateCommand();
                command.CommandText = deleteCommand;
                command.ExecuteNonQuery();

                //Обновление лицензий
                if (AuthForm.Licences != null && !AuthForm.Licences.IsDisposed)
                    AuthForm.Licences.UpdateDataGrid();

                //Закрытие формы
                if (AuthForm.LicenceCard != null && !AuthForm.LicenceCard.IsDisposed)
                    AuthForm.LicenceCard.Close();
            }
        }

        private void LicenceCardForm_Load(object sender, EventArgs e)
        {

        }

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;

                string selectCommand = $"SELECT * FROM licences WHERE id={_id}";
                MySqlCommand command = Database.Connection.CreateCommand();
                command.CommandText = selectCommand;
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    surnameBox.Text = reader.GetString("surname");
                    nameBox.Text = reader.GetString("surname");
                    middlenameBox.Text = reader.GetString("surname");
                    dateAndPlaceBirthBox.Text = reader.GetString("date_and_place_birth");
                    dateGetLicencePicker.Value = reader.GetMySqlDateTime("licence_date").Value;
                    dateExpireLicencePicker.Value = reader.GetMySqlDateTime("expire_date").Value;
                    nameCompanyBox.Text = reader.GetString("name_giver_licence");
                    idenNumberBox.Text = reader.GetString("identificator_number");
                    licenceNumberBox.Text = reader.GetString("licence_number");
                    pathImageOwnerLabel.Text = reader.GetString("photo");
                    pathSignatureOwnerLabel.Text = reader.GetString("signature");
                    defaultPlaceBox.Text = reader.GetString("default_address");
                    descriptionRegChangeCountryBox.Text = reader.GetString("information_reg_change_country");
                    descriptionRegBox.Text = reader.GetString("information_reg");
                    statusLabel.Text = "Статус: " + reader.GetString("status");

                    Bitmap bitmap = new Bitmap(25, 25);

                    switch (reader.GetString("status").ToLower())
                    {
                        case "активен":
                            for (int x = 0; x < bitmap.Width; ++x)
                            {
                                for (int y = 0; y < bitmap.Height; ++y)
                                {
                                    bitmap.SetPixel(x, y, Color.Green);
                                }
                            }
                            statusPB.Image = bitmap;
                            break;
                        case "утратил силу":
                            for (int x = 0; x < bitmap.Width; ++x)
                            {
                                for (int y = 0; y < bitmap.Height; ++y)
                                {
                                    bitmap.SetPixel(x, y, Color.Gray);
                                }
                            }
                            statusPB.Image = bitmap;
                            break;
                        case "приостановлен":
                        case "изъят":
                            for (int x = 0; x < bitmap.Width; ++x)
                            {
                                for (int y = 0; y < bitmap.Height; ++y)
                                {
                                    bitmap.SetPixel(x, y, Color.Red);
                                }
                            }
                            statusPB.Image = bitmap;
                            break;
                        default:
                            break;
                    }
                }

                reader.Close();
            }
        }

        //Изменение статуса
        private void changeStatusButton_Click(object sender, EventArgs e)
        {
            if (AuthForm.ChangeStatus == null || AuthForm.ChangeStatus.IsDisposed)
                AuthForm.ChangeStatus = new ChangeStatusForm();

            AuthForm.ChangeStatus.Id = _id;
            AuthForm.ChangeStatus.Show();
        }
    }
}
