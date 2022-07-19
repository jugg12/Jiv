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
using Zuby.ADGV;

namespace Jivopisi
{
    public partial class Vibor : Form
    {
        string query;
        DataTable dataTable = new DataTable();
        Action<string, int> callback;
        SqlConnection sqlConnect;
        string columnResultName;
        DataGridViewRow selectedRow = null;
        public Vibor(SqlConnection sqlConnect, string query, string columnResultName, Action<string, int> callback)
        {
            this.sqlConnect = sqlConnect;
            this.query = query;
            this.callback = callback;
            this.columnResultName = columnResultName;
            InitializeComponent();
            button1.Enabled = false;
        }

        private void Vibor_Load(object sender, EventArgs e)
        {

            if (sqlConnect != null && sqlConnect != null)
            {
                if (sqlConnect.State != ConnectionState.Open)
                {
                    MessageBox.Show("Подключите базу");
                    return;
                }
                try
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(this.query, this.sqlConnect);
                    adapter.Fill(dataTable);
                    advancedDataGridView1.DataSource = dataTable;
                    advancedDataGridView1.Columns[0].Visible = false;


                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }

            }
        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = advancedDataGridView1.Rows[e.RowIndex];
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedRow != null)
            {
                string result = selectedRow.Cells[columnResultName].Value.ToString();
                int id = Convert.ToInt32(selectedRow.Cells["id"].Value);


                if (id != 0 && result != null)
                {
                    callback(result, id);
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void advancedDataGridView1_DoubleClick(object sender, EventArgs e)
        {
            AdvancedDataGridView view = (sender as AdvancedDataGridView);
            string result = view.CurrentRow.Cells[columnResultName].Value.ToString();
            int id = Convert.ToInt32(view.CurrentRow.Cells["id"].Value);


            if (id != 0 && result != null)
            {
                callback(result, id);

                this.Close();
            }
        }
    }
}
