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

namespace Jivopisi
{
    public partial class Glav : Form
    {
        SqlConnection sqlConnect = null;
        
        public Glav()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hudojnik form = new Hudojnik(this.sqlConnect, this);
            this.Hide();
            form.Show();
        }

        private async void Glav_Load(object sender, EventArgs e)
        {
            sqlConnect = new SqlConnection(@"Data Source=DESKTOP-8T9EGI7\SQLEXPRESS;Initial Catalog=" + "jivopis" + ";Integrated Security=True");
            await sqlConnect.OpenAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Kart form = new Kart(this.sqlConnect, this);
            this.Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Mus form = new Mus(this.sqlConnect, this);
            this.Hide();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Auction form = new Auction(this.sqlConnect, this);
            this.Hide();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Kollection form = new Kollection(this.sqlConnect, this);
            this.Hide();
            form.Show();
        }
    }
}
