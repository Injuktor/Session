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
    public partial class DriverLicenceAddForm : Form
    {
        public DriverLicenceAddForm()
        {
            InitializeComponent();

            //Слушатель для бездействия
            this.MouseMove += AuthForm.DriverAddForm_MouseMove;
        }

        //Добавление ВУ
        private void addDriverLicenceButton_Click(object sender, EventArgs e)
        {
            int idDriver = 0;
            string surname = surnameBox.Text;
            string name = nameBox.Text;
            string middlename = middlenameBox.Text;
            string dateAndPlaceBirth = dateAndPlaceBirthBox.Text;
            DateTime dateGetLicence = dateGetLicencePicker.Value;
            DateTime dateExpireLicence = dateExpireLicencePicker.Value;
            string nameCompany = nameCompanyBox.Text;
            string idenNumber = idenNumberBox.Text;
            string licenceNumber = licenceNumberBox.Text;
            string photoOwner = pathImageOwnerLabel.Text.Replace('\\', '/');
            string signatureOwner = pathSignatureOwnerLabel.Text.Replace('\\', '/');
            string defaultPlace = defaultPlaceBox.Text;
            string categoryTS = categoryTSBox.Text;
            DateTime dateGetCategoryTS = dateGetTSPicker.Value;
            DateTime dateExpireCategoryTS = dateExpireTSPicker.Value;
            string descriptionTS = descriptionBox.Text;
            string descriptionChangeCountry = descriptionRegChangeCountryBox.Text;
            string descriptionReg = descriptionRegBox.Text;

            if (idDriverBox.Text.Length > 0)
            {
                try { idDriver = Convert.ToInt32(idDriverBox.Text); } catch { MessageBox.Show("Некорректный ID водителя"); return; }
            }
            if(surname.Length == 0)
            {
                MessageBox.Show("Введите фамилию владельца ВУ");
                return;
            }
            if (name.Length == 0)
            {
                MessageBox.Show("Введите имя владельца ВУ");
                return;
            }
            if (middlename.Length == 0)
            {
                MessageBox.Show("Введите отчество владельца ВУ");
                return;
            }
            if (dateAndPlaceBirth.Length == 0)
            {
                MessageBox.Show("Введите дату и место рождения владельца ВУ");
                return;
            }
            if (nameCompany.Length == 0)
            {
                MessageBox.Show("Введите название организации выдавшего удостоверение");
                return;
            }
            if (idenNumber.Length == 0)
            {
                MessageBox.Show("Введите идентификационный номер владельца ВУ");
                return;
            }
            if (licenceNumber.Length == 0)
            {
                MessageBox.Show("Введите номер удостоверения владельца ВУ");
                return;
            }
            if (photoOwner.Length == 0)
            {
                MessageBox.Show("Выберите фотографию владельца ВУ");
                return;
            }
            if (signatureOwner.Length == 0)
            {
                MessageBox.Show("Выберите фотографию подписи владельца ВУ");
                return;
            }
            if (defaultPlace.Length == 0)
            {
                MessageBox.Show("Введите обычное местожительство владельца ВУ");
                return;
            }
            if (categoryTS.Length == 0)
            {
                MessageBox.Show("Введите категорию ТС");
                return;
            }
            
            try
            {
                string insertCommandLicence = "INSERT INTO `user5_db`.`licences` (`driver_id`,`surname`,`name`,`middlename`,`date_and_place_birth`,`licence_date`,`expire_date`,`name_giver_licence`,`identificator_number`,`licence_number`,`photo`,`signature`,`default_address`,`information_reg_change_country`,`information_reg`,`status`)VALUES" +
                $"({idDriver},'{surname}','{name}','{middlename}','{dateAndPlaceBirth}','{dateGetLicence.ToString("yyyy-MM-dd hh:mm:ss")}','{dateExpireLicence.ToString("yyyy-MM-dd hh:mm:ss")}','{nameCompany}', '{idenNumber}','{licenceNumber}','{photoOwner}','{signatureOwner}','{defaultPlace}','{descriptionChangeCountry}','{descriptionReg}','активен');";
                MySqlCommand command = Database.Connection.CreateCommand();
                command.CommandText = insertCommandLicence;
                command.ExecuteNonQuery();

                int lastId = 0;

                MySqlCommand commandReader = Database.Connection.CreateCommand();
                commandReader.CommandText = "SELECT LAST_INSERT_ID();";
                MySqlDataReader reader = commandReader.ExecuteReader();

                if(reader.Read())
                    lastId = reader.GetInt32(0);

                reader.Close();

                string insertCommandCategory = "INSERT INTO `user5_db`.`categories`(`licence_id`,`category`,`category_date`,`expire_date`,`description`)VALUES" +
                    $"({lastId},'{categoryTS}','{dateGetCategoryTS.ToString("yyyy-MM-dd hh:mm:ss")}','{dateExpireCategoryTS.ToString("yyyy-MM-dd hh:mm:ss")}','{descriptionTS}');";

                MySqlCommand commandInsertCategory = Database.Connection.CreateCommand();
                commandInsertCategory.CommandText = insertCommandCategory;
                commandInsertCategory.ExecuteNonQuery();
            } catch
            {
                MessageBox.Show("Проверьте корректность введённых данных");
                return;
            }

            AuthForm.Licences.UpdateDataGrid();
        }

        private void DriverLicenceAdd_Load(object sender, EventArgs e)
        {

        }

        //Выбор фотографии владельца
        private void selectImageOwnerButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Фотография|*.jpg;*.png;*.jpeg;*.bmp";
            openFileDialog.Multiselect = false;

            //Если пользователь выбрал фотографию
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                pathImageOwnerLabel.Text = fileName;
            }
        }

        //Выбор фотографии подписи владельца
        private void selectSignatureOwnerButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Фотография|*.jpg;*.png;*.jpeg;*.bmp";
            openFileDialog.Multiselect = false;

            //Если пользователь выбрал фотографию
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                pathSignatureOwnerLabel.Text = fileName;
            }
        }
        
        //Добавление нового водителя
        private void addDriverButton_Click(object sender, EventArgs e)
        {
            if (AuthForm.DriverAdd == null || AuthForm.DriverAdd.IsDisposed)
                AuthForm.DriverAdd = new DriverAddForm();

            AuthForm.DriverAdd.Show();
        }
    }
}
