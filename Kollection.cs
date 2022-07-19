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
using Jivopisi.dobavlenie;
using Jivopisi.izmenenie;

namespace Jivopisi
{
    public partial class Kollection : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        public Kollection(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            button4.Enabled = false;
            button8.Enabled = false;
        }

        private void Kollection_Load(object sender, EventArgs e)
        {
            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
            load();

        }

        private async void button8_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[0].Value);
            cmd.CommandText = "delete from [Собственная коллекция] where id='" + id + "'";
            await cmd.ExecuteNonQueryAsync();
            load();
            cls();
        }
        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[Название картины] as [Название картины], [Количество] as [Количество] from [Собственная коллекция]";
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

        private void button2_Click(object sender, EventArgs e)
        {
            load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cls();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DobavKollection form = new DobavKollection(this.sqlConnect, this);

            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            IzmKollection form = new IzmKollection(this.sqlConnect, this);

            form.Show();
        }

        private void Kollection_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Вы действительно хотите закрыть форму ''Собственная коллекция?''", "Завершение программы", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {

                mainForm.Show();

            }
            else
            {
                e.Cancel = true;
            }
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
    }
}
