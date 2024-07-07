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
    public partial class Bills : Form
    {
        public Bills()
        {
            InitializeComponent();
            InitOrders();
            this.monthCalendar1.DateChanged += new DateRangeEventHandler(this.monthCalendar1_DateChanged);
            this.Dock = DockStyle.Fill;
        }
        //将Orders表中的信息显示在dataGridView1中，将userid显示为用户名
        public void InitOrders()
        {
            //连接到数据库
            string sql = "select Orders.OrderID,Users.Name,Orders.OrderDate,Orders.TotalAmount from Orders LEFT JOIN Users ON Orders.UserID=Users.ID";
            Dao dao = new Dao();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Dao.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dao.DaoClose();
        }




        private void button4_Click(object sender, EventArgs e)
        {
            //查询用户名为textBox1的订单
            string sql = "select Orders.OrderID,Users.Name,Orders.OrderDate,Orders.TotalAmount from Orders LEFT JOIN Users ON Orders.UserID=Users.ID where Users.Name='" + textBox1.Text + "'";
            Dao dao = new Dao();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Dao.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            showtotal();
            dao.DaoClose();
        }
        
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            //当点击monthCalendar1时，显示当天内的订单使用between实现
            string sql = "select Orders.OrderID,Users.Name,Orders.OrderDate,Orders.TotalAmount from Orders LEFT JOIN Users ON Orders.UserID=Users.ID where Orders.OrderDate between '" + monthCalendar1.SelectionStart + "' and '" + monthCalendar1.SelectionEnd + "'";
            Dao dao = new Dao();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Dao.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            showtotal();
            dao.DaoClose();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //显示所有订单
            InitOrders();
            showtotal();
        }
        //写一个方法让textBox2中显示dataGridView1中所有订单的总金额
        private void showtotal()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
            }
            textBox2.Text = sum.ToString();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
