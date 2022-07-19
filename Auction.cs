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
    
    public partial class Auction : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        public Auction(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            button4.Enabled = false;
            button8.Enabled = false;
        }

        private void Auction_Load(object sender, EventArgs e)
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
                string C = "SELECT id,[Дата проведения] as [Дата проведения], [Список выставленных шедевров] as [Список выставленных шедевров], [Цена] as [Цена] from [Аукционеры и комиссионки]";
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

        private void Auction_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Вы действительно хотите закрыть форму ''Аукционы и комиссионки?''", "Завершение программы", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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

        private async void button8_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[0].Value);
            cmd.CommandText = "delete from [Аукционеры и комиссионки] where id='" + id + "'";
            await cmd.ExecuteNonQueryAsync();
            load();
            cls();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cls();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DobavAuction form = new DobavAuction(this.sqlConnect, this);

            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            IzmAuction form = new IzmAuction(this.sqlConnect, this);

            form.Show();
        }
    }
}
