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
    public partial class ChangeStatusForm : Form
    {
        public int Id;
        
        public ChangeStatusForm()
        {
            InitializeComponent();
            statusCB.Items.AddRange(new string[] { "активен", "утратил силу", "приостановлен", "изъят" } );

            //Слушатель для бездействия
            this.MouseMove += AuthForm.DriverAddForm_MouseMove;
        }

        //Изменение статуса и отправка в историю
        private void changeStatusButton_Click(object sender, EventArgs e)
        {
            string newStatus = statusCB.Text;
            if(newStatus.Length == 0)
            {
                MessageBox.Show("Укажите новый статус");
                return;
            }

            MySqlCommand mySqlCommand = Database.Connection.CreateCommand();
            mySqlCommand.CommandText = $"INSERT INTO `user5_db`.`history_status`(`licence_id`,`new_status`,`comment`)VALUES({Id},'{newStatus}','{commentBox.Text}');";
            mySqlCommand.ExecuteNonQuery();

            MySqlCommand updateStatusCommand = Database.Connection.CreateCommand();
            updateStatusCommand.CommandText = $"UPDATE `user5_db`.`licences` SET `status` = '{newStatus}' WHERE `id` = {Id};";
            updateStatusCommand.ExecuteNonQuery();

            MessageBox.Show("Статус обновлен");
            if (AuthForm.ChangeStatus != null && !AuthForm.ChangeStatus.IsDisposed)
                AuthForm.ChangeStatus.Close();
        }
    }
}
