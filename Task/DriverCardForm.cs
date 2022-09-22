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
                    pathImageLabel.Text = reader.GetString("photo");
                    descriptionBox.Text = reader.GetString("description");
                }

                reader.Close();
            }
        }
    }
}
