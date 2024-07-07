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
    public partial class loginform : Form
    {
        public loginform()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && textBox2.Text!="") 
            {
                Login();
            }
            else
            {
                MessageBox.Show("输入有空项，请重新输入");
            }
        }
        //先判断是用户还是管理员，再判断登录验证，允许返回真，不允许返回假
        public void Login()
        {
            //用户
            if(radioButton1.Checked==true)
            {
                string sql = "select * from Users where ID='" + textBox1.Text + "' and Psw='" + textBox2.Text + "'";
                Dao dao = new Dao();
                IDataReader dc = dao.GetReader(sql);
                if (dc.Read())
                {
                    Data.UID = dc["ID"].ToString();
                    Data.UNAME = dc["NAME"].ToString();
                    
                    //窗体的跳转
                    user1 user = new user1();
                    this.Hide();
                    user.ShowDialog();
                }
                else
                {
                    MessageBox.Show("登录失败");         
                }
                dao.DaoClose();
            }
            //管理员
            if(radioButton2.Checked==true) 
            {
                string sql = "select * from t_admin where uid='" + textBox1.Text + "' and psw='" + textBox2.Text+ "'";
                Dao dao = new Dao();
                IDataReader dc = dao.GetReader(sql);
                if (dc.Read())
                {

                    
                   admin1 admin = new admin1();
                    this.Hide(); 
                    admin.ShowDialog();
                }
                else
                {
                    MessageBox.Show("登录失败");
                    
                }
                dao.DaoClose();
            }
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //跳转到注册窗口
            enroll enroll = new enroll();
            this.Hide();
            enroll.ShowDialog();

        }
    }
}
