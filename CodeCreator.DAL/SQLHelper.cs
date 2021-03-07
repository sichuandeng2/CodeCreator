using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
namespace CodeCreator.DAL
{
    public class SQLHelper
    {
        private string constr;
        public SQLHelper(string str) 
        {
            constr = str;
        }
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlpar"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql,params SqlParameter[] sqlpar)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand com = new SqlCommand(sql,con);
                com.Parameters.AddRange(sqlpar);
                return com.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlpar"></param>
        /// <returns></returns>
        public DataTable ExecuteTable(string sql, params SqlParameter[] sqlpar)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddRange(sqlpar);
                DataSet ds = new DataSet();
                SqlDataAdapter sa = new SqlDataAdapter(com);
                sa.Fill(ds);
                return ds.Tables[0];
            }
        }
    }
}
