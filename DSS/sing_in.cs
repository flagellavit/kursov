using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DSS
{
    public partial class sing_in : Form
    { 
        DataBase dataBase = new DataBase();
        public sing_in()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
        }

        private void sing_in_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        
            textBox1.MaxLength = 50;
            textBox2.MaxLength = 50;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            var login = textBox1.Text;
            var password = textBox2.Text;

            string querystring = $"insert into register(login_user, password_user) values('{login}', '{password}')";

            SqlCommand command = new SqlCommand(querystring,dataBase.GetSqlConnection());

            dataBase.openConnection();
            

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                log_in frm_login=new log_in();
                this.Hide();
                frm_login.ShowDialog();
            }
            else
        {
                MessageBox.Show("Аккаунт не создан!");
            }
            dataBase.CloseConnection();
        }
        
        private Boolean checkuser()
        {
            
            var loginUser=textBox1.Text;
            var passUser=textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querytystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user='{passUser}'";

            SqlCommand command = new SqlCommand(querytystring,dataBase.GetSqlConnection());


            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("есть такой польователь придумайте другой логин ");
                return  true;
            }
            else
            {
                MessageBox.Show("Такого пользователя нет");
                return false;
            }


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
        

    }

