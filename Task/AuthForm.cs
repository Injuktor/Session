using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task
{
    public partial class AuthForm : Form
    {
        private int passwordAttempts;
        private bool authLocked;
        private long timeToUnlock;
        
        //Время для бездействия
        private static long _timeAfk;

        //Формы
        private static AuthForm _authForm;
        private static DriversForm _driversForm;
        private static DriverAddForm _driverAddForm;
        private static DriverCardForm _driverCardForm;

        public AuthForm()
        {
            InitializeComponent();
            Database.Init();
            _authForm = this;
        }

        //Авторизация пользователя
        private void authButton_Click(object sender, EventArgs e)
        {
            //Проверка на пустое поле логина
            if(loginBox.Text.Length == 0)
            {
                MessageBox.Show("Введите логин");
                return;
            }
            //Проверка на пустое поле пароля
            if (passwordBox.Text.Length == 0)
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            //Проверка существования файла блокировки
            if(File.Exists("lockauth.txt"))
            {
                //Обработчик, чтобы в случае ошибки не вылетело приложение
                try
                {
                    //Считывание времени блокировки из файла
                    timeToUnlock = Convert.ToInt32(File.ReadAllText("lockauth.txt"));
                    authLocked = true;
                } catch { 
                    //Удаление файла в случае неудачи считывания из файла
                    File.Delete("lockauth.txt"); 
                }
            }

            //Проверка времени блокировки, для снятия блокировки
            if(timeToUnlock <= (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000L) && authLocked)
            {
                authLocked = false;
                passwordAttempts = 0;
                File.Delete("lockauth.txt");
            }

            //Блокировка авторизации
            if (passwordAttempts >= 3 && !authLocked)
            {
                MessageBox.Show("Авторизация заблокирована");
                authLocked = true;
                
                //Блокировка авторизации на 60 секунд
                timeToUnlock = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000L) + 60;

                //Запись в файл, чтобы при перезапуске блокировка не сбрасывалась
                File.WriteAllText("lockauth.txt", Convert.ToString(timeToUnlock));
                return;
            }

            if (!authLocked)
            {
                //Проверка авторизации
                if (loginBox.Text == "inspector" && passwordBox.Text == "inspector")
                {
                    this.Hide();
                    _driversForm = new DriversForm();
                    _driversForm.Show();
                    _timeAfk = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000L) + 10;
                }
                else
                {
                    MessageBox.Show("Неверные данные");
                    ++passwordAttempts;
                }
            } else
            {
                MessageBox.Show("Авторизация заблокирована");
            }
        }

        private void AuthForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Database.Connection != null)
            {
                Database.Connection.Close();
            }
        }

        //Метод бездействия
        public static void DriverAddForm_MouseMove(object sender, MouseEventArgs e)
        {
            //Если прошло время, которое было допустимо
            if (AuthForm.TimeAfk <= (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000L))
            {
                if (AuthForm.DriverAdd != null && !AuthForm.DriverAdd.IsDisposed)
                    AuthForm.DriverAdd.Close();

                if (AuthForm.Drivers != null && !AuthForm.Drivers.IsDisposed)
                    AuthForm.Drivers.Close();

                if (AuthForm.DriverCard != null && !AuthForm.DriverCard.IsDisposed)
                    AuthForm.DriverCard.Close();

                if (AuthForm.Auth == null || AuthForm.Auth.IsDisposed)
                    AuthForm.Auth = new AuthForm();

                AuthForm.Auth.Show();
                return;
            }
            else
            {
                //Если время не вышло, обновить таймер
                AuthForm.TimeAfk = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000L) + 10;
            }
        }

        //Свойство для доступа к форме
        public static AuthForm Auth
        {
            get { return _authForm; }
            set { _authForm = value; }
        }

        public static DriversForm Drivers
        {
            get { return _driversForm; }
            set { _driversForm = value; }
        }

        public static DriverAddForm DriverAdd
        {
            get { return _driverAddForm; }
            set { _driverAddForm = value; }
        }

        public static DriverCardForm DriverCard
        {
            get { return _driverCardForm; }
            set { _driverCardForm = value; }
        }

        public static long TimeAfk
        {
            get { return _timeAfk; }
            set { _timeAfk = value; }
        }
    }
}
