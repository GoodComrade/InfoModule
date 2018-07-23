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
    public partial class Form2 : Form
    {
        string connect = "datasource=localhost;database=InfoModule;username=root;";
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection MyConn2 = new MySqlConnection(connect);
                MySqlCommand upd3 = new MySqlCommand("SELECT COUNT(*) FROM `Info` WHERE `Name` = '" + this.textBox1.Text + "' or `Description` = '" + this.textBox2.Text + "';", MyConn2);
                MyConn2.Open();
                int ind;
                int.TryParse(upd3.ExecuteScalar().ToString(), out ind);
                if (ind == 0)
                {
                    this.insert();
                }
                else
                {
                    MessageBox.Show("Занято");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Что то пошло не так, проверьте состояние БД.");
            }
        }

        private void insert()
        {
            try
            {
                string Query = "insert into  `Info` (`Name`, `Description`, `Category`) values('" + this.textBox1.Text + "','" + this.textBox2.Text + "','" + this.textBox3.Text + "');";
                MySqlConnection MyConn2 = new MySqlConnection(connect);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MyConn2.Close();
                this.Hide();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
