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
    public partial class DriverAddForm : Form
    {
        public DriverAddForm()
        {
            InitializeComponent();

            addrRegCityCB.Items.AddRange(new string[] { "Москва", "Санкт-Петербург", "Красноярск", "Краснодар", "Краснокаменск", "Канск", "Зеленогорск", "Ярославль" });
            addrLifeCityCB.Items.AddRange(new string[] { "Москва", "Санкт-Петербург", "Красноярск", "Краснодар", "Краснокаменск", "Канск", "Зеленогорск", "Ярославль" });

            //Слушатель для бездействия
            this.MouseMove += AuthForm.DriverAddForm_MouseMove;
        }

        //Добавление водителя
        private void addDriverButton_Click(object sender, EventArgs e)
        {
            int guid = 0; //id Для БД
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
            if(guidBox.Text.Length == 0)
            {
                MessageBox.Show("Введите GUID водителя");
                return;
            }

            //Конвертация guid из строки в число
            try { guid = Convert.ToInt32(guidBox.Text); } catch { MessageBox.Show("Некорректный GUID"); return; }

            if(surname.Length == 0)
            {
                MessageBox.Show("Введите фамилию водителя");
                return;
            }
            if(name.Length == 0)
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

            string insertCommand = $"INSERT INTO `user5_db`.`drivers` (`id`,`surname`,`name`,`middlename`,`pass_serial`,`pass_number`,`address_city`,`address`,`address_life_city`,`address_life`,`company`,`jobname`,`phone`,`email`,`photo`,`description`) VALUES " +
                $"({guid},'{surname}','{name}','{middlename}','{passSerial}','{passNumber}','{addrRegCity}','{addrReg}','{addrLifeCity}','{addrLife}','{company}','{jobname}','{phone}','{email}','{pathPhoto}','{description}');";

            try
            {
                MySqlCommand mySqlCommand = Database.Connection.CreateCommand();
                mySqlCommand.CommandText = insertCommand;
                mySqlCommand.ExecuteNonQuery();

                MessageBox.Show("Водитель успешно добавлен!");
            }
            catch
            {
                MessageBox.Show("Произошла ошибка. Проверьте корректность введённых данных");
                return;
            }

            AuthForm.Drivers.UpdateDataGrid();
        }

        //Выбор фотографии
        private void selectImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Фотография|*.jpg;*.png";
            openFileDialog.Multiselect = false;
            
            //Если пользователь выбрал фотографию
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                //Обработчик для фотографии
                try
                {
                    Image image = Image.FromFile(fileName);

                    //Проверка соотношения сторон фотографии
                    if(Convert.ToDouble(image.Width) / Convert.ToDouble(image.Height) > 1.7)
                    {
                        MessageBox.Show("Пожалуйста, выберите фотографию 3:4");
                        return;
                    }

                    //Проверка на размер фотографии
                    if(new FileInfo(fileName).Length > 2097152)
                    {
                        MessageBox.Show("Фотография должна быть не более 2МБ");
                        return;
                    }

                    //Проверка на ориентацию изображения
                    if(Array.IndexOf(image.PropertyItems, 274) > -1)
                    {
                        //Ориентация изображениея
                        int orientation = image.GetPropertyItem(274).Value[0];

                        //6 и 8 - Вертикальные изображения
                        if(orientation != 6 && orientation != 8)
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
