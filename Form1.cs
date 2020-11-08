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

namespace mysqlDemo2
{
    public partial class Form1 : Form
    {
        MySqlConnection c;
        int id = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            c.Open();
            string sql = "INSERT INTO tb_book(Name, Price) VALUES(@Name, @Price)";
            MySqlCommand cmd = new MySqlCommand(sql, c);
            cmd.Parameters.AddWithValue("@Name", txtName.Text);
            cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            MessageBox.Show("insert success", "insert", MessageBoxButtons.OK);

            c.Close();

            loadData(); // INSERT HERE
        }

        void loadData()
        {
            c.Open();
            string sql = "SELECT * FROM tb_book";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, c);
            DataTable tb = new DataTable();
            adapter.Fill(tb);

            dataGridView1.DataSource = tb;

            c.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string str = "server=localhost; userid=root; password=; database=db_mysql";
            c = new MySqlConnection(str);

            loadData(); // INSERT HERE
        }

        // cancel button
        private void button4_Click(object sender, EventArgs e)
        {
            id = 0;
            txtName.Text = "";
            txtPrice.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex < dataGridView1.Rows.Count - 1)
            {
                if (rowIndex > -1)
                {
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    string idValue = row.Cells[0].Value.ToString();
                    id = Convert.ToInt32(idValue);

                    txtName.Text = row.Cells[1].Value.ToString();
                    txtPrice.Text = row.Cells[2].Value.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                c.Open();
                string sql = "UPDATE tb_book SET Name = @Name, Price = @Price WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, c);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                MessageBox.Show("update success", "update", MessageBoxButtons.OK);
                id = 0;

                c.Close();

                loadData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                c.Open();
                string sql = "DELETE FROM tb_book WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, c);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                id = 0;
                MessageBox.Show("deleted", "delete data", MessageBoxButtons.OK);

                c.Close();

                loadData();
            }
        }
    }
}
