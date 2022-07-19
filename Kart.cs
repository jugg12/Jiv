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
using Jivopisi.izmenenie;
using Jivopisi.dobavlenie;

namespace Jivopisi
{
    public partial class Kart : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        public Kart(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            button4.Enabled = false;
            button8.Enabled = false;
        }

        private void Kart_Load(object sender, EventArgs e)
        {
         
            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
            load();
        }
        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[Картина] as [Картина], [ФИО Художника] as [ФИО Художника], [Дата создания] as [Дата создания], [Жанр картины] as [Жанр картины] from Картина";
                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
                advancedDataGridView1.Columns[0].Visible = false;
            }
        }
        void cls()
        {
            selectedRow = null;
            button4.Enabled = false;
            button8.Enabled = false;
            advancedDataGridView1.ClearSelection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cls();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load();
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[0].Value);
            cmd.CommandText = "delete from Картина where id='" + id + "'";
            await cmd.ExecuteNonQueryAsync();
            load();
            cls();
        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                selectedRow = advancedDataGridView1.Rows[e.RowIndex];

                button4.Enabled = true;
                button8.Enabled = true;

            }
        }

        private void Kart_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult dialog = MessageBox.Show("Вы действительно хотите закрыть форму ''Картины?''", "Завершение программы", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {

                mainForm.Show();

            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DobavKart form = new DobavKart(this.sqlConnect, this);

            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            IzmKart form = new IzmKart(this.sqlConnect, this);

            form.Show();
        }
    }
}
