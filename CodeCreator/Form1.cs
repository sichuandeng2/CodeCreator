using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable dts = null;

        private DataTable Sqlcon(string sql)
        {
            string constr = txtconstr.Text.Trim();

            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(sql, con)) 
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter adp = new SqlDataAdapter(com);
                    adp.Fill(ds);

                    return ds.Tables[0];

                }
            }
        }

        private void btncon_Click(object sender, EventArgs e)
        {
           DataTable dt=dts = Sqlcon(@"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ");
            DataRow r = dt.NewRow();
            r["TABLE_NAME"] = "全选";
            dt.Rows.InsertAt(r, 0);
            

            cmTable.DataSource = dt;
            cmTable.DisplayMember = "TABLE_NAME";
            cmTable.ValueMember = "TABLE_NAME";

        }

        private string CreateModel(DataTable dt,String  tableName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;"); 
            sb.AppendLine();
            sb.AppendLine($"namespace {txtnamespace.Text.Trim()}.Model//实体层"); 
            sb.AppendLine("{"); 
            sb.AppendLine($"        public class {tableName}");
            sb.AppendLine("         {"); 
            foreach (DataColumn  col in dt.Columns)
            {
                sb.Append($"             public {GetDtType(col)} {col.ColumnName}").AppendLine(" { get; set; }"); 
            } 
            sb.AppendLine("         }"); 
            sb.AppendLine("}");
            txtModel.AppendText(sb.ToString());
            return sb.ToString();

        }

        private string CreateDAL(DataTable dt,string tableName)
        {
            string[] col = Getcols(dt);
            string[] colNotAutoKey = Getcols(dt).Where(m => m != txtAuotoKey.Text.Trim()).ToArray();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Configuration;"); 
            sb.AppendLine($"using {txtnamespace.Text.Trim()}.Model;"); 
            sb.AppendLine("using System.Data.SqlClient;"); 
            sb.AppendLine("using System.Data;"); 
            sb.AppendLine();
            sb.AppendLine($"namespace {txtnamespace.Text.Trim()}.DAL//数据访问层"); 
            sb.AppendLine("{"); 
            sb.AppendLine($"        public class {tableName}DAL//{tableName}对象类");
            sb.AppendLine("        {");
            sb.AppendLine("             SQLHelper sqlHelper = new SQLHelper(ConfigurationManager.ConnectionStrings[\"constr\"].ToString());");
            
            sb.AppendLine("             /// <summary>");
            sb.AppendLine("             /// 构建实例");
            sb.AppendLine("             /// </summary>");
            sb.AppendLine("             /// <param name=\"obj\">obj对象</param>");
            sb.AppendLine($"             public void InsertCode({tableName} obj)"); 
            sb.AppendLine("             {"); 
            sb.AppendLine($"                sqlHelper.ExecuteNonQuery(@\"INSERT INTO {tableName} {string.Join(",", colNotAutoKey.Select(m=> "["+m+"]"))} VALUES ({string.Join(",", colNotAutoKey.Select(m=>"@["+m+"]"))}\""); 
            for (int i = 0; i < colNotAutoKey.Length; i++)
            {
                sb.AppendLine($"                 ,new SqlParameter(\"@[{colNotAutoKey[i]}]\", obj.{colNotAutoKey[i]})"); 
            }
            sb.AppendLine("             );}");
            sb.AppendLine();
            sb.AppendLine("             /// <summary>");
            sb.AppendLine("             /// 构建实例并返回id");
            sb.AppendLine("             /// </summary>");
            sb.AppendLine($"             public int InsertCodeById({tableName} obj)");
            sb.AppendLine("             {");
            sb.AppendLine($"                return sqlHelper.ExecuteNonQuery(@\"INSERT INTO {tableName} {string.Join(",", colNotAutoKey.Select(m => "[" + m + "]"))} output Users.id VALUES ({string.Join(",", colNotAutoKey.Select(m => "@[" + m + "]"))}\"");
                for (int i = 0; i < colNotAutoKey.Length; i++)
                {
                    sb.AppendLine($"                 ,new SqlParameter(\"@[{colNotAutoKey[i]}]\", obj.{colNotAutoKey[i]})");
                }
            sb.AppendLine("                 );");
            sb.AppendLine("              }");
            sb.AppendLine();

            sb.AppendLine("             /// <summary>");
            sb.AppendLine("             /// 删除所有数据");
            sb.AppendLine("             /// </summary>");
            sb.AppendLine("             public void DeleteRecord()");
            sb.AppendLine("             {");
            sb.AppendLine($"                 sqlHelper.ExecuteNonQuery(\"DELETE FROM {tableName}\");");
            sb.AppendLine("             }");
            sb.AppendLine();

            sb.AppendLine("             /// <summary>");
            sb.AppendLine("             /// 删除指定id数据");
            sb.AppendLine("             /// </summary>");
            sb.AppendLine("             ///// <param name=\"id\"></param>");

            sb.AppendLine("             public void DeleteRecordById(int id)");
            sb.AppendLine("             {");
            sb.AppendLine($"                 sqlHelper.ExecuteNonQuery(\"DELETE FROM {tableName} WHERE id=@id\",");
            sb.AppendLine("                     new SqlParameter(\"@id\", id));");
            sb.AppendLine("             }");
            sb.AppendLine();

            sb.AppendLine("             /// <summary>");
            sb.AppendLine("             /// 数据更新");
            sb.AppendLine("             /// </summary>");
            sb.AppendLine("             ///// <param name=\"obj\">对象数据</param>");
            sb.AppendLine($"             public void UpDateRecord({tableName} obj)");
            sb.AppendLine("             {");
            sb.AppendLine($"                 sqlHelper.ExecuteNonQuery(@\"UPDATE {tableName} SET {string.Join("," ,colNotAutoKey.Select(m=>"["+m+"]=@"+m))} WHERE id = @id\"");
                for (int item = 0; item < col.Length; item++)
                {
                    sb.AppendLine($"                 ,new SqlParameter(\"@[{col[item]}]\", obj.{col[item]})");

                }
            sb.AppendLine("                 );");
            sb.AppendLine("             }");
            sb.AppendLine();

            sb.AppendLine("             /// <summary>");
            sb.AppendLine("             /// 获取List数据集合");
            sb.AppendLine("             /// </summary>");
            sb.AppendLine("             ///// <returns></returns>");
            sb.AppendLine($"             public IEnumerable<{tableName}> ListAll()");
            sb.AppendLine("             {");
            sb.AppendLine($"                 List<{tableName}> {tableName}objs = new List<{tableName}>();");
            sb.AppendLine($"                 DataTable dt = sqlHelper.ExecuteTable(@\"SELECT * FROM {tableName}\");");
            sb.AppendLine("                 foreach (DataRow r in dt.Rows)");
            sb.AppendLine("                 {");
            sb.AppendLine($"                     {tableName}objs.Add(ToModel(r));");
            sb.AppendLine("                 }");
            sb.AppendLine($"                 return {tableName}objs;");
            sb.AppendLine("             }");
            sb.AppendLine();

            sb.AppendLine("             /// <summary>");
            sb.AppendLine("             /// 获取指定id的数据");
            sb.AppendLine("             /// </summary>");
            sb.AppendLine("             ///// <param name=\"id\"></param>");
            sb.AppendLine("             ///// <returns></returns>");

            sb.AppendLine($"             public IEnumerable<{tableName}> ListById(int id)");
            sb.AppendLine("             {");
            sb.AppendLine($"                 List<{tableName}> {tableName}objs = new List<{tableName}>();");
            sb.AppendLine($"                 DataTable dt = sqlHelper.ExecuteTable(@\"SELECT * FROM {tableName} WHERE id=@id\",");
            sb.AppendLine($"                 new SqlParameter(\"@id\", id));");
            sb.AppendLine("                 foreach (DataRow r in dt.Rows)");
            sb.AppendLine("                 {");
            sb.AppendLine($"                     {tableName}objs.Add(ToModel(r));");
            sb.AppendLine("                 }");
            sb.AppendLine($"                 return {tableName}objs;");
            sb.AppendLine("             }");
            sb.AppendLine();

            sb.AppendLine("             /// <summary>");
            sb.AppendLine("             /// 获取分页数据");
            sb.AppendLine("             /// </summary>");
            sb.AppendLine("             ///// <param name=\"startNum\"></param>");
            sb.AppendLine("             ///// <param name=\"endNnm\"></param>");
            sb.AppendLine("             ///// <returns></returns>");
            sb.AppendLine($"             public IEnumerable<{tableName}> ListOfAllPage(int? startNum, int? endNnm)");
            sb.AppendLine("             {");
            sb.AppendLine($"                 List<{tableName}> {tableName}objs = new List<{tableName}>();");
            sb.AppendLine($"                 DataTable dt = sqlHelper.ExecuteTable(@\"SELECT * FROM(SELECT *, ROW_NUMBER() OVER(ORDER BY id) AS num FROM Users) AS PageTable WHERE PageTable.num BETWEEN @startNum AND @endNum\",");
            sb.AppendLine($"                    new SqlParameter(\"@startNum\", startNum),");
            sb.AppendLine($"                    new SqlParameter(\"@endNum\", endNnm));");
            sb.AppendLine($"                 foreach (DataRow r in dt.Rows)");
            sb.AppendLine("                 {");
            sb.AppendLine($"                     {tableName}objs.Add(ToModel(r));");
            sb.AppendLine("                 }");
            sb.AppendLine($"                 return {tableName}objs;");
            sb.AppendLine("             }");
            sb.AppendLine();

            sb.AppendLine("             /// <summary>");
            sb.AppendLine("             /// 将表格类型转换为对象类型");
            sb.AppendLine("             /// </summary>");
            sb.AppendLine("             ///// <param name=\"r\"></param>");
            sb.AppendLine("             ///// <returns></returns>");
            sb.AppendLine($"             public {tableName} ToModel(DataRow r)");
            sb.AppendLine("             {");
            sb.AppendLine($"                 {tableName} {tableName}obj = new {tableName}();");
            for (int i = 0; i < col.Length; i++)
            {
                sb.AppendLine($"                 {tableName}obj.{col[i]} = ({GetDtType(dt.Columns[i])})r[\"{col[i]}\"];");

            }
            sb.AppendLine($"                 return {tableName}obj;");
            sb.AppendLine("             }");
            sb.AppendLine("        }");
            sb.AppendLine("}");
            txtDAL.AppendText(sb.ToString());
            return sb.ToString();
        }

        private string CreateBLL(string tableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine($"using {txtnamespace.Text.Trim()}.Model;");
            sb.AppendLine($"using {txtnamespace.Text.Trim()}.DAL;");
            sb.AppendLine();
            sb.AppendLine($"namespace {txtnamespace.Text.Trim()}.BLL//数据访问层");
            sb.AppendLine("{");
            sb.AppendLine($"        public class {tableName}BLL//{tableName}对象类");
            sb.AppendLine("        {");
            sb.AppendLine($"            {tableName}DAL InstanceDAL = new {tableName}DAL();");
            sb.AppendLine();

            sb.AppendLine($"            ///// <summary>");
            sb.AppendLine($"            ///// 构建实例");
            sb.AppendLine($"            ///// </summary>");
            sb.AppendLine($"            ///// <param name=\"obj\"></param>");
            sb.AppendLine($"            public void InsertCode({tableName} obj)");
            sb.AppendLine("            {");
            sb.AppendLine("                InstanceDAL.InsertCode(obj);");
            sb.AppendLine("            }");
            sb.AppendLine();

            sb.AppendLine($"            ///// <summary>");
            sb.AppendLine($"            ///// 创建实例对象并返回索引");
            sb.AppendLine($"            ///// </summary>");
            sb.AppendLine($"            ///// <param name=\"obj\"></param>");
            sb.AppendLine($"            public int InsertCodeById({tableName} obj)");
            sb.AppendLine("            {");
            sb.AppendLine("                return InstanceDAL.InsertCodeById(obj);");
            sb.AppendLine("            }");
            sb.AppendLine();

            sb.AppendLine($"            ///// <summary>");
            sb.AppendLine($"            ///// 删除所以数据");
            sb.AppendLine($"            ///// </summary>");
            sb.AppendLine($"            public void DeletInsertCode()");
            sb.AppendLine("            {");
            sb.AppendLine("                InstanceDAL.DeleteRecord();");
            sb.AppendLine("            }");
            sb.AppendLine();

            sb.AppendLine($"            ///// <summary>");
            sb.AppendLine($"            ///// 删除指定id数据");
            sb.AppendLine($"            ///// </summary>");
            sb.AppendLine($"            ///// <param name=\"id\"></param>");
            sb.AppendLine($"            public void DeletInsertCodeById(int id)");
            sb.AppendLine("            {");
            sb.AppendLine("                InstanceDAL.DeleteRecordById(id);");
            sb.AppendLine("            }");
            sb.AppendLine();

            sb.AppendLine("            ///// <summary>");
            sb.AppendLine("            ///// 更新数据集");
            sb.AppendLine("            ///// </summary>");
            sb.AppendLine("            ///// <param name=\"obj\"></param>");
            sb.AppendLine($"            public void UpDateRecord({tableName} obj)");
            sb.AppendLine("            {");
            sb.AppendLine("                InstanceDAL.UpDateRecord(obj);");
            sb.AppendLine("            }");
            sb.AppendLine();

            sb.AppendLine("            ///// <summary>");
            sb.AppendLine("            ///// 返回所有数据");
            sb.AppendLine("            ///// </summary>");
            sb.AppendLine("            ///// <returns></returns>");
            sb.AppendLine($"            public IEnumerable<{tableName}> LisetALL()");
            sb.AppendLine("            {");
            sb.AppendLine("                return InstanceDAL.ListAll();");
            sb.AppendLine("            }");
            sb.AppendLine();

            sb.AppendLine("            ///// <summary>");
            sb.AppendLine("            ///// 返回指定id的数据");
            sb.AppendLine("            ///// </summary>");
            sb.AppendLine("            ///// <param name=\"id\"></param>");
            sb.AppendLine("            ///// <returns></returns>");
            sb.AppendLine($"            public IEnumerable<{tableName}> LisetALL(int id)");
            sb.AppendLine("            {");
            sb.AppendLine("                return InstanceDAL.ListById(id);");
            sb.AppendLine("            }");
            sb.AppendLine();

            sb.AppendLine("            ///// <summary>");
            sb.AppendLine("            ///// 返回指定页的数据集");
            sb.AppendLine("            ///// </summary>");
            sb.AppendLine("            ///// <param name=\"pageIndex\"></param>");
            sb.AppendLine("            ///// <param name=\"pageSize\"></param>");
            sb.AppendLine("            ///// <returns></returns>");
            sb.AppendLine($"            public IEnumerable<{tableName}> LisetALL(int pageIndex, int pageSize)");
            sb.AppendLine("            {");
            sb.AppendLine("                int startNum = pageIndex * pageSize - pageSize;");
            sb.AppendLine("                int endNum = pageIndex * pageSize;");
            sb.AppendLine("                return InstanceDAL.ListOfAllPage(startNum, endNum);");
            sb.AppendLine("            }");
            sb.AppendLine();

            sb.AppendLine("        }");

            sb.AppendLine("}");

            txtBLL.AppendText(sb.ToString());
            return sb.ToString();
        }

        private string GetDtType(DataColumn col)
        {
            if (col.AllowDBNull && col.DataType.IsValueType)
            {
                return col.DataType + "?";
            }
            else
            {
                return col.DataType.ToString();
            }
        }

        private string[] Getcols(DataTable dt)
        {
            List<string> cols = new List<string>();
            foreach (DataColumn item in dt.Columns)
            {
                cols.Add(item.ColumnName);
            }
            return cols.ToArray();

        }

        private void txtPreview_Click(object sender, EventArgs e)
        {
            CreateCode((dt, name) =>
            {
                CreateModel(dt, name);
                CreateDAL(dt, name);
                CreateBLL(name);

            });
        }

        private void btnCreateCode_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            string path = AppDomain.CurrentDomain.BaseDirectory + "SolutionPath.txt";
            if (File.Exists(path))
            {
                fbd.SelectedPath = File.ReadAllText(path);
            }
            else
            {
                File.Create(path).Close();
            }
            DialogResult  dialogresult=  fbd.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                File.WriteAllText(path, fbd.SelectedPath);
            }
            CreateCode((dt, name) =>
            {
                try
                {
                    File.WriteAllText($@"{fbd.SelectedPath}\{txtnamespace.Text.Trim()}.Model\{name}Model.cs", CreateModel(dt, name));
                    File.WriteAllText($@"{fbd.SelectedPath}\{txtnamespace.Text.Trim()}.DAL\{name}DAL.cs", CreateDAL(dt, name));
                    File.WriteAllText($@"{fbd.SelectedPath}\{txtnamespace.Text.Trim()}.BLL\{name}BLL.cs", CreateBLL(name));
                }
                catch (Exception)
                {
                    MessageBox.Show("文件路径有误");
                }
            }); 
        }

        private void CreateCode(Action<DataTable,string> CreateAction)
        {
            txtModel.Text = "";
            txtDAL.Text = "";
            txtBLL.Text = "";
            string lab = cmTable.SelectedValue.ToString();
            if (lab != "全选")
            {
                dts = Sqlcon($"select * from {lab}");
                CreateAction(dts, lab);
                //CreateDAL(dts, lab);
                //CreateBLL(lab);
            }
            else
            {
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    DataRow dr = dts.Rows[i];
                    if (dr["TABLE_NAME"].ToString() != "全选" && dr["TABLE_NAME"].ToString() != "sysdiagrams")
                    {
                        DataTable dtss = Sqlcon($"select * from {dr["TABLE_NAME"].ToString()}");
                        CreateAction(dts, dr["TABLE_NAME"].ToString());
                    }
                    
                }
            }

        }

    }












}
