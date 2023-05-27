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

    public partial class Dobav : Form
    {
        DataBase dataBase = new DataBase();

        public Dobav()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var seksia = textBox1.Text;
                var name = textBox2.Text;
            var kabinet = textBox3.Text;
            int price;

            if (int.TryParse(textBox4.Text, out price))
            {
                var addQuery = $"insert into dss_db (seksia, name_trener, kabinet, price) values ('{seksia}','{name}','{kabinet}','{price}')";

                var command = new SqlCommand(addQuery, dataBase.GetSqlConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Не удалось создать запись ", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            dataBase.CloseConnection();
        }

        private void Dobav_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
