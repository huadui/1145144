using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1145144
{
    internal class Dao
    {
        //连接到数据库
        public static SqlConnection GetConnection()
        {
            string str = "Data Source=test;Initial Catalog=114514;Integrated Security=True";
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            return conn;//返回数据库连接对象
        }
        //sqlcommand
        public static SqlCommand GetCommand(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            return cmd;
        }
        //传入的SQL语句
        public int Execute(string sql)//更新操作
        {
           return  GetCommand(sql).ExecuteNonQuery();
        }
        //读取操作
        public SqlDataReader GetReader(string sql)
        {
            return GetCommand(sql).ExecuteReader();
        }
        public void DaoClose()
        {
            GetConnection().Close();//关闭数据库链接
        }   

    }
}
