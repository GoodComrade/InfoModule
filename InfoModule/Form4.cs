using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace InfoModule
{
    public partial class Form4 : Form
    {
        string connect = "datasource=localhost;database=InfoModule;username=root;";
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            MySqlConnection MyConn2 = new MySqlConnection(connect);
            MySqlCommand upd3 = new MySqlCommand("SELECT COUNT(*) FROM `Users` WHERE `Login` = '" + this.textBox1.Text + "' and `Pass` = '" + this.textBox2.Text + "' ;", MyConn2);
            MyConn2.Open();
                if (upd3.ExecuteScalar().ToString() == "1")
                {
                    MySqlConnection MyConn1 = new MySqlConnection(connect);
                    MySqlCommand upd2 = new MySqlCommand("SELECT * FROM `Users` WHERE `Login` = '" + this.textBox1.Text + "' and `Pass` = '" + this.textBox2.Text + "' ;", MyConn1);
                    MySqlDataReader MyReader2;
                    MyConn1.Open();
                    MyReader2 = upd2.ExecuteReader();
                    while (MyReader2.Read())
                    {
                        if (MyReader2.GetString("Enable") == "true")
                        {
                            global.user = this.textBox1.Text;
                            Form1 f1 = new Form1(MyReader2.GetString("Date"));
                            f1.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Учетная запись заблокирована. Обратитесь к системному администратору");
                        }
                        
                    }
                }
                else
                {
                    this.label4.Text = "Неверный логин или пароль";

                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
