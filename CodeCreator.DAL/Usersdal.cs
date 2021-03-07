using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CodeCreator.Model;
using System.Data.SqlClient;
using System.Data;

namespace CodeCreator.DAL//数据访问层
{
    public class UsersDAL
    {
        //配置文件提取了解字符串
        static string constr = ConfigurationManager.ConnectionStrings["constr"].ToString();
        SQLHelper sqlHelper = new SQLHelper(constr);
        /// <summary>
        /// 新增内容
        /// </summary>
        public void InsertCode(Users obj) 
        {
            sqlHelper.ExecuteNonQuery(@"INSERT INTO [T_Users] ([user],[name],[pwd],[newdate],[lastdate],[lockuser],[admin],[order],[delete]) VALUES" +
                "(@[user],@[name],@[pwd],@[newdate],@[lastdate],@[lockuser],@[admin],@[order],@[delete])",
                new SqlParameter("@[user]", obj.user),
                new SqlParameter("@[name]", obj.name),
                new SqlParameter("@[pwd]", obj.pwd),
                new SqlParameter("@[newdate]", obj.newdate),
                new SqlParameter("@[lastdate]", obj.lastdate),
                new SqlParameter("@[lockuser]", obj.lockuser),
                new SqlParameter("@[admin]", obj.admin),
                new SqlParameter("@[order]", obj.order),
                new SqlParameter("@[delete]", obj.delete)
                );
        }
        /// <param name="obj"></param>
        /// <summary>
        /// 新增内容并返回新增id
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertCodeById(Users obj)
        {
           return sqlHelper.ExecuteNonQuery(@"INSERT INTO [T_Users] ([user],[name],[pwd],[newdate],[lastdate],[lockuser],[admin],[order],[delete]) output Users.id VALUES" +
                "(@[user],@[name],@[pwd],@[newdate],@[lastdate],@[lockuser],@[admin],@[order],@[delete])",
                new SqlParameter("@[user]", obj.user),
                new SqlParameter("@[name]", obj.name),
                new SqlParameter("@[pwd]", obj.pwd),
                new SqlParameter("@[newdate]", obj.newdate),
                new SqlParameter("@[lastdate]", obj.lastdate),
                new SqlParameter("@[lockuser]", obj.lockuser),
                new SqlParameter("@[admin]", obj.admin),
                new SqlParameter("@[order]", obj.order),
                new SqlParameter("@[delete]", obj.delete)
                );
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        public void DeleteRecord()
        {
            sqlHelper.ExecuteNonQuery("DELETE FROM Users");
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteRecordById(int id)
        {
            sqlHelper.ExecuteNonQuery(@"DELETE FROM T_Users WHERE id=@id",
                new SqlParameter("@id", id));
        }
        /// <summary>
        /// 数据更新
        /// </summary>
        /// <param name="obj"></param>
        public void UpDateRecord(Users obj)
        {
            sqlHelper.ExecuteNonQuery(@"UPDATE T_Users SET [user]=@user,[name]=@name,[pwd]=@pwd,newdate=@newdate,[lastdate]=@lastdate,[lockuser]=@lockuser,[admin]=@admin,[order]=@order,[delete]=@delete WHERE id=@id",
                new SqlParameter("@[user]", obj.user),
                new SqlParameter("@[name]", obj.name),
                new SqlParameter("@[pwd]", obj.pwd),
                new SqlParameter("@[newdate]", obj.newdate),
                new SqlParameter("@[lastdate]", obj.lastdate),
                new SqlParameter("@[lockuser]", obj.lockuser),
                new SqlParameter("@[admin]", obj.admin),
                new SqlParameter("@[order]", obj.order),
                new SqlParameter("@[delete]", obj.delete),
                new SqlParameter("@id", obj.id)
                ) ;
        }
        /// <summary>
        /// 获取List数据集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Users> ListAll()
        {
            List<Users> us = new List<Users>();
            DataTable dt = sqlHelper.ExecuteTable(@"SELECT * FROM T_Users");

            foreach (DataRow r  in dt.Rows)
            {
                us.Add(ToModel(r));
            }
            return us;
        }
        /// <summary>
        /// 获取指定id的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Users> ListById(int id)
        {
            List<Users> users = new List<Users>();
            DataTable td= sqlHelper.ExecuteTable("SELECT FROM Users WHERE id=@id",
                new SqlParameter("@id", id));
            foreach (DataRow r in td.Rows)
            {
                users.Add(ToModel(r));
            }

            return users;
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="startNum"></param>
        /// <param name="endNnm"></param>
        /// <returns></returns>
        public IEnumerable<Users> ListOfAllPage(int? startNum,int? endNnm) 
        {
            List<Users> users = new List<Users>();
            DataTable dt= sqlHelper.ExecuteTable(@"SELECT * FROM (SELECT *,ROW_NUMBER() OVER(ORDER BY id) AS num FROM Users) AS PageTable WHERE PageTable.num BETWEEN @startNum AND @endNum",
                new SqlParameter("@startNum", startNum),
                new SqlParameter("@endNum", endNnm)
                );
            foreach (DataRow r in dt.Rows)
            {
                users.Add(ToModel(r));
            }
            return users;
        }
        /// <summary>
        /// 将表格类型转换为对象类型
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private Users ToModel(DataRow r)
        {
            Users u = new Users();
            u.id = (Int32)r["id"];
            u.user = r["user"].ToString();
            u.name = r["name"].ToString();
            u.pwd = r["pwd"].ToString();
            u.newdate = (DateTime)r["newdate"];
            u.lockuser = (Boolean)r["lockuser"];
            u.admin = (Int32)r["admin"];
            u.delete = (Boolean)r["delete"];
            u.order = (Int32)r["order"];
            return u;
        }

    }


}


