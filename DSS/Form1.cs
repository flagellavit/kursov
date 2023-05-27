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
    enum RowState
    {
        Existede,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Form1 : Form
    {

        DataBase dataBase = new DataBase();
        int selectedRow;
        public Form1()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "id");

            dataGridView1.Columns.Add("seksia", "Секция");
            dataGridView1.Columns.Add("name_trener", "Фамилия тренера");
            dataGridView1.Columns.Add("kabinet", "Кабинет");
            dataGridView1.Columns.Add("price", "Цена за занятие");
            dataGridView1.Columns.Add("IsNew",String.Empty);

        }

        private void ClearFields()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetInt32(3), record.GetInt32(4) ,RowState.ModifiedNew) ;


        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string queryString = $"select * from dss_db";
            SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection());
            dataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             selectedRow = e.RowIndex;
            if(e.RowIndex>=0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[2].Value.ToString();
                textBox5.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dobav adffrm = new Dobav();
            adffrm.Show();


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string searchString = $"select * from dss_db where concat (id , seksia , name_trener, kabinet, price) like '%" + textBox1.Text + "%'";
            SqlCommand com = new SqlCommand(searchString,dataBase.GetSqlConnection());

            dataBase.openConnection();
            SqlDataReader read = com.ExecuteReader();

            while(read.Read())

            {
                ReadSingleRow(dgw, read);   

            }
            read.Close();

        }
        private void deleteRow()
        {
            int index= dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;
            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == String.Empty)
            {
                dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;
                return;
            }

            dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;

        }

        private void Update()
        {
            dataBase.openConnection();

            for(int index = 0; index<dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[5].Value;
                if (rowState == RowState.Existede)
                    continue;

                if(rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from dss_db where id={id}";

                    var command = new SqlCommand(deleteQuery,dataBase.GetSqlConnection());
                }

            }

            dataBase.CloseConnection();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            deleteRow();
            ClearFields();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        ClearFields();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
