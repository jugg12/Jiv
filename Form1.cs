using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jivopisi
{
    public partial class Form1 : Form
    {
        string log = "1";
        string pass = "1";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == log && textBox2.Text == pass)
            {
                Glav form = new Glav();
                this.Hide();
                form.Show();
            }
            else
            {
                if ((textBox1.Text == "" && textBox2.Text == "") || (textBox1.Text == " " && textBox2.Text == " "))
                {
                    MessageBox.Show("Заполните поля!");
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }
        }
    }
}
