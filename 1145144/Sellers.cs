using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1145144
{
    public partial class Sellers : Form
    {
        public Sellers()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            InitUsers();
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
        }
        //在dataGridView1中显示所有的用户信息
        public void InitUsers()
        {
            //连接到数据库
            string sql = "select * from Users";
            Dao dao = new Dao();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Dao.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dao.DaoClose();
        }
        //将dataGridView1中的用户信息显示在textBox1到textBox4中
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //当输入框中的内容为空时，显示所有的用户信息
            if (textBox5.Text == "")
            {
                InitUsers();
                return;
            }
            //查询用户名为textBox5的用户
            string sql = "select * from Users where Name='" + textBox5.Text + "'";
            Dao dao = new Dao();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Dao.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dao.DaoClose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //将textBox1到textBox4中的信息插入到数据库中
            string sql = "insert into Users values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
            Dao dao = new Dao();
            dao.Execute(sql);
            InitUsers();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //将textBox1到textBox4中的信息更新到数据库中
            string sql = "update Users set Name='" + textBox2.Text + "',Psw='" + textBox3.Text + "',PhoneNum='" + textBox4.Text + "' where ID='" + textBox1.Text + "'";
            Dao dao = new Dao();
            dao.Execute(sql);
            InitUsers();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //删除textBox1中的用户
            string sql = "delete from Users where ID='" + textBox1.Text + "'";
            Dao dao = new Dao();
            dao.Execute(sql);
            InitUsers();

        }
    }
}
