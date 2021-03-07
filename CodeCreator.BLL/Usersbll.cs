using CodeCreator.DAL;
using CodeCreator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCreator.BLL
{
    public class Usersbll
    {
        UsersDAL InstanceDAL = new UsersDAL();
        /// <summary>
        /// 构建实例
        /// </summary>
        /// <param name="obj"></param>
        /// 
        public void InsertCode(Users obj)
        {
            InstanceDAL.InsertCode(obj);
        }
        /// <summary>
        /// 创建实例对象并返回索引
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertCodeById(Users obj) 
        {
            return InstanceDAL.InsertCodeById(obj);
        }
        /// <summary>
        /// 删除所以数据
        /// </summary>
        public void DeletInsertCode()
        {
            InstanceDAL.DeleteRecord();
        }
        /// <summary>
        /// 删除指定id数据
        /// </summary>
        /// <param name="id"></param>
        public void DeletInsertCodeById(int id)
        {
            InstanceDAL.DeleteRecordById(id);
        }
        /// <summary>
        /// 更新数据集
        /// </summary>
        /// <param name="obj"></param>
        public void UpDateRecord(Users obj)
        {
            InstanceDAL.UpDateRecord(obj);
        }
        /// <summary>
        /// 返回所有数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Users> LisetALL()
        {
           return InstanceDAL.ListAll();
        }
        /// <summary>
        /// 返回指定id的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Users> LisetALLById(int id)
        {
            return InstanceDAL.ListById(id);
        }
        /// <summary>
        /// 返回指定页的数据集
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Users> LisetALLByPage(int pageIndex,int pageSize)
        {
            int startNum = pageIndex * pageSize-pageSize;
            int endNum = pageIndex * pageSize;
            return InstanceDAL.ListOfAllPage(startNum,endNum);
        }


    }
}
