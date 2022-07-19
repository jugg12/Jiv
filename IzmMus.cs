﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Jivopisi.izmenenie
{
    public partial class IzmMus : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        Action<string, int> callback;
        int oId = 0;
        int k = 0;
        public IzmMus(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void IzmMus_Load(object sender, EventArgs e)
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
                string C = "SELECT id,[Наличие оригиналов картин и копий] as [Наличие оригиналов картин и копий], [ФИО Коллекционера] as [ФИО Коллекционера], [Название музея] as [Название музея] from [Коллекционеры и музеи]";
                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
                advancedDataGridView1.Columns[0].Visible = false;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[0].Value);

            cmd.CommandText = "Update [Коллекционеры и музеи] set [Наличие оригиналов картин и копий]='" + textBox1.Text + "',[ФИО Коллекционера]=  '" + textBox2.Text + "',[Название музея]=  '" + textBox3.Text + "' where id = '" + id + "'";
            await cmd.ExecuteNonQueryAsync();

            load();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "-";
            textBox3.Text = "-";
            button1.Enabled = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;

        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                selectedRow = advancedDataGridView1.Rows[e.RowIndex];
                textBox1.Text = selectedRow.Cells[1].Value.ToString();
                textBox2.Text = selectedRow.Cells[2].Value.ToString();
                textBox3.Text = selectedRow.Cells[3].Value.ToString();
                if (textBox2.Text == "-")
                {
                    checkBox2.Checked = true;
                    textBox3.Enabled = true;
                }
                else
                {
                    checkBox1.Checked = true;
                    textBox2.Enabled = true;
                }
                textBox1.Enabled = true;
                if (textBox1.Text != "" && (textBox2.Text != "" && textBox3.Text != "") && (checkBox1.Checked == true || checkBox2.Checked == true))
                {
                    button1.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && (textBox2.Text != "" && textBox3.Text != "") && (checkBox1.Checked == true || checkBox2.Checked == true))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && (textBox2.Text != "" && textBox3.Text != "") && (checkBox1.Checked == true || checkBox2.Checked == true))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && (textBox2.Text != "" && textBox3.Text != "") && (checkBox1.Checked == true || checkBox2.Checked == true))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && k == 0)
            {
                if (textBox2.Text == "-")
                {
                    textBox2.Text = "";
                }
                textBox2.Enabled = true;
                textBox3.Text = "-";
                k++;
            }
            else if (checkBox1.Checked == true && checkBox2.Checked == true && k == 1)
            {
                MessageBox.Show("Выбрано может быть только одно значение!");
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                button1.Enabled = false;
                k = 0;
            }
            else
            {
                textBox2.Enabled = false;
                textBox2.Text = "-";
                k = 0;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true && k == 0)
            {
                if (textBox3.Text == "-")
                {
                    textBox3.Text = "";
                }
                textBox3.Enabled = true;
                textBox2.Text = "-";
                k++;
            }
            else if (checkBox1.Checked == true && checkBox2.Checked == true && k == 1)
            {
                MessageBox.Show("Выбрано может быть только одно значение!");
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                button1.Enabled = false;
                k = 0;
            }
            else
            {
                textBox3.Enabled = false;
                textBox3.Text = "-";
                k = 0;
            }
        }
    }
}
