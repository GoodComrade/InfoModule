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
    public partial class Form1 : Form
    {
        string date;
        string date1;
        string connect = "datasource=localhost;database=InfoModule;username=root;";
        Timer MyTimer = new Timer();
        public Form1(string str)
        {
            
            this.date = str;

            MyTimer.Interval = (1 * 10); // 45 mins
            MyTimer.Tick += new EventHandler(l1);
            MyTimer.Start();


            InitializeComponent();
            this.updateits();
        }


        private void updateits()
        {
            try
            {
                string Query1 = $"select * from Info;";
                string Query2 = $"select * from Info where Category = (select Role from Users where Login = '{global.user}');";

                MySqlConnection MyConn2 = new MySqlConnection(connect);
                MySqlCommand MyCommand1 = new MySqlCommand(Query1, MyConn2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query2, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                if (global.user == "admin")
                {
                    MyReader2 = MyCommand1.ExecuteReader();
                    this.label1.Visible = false;
                    this.button7.Visible = false;
                }
                else
                {
                    this.button1.Visible = false;
                    this.button3.Visible = false;
                    this.button5.Visible = false;
                    this.button6.Visible = false;
                    MyReader2 = MyCommand2.ExecuteReader();
                }
                this.listBox1.Items.Clear();
                this.textBox3.Text = "";
                while (MyReader2.Read())
                {
                    this.listBox1.Items.Add(MyReader2.GetString("Name"));
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f3 = new Form2();
            f3.ShowDialog();
            this.updateits();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Query = "select * from `Info` where `Name` = '" + this.listBox1.Text + "';";
            MySqlConnection MyConn2 = new MySqlConnection(connect);
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();
            while (MyReader2.Read())
            {
                this.textBox3.Text = MyReader2.GetString("Description");
            }
            MyConn2.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "SELECT * FROM Info WHERE `Name` LIKE'%" + this.textBox1.Text + "%';";
                MySqlConnection MyConn2 = new MySqlConnection(connect);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                this.listBox1.Items.Clear();
                while (MyReader2.Read())
                {
                    this.listBox1.Items.Add(MyReader2.GetString("Name"));
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "delete from Info where `Name`='" +this.listBox1.Text+ "';";
                MySqlConnection MyConn2 = new MySqlConnection(connect);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                this.listBox1.Items.Clear();
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MyConn2.Close();
                updateits();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form6 f3 = new Form6(this.listBox1.Text);
            f3.ShowDialog();
            this.updateits();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 f2 = new Form3();
            f2.ShowDialog();
        }

        private void l1(object sender, EventArgs e)
        {
            DateTime thisDay = DateTime.Now;
            this.date1 = thisDay.ToString();

            DateTime d1 =  DateTime.Parse(date);
            DateTime d2 =  DateTime.Parse(date1);
            TimeSpan d3 = d1.Subtract(d2);
            string d3day = d3.Days.ToString();
            string d3hour = d3.Hours.ToString();
            string d3minutes = d3.Minutes.ToString();
            string d3sec = d3.Seconds.ToString();
            label1.Text = "Дни " + d3day + " Часы " + d3hour + " Минуты " + d3minutes + " Секунды " + d3sec;

            if (d1 <= d2)
            {
                MyTimer.Stop();
                //MessageBox.Show("Время теста");
                this.button7.Enabled = true;
                label1.Hide();
                
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.ShowDialog();
        }
    } 

}
