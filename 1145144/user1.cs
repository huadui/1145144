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
    public partial class user1 : Form
    {
        public user1()
        {
            InitializeComponent();
            InitCategory();
            InitCart();
            InitProduct();
            showtotal();
            
        }
        //在label1中显示当前用户的用户名
        private void user1_Load(object sender, EventArgs e)
        {
            label1.Text = Data.UNAME;
        }
        public void InitCart()
        {
            //显示当前用户的购物车的商品的名字，数量，价格
            string sql = "select Products.Name,Products.Price,ShoppingCartItems.Quantity from ShoppingCartItems LEFT JOIN Products ON ShoppingCartItems.ProductID=Products.ProductID where ShoppingCartItems.UserID='" + Data.UID + "'";
            Dao dao = new Dao();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Dao.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
            dao.DaoClose();
        }
        //在textBox3中显示当前用户的购物车的商品的总价
        public void showtotal()
        {
            string sql = "select sum(Products.Price*ShoppingCartItems.Quantity) from ShoppingCartItems LEFT JOIN Products ON ShoppingCartItems.ProductID=Products.ProductID where ShoppingCartItems.UserID='" + Data.UID + "'";
            Dao dao = new Dao();
            IDataReader dc = dao.GetReader(sql);
            if (dc.Read())
            {
                textBox3.Text = dc[0].ToString();
            }
            dao.DaoClose();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

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
            }
            dao.DaoClose();
        }
        //在dataGridView1中显示所有商品
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
        

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            
            //判断是否存在该商品
            string sql1 = "select * from Products where ProductID='" + textBox1.Text + "'";
            Dao dao = new Dao();
            IDataReader dc = dao.GetReader(sql1);
            if (!dc.Read())
            {
                MessageBox.Show("不存在该商品");
                return;
            }
            else 
            {
                //判断商品数量是否足够
                sql1 = "select * from Products where ProductID='" + textBox1.Text + "' and Quantity>=" + textBox2.Text;
                dc = dao.GetReader(sql1);
                if (dc.Read())
                {
                    //判断购物车中是否已经存在该商品，如果存在就增加数量，如果不存在就插入
                    sql1 = "select * from ShoppingCartItems where ProductID='" + textBox1.Text + "' and UserID='" + Data.UID + "'";
                    dc = dao.GetReader(sql1);
                    if (dc.Read())
                    {
                        sql1 = "update ShoppingCartItems set Quantity=Quantity+" + textBox2.Text + " where ProductID='" + textBox1.Text + "' and UserID='" + Data.UID + "'";
                        dao.Execute(sql1);
                        InitCart();
                        showtotal();
                    }
                    else {
                        //先查询textBox1中商品的价格，然后把productid,userid,quantity,价格，加入时间插入到购物车中
                        sql1 = "select Price from Products where ProductID='" + textBox1.Text + "'";
                        dc = dao.GetReader(sql1);
                        string price = "";
                        if (dc.Read())
                        {
                            price = dc[0].ToString();
                        }
                        sql1 = "insert into ShoppingCartItems values('" + textBox1.Text + "','" + Data.UID + "','" + textBox2.Text + "','" + price + "','" + DateTime.Now + "')";
                        dao.Execute(sql1);
                        InitCart();
                        showtotal();
                    }
                    
                }
                else
                {
                    MessageBox.Show("商品数量不足");
                }
            }
            dao.DaoClose();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            //清除当前用户的购物车
            string sql = "delete from ShoppingCartItems where UserID='" + Data.UID + "'";
            Dao dao = new Dao();
            dao.Execute(sql);
            InitCart();
            showtotal();
            dao.DaoClose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //退出登录
            Data.UID = "";
            Data.UNAME = "";
            loginform login = new loginform();
            this.Hide();
            login.Show();
            
            
        }
        //在dataGridView3中显示订单表
        public void Initorders()
        {
            //显示订单表
            string sql = "select * from Orders where UserID='" + Data.UID + "'";
            Dao dao = new Dao();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Dao.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView3.DataSource = dt;
            dao.DaoClose();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            //判断购物车是否为空
            string sql = "select * from ShoppingCartItems where UserID='" + Data.UID + "'";
            Dao dao = new Dao();
            IDataReader dc = dao.GetReader(sql);
            if (!dc.Read())
            {
                MessageBox.Show("购物车为空");
                return;
            }
            else
            {
                //将购物车中对应的商品数量在products表中减少
                sql = "select * from ShoppingCartItems where UserID='" + Data.UID + "'";
                dc = dao.GetReader(sql);
                while (dc.Read())
                {
                    string productid = dc["ProductID"].ToString();
                    string quantity = dc["Quantity"].ToString();
                    sql = "update Products set Quantity=Quantity-" + quantity + " where ProductID='" + productid + "'";
                    dao.Execute(sql);
                    InitProduct();
                }
                dao.DaoClose();
                //将userid,时间，总价插入到orders表中
                sql = "insert into Orders values('" + Data.UID + "','" + DateTime.Now + "','" + textBox3.Text + "')";
                dao = new Dao();
                dao.Execute(sql);
                Initorders();
                //将购物车中的商品插入到orderitems表中
                sql = "select * from ShoppingCartItems where UserID='" + Data.UID + "'";
                dao = new Dao();
                dc = dao.GetReader(sql);
                while (dc.Read())
                {
                    string productid = dc["ProductID"].ToString();
                    string quantity = dc["Quantity"].ToString();
                    string price = dc["UnitPrice"].ToString();
                    sql = "insert into OrderItems values((select max(OrderID) from Orders),'" + productid + "','" + quantity + "','" + price + "')";
                    dao.Execute(sql);
                }

                //清空购物车
                sql = "delete from ShoppingCartItems where UserID='" + Data.UID + "'";
                dao = new Dao();
                dao.Execute(sql);
                InitCart();
                showtotal();
                dao.DaoClose();
            }
        }
        
        
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            Initorders();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            //在dataGridView4中显示orderItems表中，当前选择的订单的商品
            string sql = "select Products.Name,Products.Price,OrderItems.Quantity from OrderItems LEFT JOIN Products ON OrderItems.ProductID=Products.ProductID where OrderItems.OrderID='" + dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[0].Value.ToString() + "'";
            Dao dao = new Dao();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Dao.GetConnection());
            DataTable dt = new DataTable();
                adapter.Fill(dt);
            dataGridView4.DataSource = dt;
            dao.DaoClose();

            

        }
    }
}
