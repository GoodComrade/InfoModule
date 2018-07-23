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
    public partial class Form3 : Form
    {
        string connect = "datasource=localhost;database=InfoModule;username=root;";
        public Form3()
        {
            InitializeComponent();
            updat();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "delete from Users where `Login`='" + this.listBox1.Text + "';";
                MySqlConnection MyConn2 = new MySqlConnection(connect);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                this.listBox1.Items.Clear();
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MyConn2.Close();
                updat();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 f3 = new Form5();
            f3.ShowDialog();
            updat();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "SELECT * FROM Users WHERE `Login` LIKE'%" + this.textBox1.Text + "%';";
                MySqlConnection MyConn2 = new MySqlConnection(connect);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                this.listBox1.Items.Clear();
                while (MyReader2.Read())
                {
                    this.listBox1.Items.Add(MyReader2.GetString("Login"));
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updat()
        {
            string Query = "select * from `Users`;";
            MySqlConnection MyConn2 = new MySqlConnection(connect);
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();
            this.listBox1.Items.Clear();
            while (MyReader2.Read())
            {
                this.listBox1.Items.Add(MyReader2.GetString("Login"));
            }
            MyConn2.Close();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form7 f3 = new Form7();
            f3.ShowDialog();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                
                string Query = $"UPDATE Users SET Date = ADDTIME(CURRENT_TIMESTAMP, '1:0:0'), `Enable`= 'true' where Login = '{listBox1.Text}';";
                string Query1 = $"SELECT COUNT(*) from `Users` where `Login` = '{listBox1.Text}' and `Enable` = 'true';";
                MySqlConnection MyConn2 = new MySqlConnection(connect);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlCommand MyCommand3 = new MySqlCommand(Query1, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                int ind;
                int.TryParse(MyCommand3.ExecuteScalar().ToString(), out ind);
                if (ind == 0)
                {
                    MyReader2 = MyCommand2.ExecuteReader();
                    MessageBox.Show("Учетная запись активирована.");
                }
                else
                {
                    MessageBox.Show("Учетная запись уже активна.");
                }
                MyConn2.Close();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось активировать учетную запись.");
            }
            
        }
    }
}
