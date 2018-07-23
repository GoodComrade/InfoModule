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
    public partial class Form6 : Form
    {
        string connect = "datasource=localhost;database=InfoModule;username=root;";
        string Name2;
        public Form6(string Name1)
        {
            Name2 = Name1;
            InitializeComponent();
            redact(Name1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Update();
        }

        private void Update()
        {
            try
            {
                string Query = "UPDATE Info SET `Name`='" + this.textBox1.Text + "', `Description`='" + this.textBox2.Text + "', `Category`='" + this.textBox3.Text + "' WHERE `Name`='"+ Name2 +"';";
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

        private void redact(string Name1)
        {
            try
            {
                string Query = "SELECT * FROM Info WHERE `Name` = '" + Name1 + "';";
                MySqlConnection MyConn2 = new MySqlConnection(connect);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                this.textBox1.Clear();
                this.textBox2.Clear();
                this.textBox3.Clear();
                while (MyReader2.Read())
                {
                    this.textBox1.Text = MyReader2.GetString("Name");
                    this.textBox2.Text = MyReader2.GetString("Description");
                    this.textBox3.Text = MyReader2.GetString("Category");
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
