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
    public partial class Procurement : Form
    {
        public Procurement()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
           InitProducts();
        }
        //在dataGridView1中显示数量小于50的商品,将CategoryID显示为CategoryName
        public void InitProducts()
        {
            //连接到数据库
            string sql = "select Products.ProductID,Products.Name,Products.Price,Products.Quantity,Categories.CategoryName from Products LEFT JOIN Categories ON  Products.CategoryID=Categories.CategoryID where Products.Quantity<50";
            Dao dao = new Dao();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Dao.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dao.DaoClose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //根据textBox1和textBox4的内容更新数据库，将textBox1的商品数量增加textBox4的数量
            string sql = "update Products set Quantity=Quantity+" + textBox4.Text + " where ProductID='" + textBox1.Text + "'";
            Dao dao = new Dao();
            dao.Execute(sql);
            InitProducts();
            MessageBox.Show("入库成功");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
        }
    }
}
