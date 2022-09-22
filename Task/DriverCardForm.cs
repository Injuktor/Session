using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task
{
    public partial class DriverCardForm : Form
    {
        private int _id;

        public DriverCardForm()
        {
            InitializeComponent();

            //Слушатель для бездействия
            this.MouseMove += AuthForm.DriverAddForm_MouseMove;
        }

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;

                string selectCommand = $"SELECT * FROM drivers WHERE id={_id}";
                MySqlCommand command = Database.Connection.CreateCommand();
                command.CommandText = selectCommand;
                MySqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    guidBox.Text = Convert.ToString(reader.GetInt32("id"));
                    surnameBox.Text = reader.GetString("surname");
                    nameBox.Text = reader.GetString("name");
                    middlenameBox.Text = reader.GetString("middlename");
                    passSerialBox.Text = reader.GetString("pass_serial");
                    passNumberBox.Text = reader.GetString("pass_number");
                    addrRegCityCB.Text = reader.GetString("address_city");
                    addrRegBox.Text = reader.GetString("address");
                    addrLifeCityCB.Text = reader.GetString("address_life_city");
                    addrLifeBox.Text = reader.GetString("address_life");
                    companyBox.Text = reader.GetString("company");
                    jobnameBox.Text = reader.GetString("jobname");
                    phoneBox.Text = reader.GetString("phone");
                    emailBox.Text = reader.GetString("email");
                    pathImageLabel.Text = reader.GetString("photo");
                    descriptionBox.Text = reader.GetString("description");
                }

                reader.Close();

                MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT * FROM licences WHERE driver_id={_id}", Database.Connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                licencesGrid.DataSource = dataSet.Tables[0];

                //Перебор всех строк для индикатора
                foreach (DataGridViewRow row in licencesGrid.Rows)
                {
                    //Проверка клетки со статусом
                    if (row.Cells["status"].Value != null && Convert.ToString(row.Cells["status"].Value).Length > 0)
                    {
                        string status = Convert.ToString(row.Cells["status"].Value);

                        DataGridViewImageCell dataGridViewImageCell = (DataGridViewImageCell)row.Cells["Indicator"];

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
        }

        //Удаление водителя
        private void deleteButton_Click(object sender, EventArgs e)
        {
            //Диалог подтверждения удаления
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить водителя?", "Подтверждение", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteCommand = $"DELETE FROM drivers WHERE id={_id}";
                MySqlCommand command = Database.Connection.CreateCommand();
                command.CommandText = deleteCommand;
                command.ExecuteNonQuery();
                
                //Обновление водителей
                if (AuthForm.Drivers != null && !AuthForm.Drivers.IsDisposed)
                    AuthForm.Drivers.UpdateDataGrid();

                //Закрытие формы
                if (AuthForm.DriverCard != null && !AuthForm.DriverCard.IsDisposed)
                    AuthForm.DriverCard.Close();
            }
        }

        //Обновление информации о водителе
        private void saveDriverButton_Click(object sender, EventArgs e)
        {
            int guid = _id;
            string surname = surnameBox.Text;
            string name = nameBox.Text;
            string middlename = middlenameBox.Text;
            string passSerial = passSerialBox.Text;
            string passNumber = passNumberBox.Text;
            string addrRegCity = addrRegCityCB.Text;
            string addrReg = addrRegBox.Text;
            string addrLifeCity = addrLifeCityCB.Text;
            string addrLife = addrLifeBox.Text;
            string company = companyBox.Text;
            string jobname = jobnameBox.Text;
            string phone = phoneBox.Text;
            string email = emailBox.Text;
            string pathPhoto = pathImageLabel.Text.Replace('\\', '/');
            string description = descriptionBox.Text;

            //Проверка на то, что в поле что-то введено
            if (guidBox.Text.Length == 0)
            {
                MessageBox.Show("Введите GUID водителя");
                return;
            }

            //Конвертация guid из строки в число
            try { guid = Convert.ToInt32(guidBox.Text); } catch { MessageBox.Show("Некорректный GUID"); return; }

            if (surname.Length == 0)
            {
                MessageBox.Show("Введите фамилию водителя");
                return;
            }
            if (name.Length == 0)
            {
                MessageBox.Show("Введите имя водителя");
                return;
            }
            if (middlename.Length == 0)
            {
                MessageBox.Show("Введите отчество водителя");
                return;
            }
            if (passSerial.Length == 0)
            {
                MessageBox.Show("Введите серию паспорта водителя");
                return;
            }
            if (passNumber.Length == 0)
            {
                MessageBox.Show("Введите номер паспорта водителя");
                return;
            }
            if (phone.Length == 0)
            {
                MessageBox.Show("Введите номер телефона водителя");
                return;
            }
            if (email.Length == 0)
            {
                MessageBox.Show("Введите email водителя");
                return;
            }
            //Валидация email
            try { new MailAddress(email); } catch { MessageBox.Show("Некорректный email"); return; }

            if (pathPhoto.Length == 0)
            {
                MessageBox.Show("Выберите фотографию водителя");
                return;
            }

            //Запрос на обновление данных водителя
            string updateCommand = $"UPDATE `user5_db`.`drivers`SET `surname` = '{surname}',`name` = '{name}',`middlename` = '{middlename}',`pass_serial` = '{passSerial}',`pass_number` = '{passNumber}'," +
                $"`address_city` = '{addrRegCity}',`address` = '{addrReg}',`address_life_city` = '{addrLifeCity}',`address_life` = '{addrLife}',`company` = '{company}',`jobname` = '{jobname}',`phone` = '{phone}',`email` = '{email}',`photo` = '{pathPhoto}', `description` = '{description}' WHERE `id` = '{_id}';";
            try
            {
                MySqlCommand mySqlCommand = Database.Connection.CreateCommand();
                mySqlCommand.CommandText = updateCommand;
                mySqlCommand.ExecuteNonQuery();

                MessageBox.Show("Информация о водителе обновлена");
            }
            catch
            {
                MessageBox.Show("Произошла ошибка. Проверьте корректность введённых данных");
                return;
            }

            AuthForm.Drivers.UpdateDataGrid();
        }

        private void selectImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Фотография|*.jpg;*.png";
            openFileDialog.Multiselect = false;

            //Если пользователь выбрал фотографию
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                //Обработчик для фотографии
                try
                {
                    Image image = Image.FromFile(fileName);

                    //Проверка соотношения сторон фотографии
                    if (Convert.ToDouble(image.Width) / Convert.ToDouble(image.Height) > 1.7)
                    {
                        MessageBox.Show("Пожалуйста, выберите фотографию 3:4");
                        return;
                    }

                    //Проверка на размер фотографии
                    if (new FileInfo(fileName).Length > 2097152)
                    {
                        MessageBox.Show("Фотография должна быть не более 2МБ");
                        return;
                    }

                    //Проверка на ориентацию изображения
                    if (Array.IndexOf(image.PropertyItems, 274) > -1)
                    {
                        //Ориентация изображениея
                        int orientation = image.GetPropertyItem(274).Value[0];

                        //6 и 8 - Вертикальные изображения
                        if (orientation != 6 && orientation != 8)
                        {
                            MessageBox.Show("Выберите вертикальное изображение");
                            return;
                        }
                        return;
                    }

                    pathImageLabel.Text = fileName;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show("Нет прав на использование этой фотографии");
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при считывании фотографии" +
                        "\nПопробуйте выбрать другую фотографию");
                    return;
                }
            }
        }
    }
}
