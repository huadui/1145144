using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1145144
{
    public partial class admin1 : Form
    {
        private Product product;
        private Sellers Sellers;
        private Category Category;
        private Bills Bills;
        private Procurement Procurement;
        public admin1()
        {
            InitializeComponent();
            product = new Product();
            Sellers = new Sellers();
            Category = new Category();
            Bills = new Bills();
            Procurement = new Procurement();
        }
        private void panellogo_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void iconButtonCurrent_Click(object sender, EventArgs e)
        {
            //清除Panel2中的内容
            panel2.Controls.Clear();
            //把pictureBox3放入Panel2中
            pictureBox3.Dock = DockStyle.Fill;
            panel2.Controls.Add(pictureBox3);


        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            //将Product类中的内容显示在panel2中,并且填充
            product.TopLevel = false;
            panel2.Controls.Add(product);
            product.Show();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            //将Sellers类中的内容显示在panel2中,并且填充
            Sellers.TopLevel = false;
            panel2.Controls.Add(Sellers);
            Sellers.Show();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            //将Category类中的内容显示在panel2中,并且填充
            Category.TopLevel = false;
            panel2.Controls.Add(Category);
            Category.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            //将Bill类中的内容显示在panel2中,并且填充
            panel2.Controls.Clear();
            Bills.TopLevel = false;
            panel2.Controls.Add(Bills);
            Bills.Show();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            //退出
            loginform login = new loginform();
            this.Hide();
            login.Show();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            //将Procurement类中的内容显示在panel2中,并且填充
            panel2.Controls.Clear();    
            Procurement.TopLevel = false;
            panel2.Controls.Add(Procurement);
            Procurement.Show();
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
