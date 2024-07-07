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
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            InitCategory();
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
        }
        //将分类信息显示在dataGridView1中
        public void InitCategory()
        {
            //连接到数据库
            string sql = "select * from Categories";
            Dao dao = new Dao();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Dao.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dao.DaoClose();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //将textBox1和textBox2中的内容插入到数据库中
            string sql = "insert into Categories values('" + textBox1.Text + "','" + textBox2.Text + "')";
            Dao dao = new Dao();
            dao.Execute(sql);
            InitCategory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //将textBox1和textBox2中的内容更新到数据库中
            string sql = "update Categories set CategoryName='" + textBox2.Text + "' where CategoryID='" + textBox1.Text + "'";
            Dao dao = new Dao();
            dao.Execute(sql);
            InitCategory();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //删除textBox1中的分类使用异常处理
            try
            {
                string sql = "delete from Categories where CategoryID='" + textBox1.Text + "'";
                Dao dao = new Dao();
                dao.Execute(sql);
                InitCategory();
            }
            catch
            {
                MessageBox.Show("删除失败");
            }
        }
        //将dataGridView1中的分类信息显示在textBox1和textBox2中
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
        }
    }
}
