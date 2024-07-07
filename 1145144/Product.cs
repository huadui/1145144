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
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
            this.Dock=DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
        }
        public void InitCategory()
        {
            //将comboBox2连接到数据库
            string sql = "select * from Categories";
            Dao dao = new Dao();
            IDataReader dc = dao.GetReader(sql);
            while (dc.Read())
            {
                comboBox2.Items.Add(dc["CategoryName"].ToString());
                comboBox1.Items.Add(dc["CategoryName"].ToString());
            }
            dao.DaoClose();
        }
        //刷新comboBox1和comboBox2
        public void RefreshCategory()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            InitCategory();
        }

        //将商品信息显示在dataGridView1中
        public void InitProduct()
        {
            //连接到数据库
            string sql = "select ProductID,Name,Price,Quantity,Categories.CategoryName from Products LEFT JOIN Categories ON  Products.CategoryID=Categories.CategoryID";
            Dao dao = new Dao();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Dao.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dao.DaoClose();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            InitCategory();
            InitProduct();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //查询分类为combobox2的商品
            string sql = "select ProductID,Name,Price,Quantity,Categories.CategoryName from Products LEFT JOIN Categories ON  Products.CategoryID=Categories.CategoryID where Categories.CategoryName='" + comboBox2.Text + "'";
            Dao dao = new Dao();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Dao.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dao.DaoClose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //将textbox1到textbox4的信息插入到数据库，将分类名转换为分类ID
            string sql = "insert into Products values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "',(select CategoryID from Categories where CategoryName='" + comboBox1.Text + "'))";
            Dao dao = new Dao();
            //使用异常处理，插入失败就重新输入
            try
            {
                dao.Execute(sql);
            }
            catch (Exception)
            {
                MessageBox.Show("输入有误，请重新输入");
            }
            dao.DaoClose();
            InitProduct();  

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //将textbox1到textbox4的信息更新到数据库，将分类名转换为分类ID
            string sql = "update Products set Name='" + textBox2.Text + "',Price='" + textBox3.Text + "',Quantity='" + textBox4.Text + "',CategoryID=(select CategoryID from Categories where CategoryName='" + comboBox1.Text + "') where ProductID='" + textBox1.Text + "'";
            Dao dao = new Dao();
            //使用异常处理，更新失败就重新输入
            try
            {
                dao.Execute(sql);
            }
            catch (Exception)
            {
                MessageBox.Show("输入有误，请重新输入");
            }
            dao.DaoClose();
            InitProduct();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            //判断textbox1是否为空以及表中是否有该商品
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入商品编号");
                return;
            }
            string sql1 = "select * from Products where ProductID='" + textBox1.Text + "'";
            Dao dao1 = new Dao();
            IDataReader dc = dao1.GetReader(sql1);
            if (!dc.Read())
            {
                MessageBox.Show("没有该商品");
                return;
            }
            dao1.DaoClose();
            InitProduct();
            //删除textbox1的商品
            string sql = "delete from Products where ProductID='" + textBox1.Text + "'";
            Dao dao = new Dao();
            //使用异常处理，删除失败就重新输入
            try
            {
                dao.Execute(sql);
            }
            catch (Exception)
            {
                MessageBox.Show("输入有误，请重新输入");
            }
           dao.DaoClose();
            InitProduct();

        }
        //当点击dataGridView1中的某一行时，将该行的信息显示在textbox1到textbox4中
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            InitProduct();
            RefreshCategory();
            
            
        }
    }
}
