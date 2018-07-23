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
    public partial class Form7 : Form
    {
        string connect = "datasource=localhost;database=InfoModule;username=root;";
        int next = 1;
        int pr;
        public Form7()
        {
            InitializeComponent();
            ss(next);
        }

        protected void ss(int xer)
        {
            string Query = $"select * from `quiz` where role = (select Role from Users where Login = '{global.user}') limit {xer};";
            MySqlConnection MyConn2 = new MySqlConnection(connect);
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();
            while (MyReader2.Read())
            {
                //this.textBox1.Text = MyReader2.GetString("Description");
                this.textBox1.Text = MyReader2.GetString("name");
                this.radioButton1.Text = MyReader2.GetString("win");
                this.radioButton2.Text = MyReader2.GetString("q1");
                this.radioButton3.Text = MyReader2.GetString("q2");
                this.label2.Text = xer.ToString();
                
            }
            next++;
            MyConn2.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (next > 10)
            {
                if(pr >= 5)
                {
                    MessageBox.Show($"Вы ответили верно на {pr * 10} % вопросов. Вы прошли тестирование. Поздравляю!");
                }
                else
                {
                    string Query = $"UPDATE Users SET `Enable`= false where Login = '{global.user}';";
                    MySqlConnection MyConn2 = new MySqlConnection(connect);
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;
                    MyConn2.Open();
                    MyReader2 = MyCommand2.ExecuteReader();
                    MyConn2.Close();
                    MessageBox.Show($"Вы ответили верно на {pr * 10} % вопросов. Вы не прошли тестирование. Обратитесь к системному администратору.");
                    Application.Exit();
                }
                this.Hide();
            }

            ss(next);

            if (this.radioButton1.Checked == true)
            {
                pr++;
               
            }

            this.radioButton1.Checked = false;
            this.radioButton2.Checked = false;
            this.radioButton3.Checked = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
