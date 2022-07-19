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
    public partial class Hudojnik : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        
        public Hudojnik(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            button4.Enabled = false;
            button8.Enabled = false;
        }

        private void Hudojnik_Load(object sender, EventArgs e)
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
                string C = "SELECT id,[ФИО] as [ФИО], [Возраст] as [Возраст], [Стаж] as [Стаж], [Стиль написания картины] as [Стиль написания картины] from Художник";
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
            cmd.CommandText = "delete from [Художник] where id='" + id + "'";
            await cmd.ExecuteNonQueryAsync();
            load();
            cls();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DobavHud form = new DobavHud(this.sqlConnect, this);

            form.Show();
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

        private void Hudojnik_FormClosing(object sender, FormClosingEventArgs e)
        {
           
                DialogResult dialog = MessageBox.Show("Вы действительно хотите закрыть форму ''Художники?''", "Завершение программы", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {

                    mainForm.Show();
                  
                }
                else
                {
                    e.Cancel = true;
                }
            
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            IzmHud form = new IzmHud(this.sqlConnect, this);

            form.Show();
        }
    }
}
