using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public class BiaoDanInfoDAL
    {

       /// <summary>
       /// 修改 表单信息 
       /// </summary>
       /// <param name="biaodan"></param>
       /// <returns></returns>
       public int EditBiaoDanByModel(Model.BiaoDanInfo biaodan)
       {
           string sql = "update  BiaoDanInfo set BookType=@BookType,BookName=@BookName,Count=@Count,Price=@Price,Publisher=@Publisher,PiCi=@PiCi,DateTime=@DateTime,Foalt=@Foalt where Id=" + biaodan.Id;
           SqlParameter[] pars = { 
                             new  SqlParameter("@BookType",SqlDbType.NVarChar,50),
                               new  SqlParameter("@BookName",SqlDbType.NVarChar,50),
                                 new  SqlParameter("@Count",SqlDbType.Int,10),
                                   new  SqlParameter("@Price",SqlDbType.Int,50),
                                     new  SqlParameter("@Publisher",SqlDbType.NVarChar,50),
                                       new  SqlParameter("@PiCi",SqlDbType.NVarChar,50),
                                         new  SqlParameter("@DateTime",SqlDbType.DateTime,50),
                                           new  SqlParameter("@Foalt",SqlDbType.Int,50)
                                 };
           pars[0].Value = biaodan.BookType;
           pars[1].Value = biaodan.BookName;
           pars[2].Value = biaodan.Count;
           pars[3].Value = biaodan.Price;
           pars[4].Value = biaodan.Publisher;
           pars[5].Value = biaodan.PiCi;
           pars[6].Value = biaodan.DateTime;
           pars[7].Value = biaodan.Foalt;
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, pars);
       }


       /// <summary>
       /// 通过id删除 表单信息
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int DeleteModelById(int id)
       {
           string sql = "delete from BiaoDanInfo where Id=@id";
           SqlParameter[] pars = { 
                           new SqlParameter("@id",id)
                                };
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, pars);
       }
      

       /// <summary>
       /// 通过条件查询对象集
       /// </summary>
       /// <param name="condition"></param>
       /// <returns></returns>
       public IList<Model.BiaoDanInfo> GetListsByCondition(string condition)
       {
           string sql = "select * from BiaoDanInfo where "+condition;
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text);
           IList<Model.BiaoDanInfo> lists = new List<Model.BiaoDanInfo>();
           Model.BiaoDanInfo biaodan = null;
           if (da.Rows.Count > 0)
           {
               foreach (DataRow row in da.Rows)
               {
                   biaodan = new Model.BiaoDanInfo();
                   LoadEdity(biaodan, row);
                   lists.Add(biaodan);
               }
           }
           return lists;
       }

       /// <summary>
       /// 得到总页数
       /// </summary>
       /// <returns></returns>
       public int GetPageCount()
       {
           string sql = "select COUNT(*) from BiaoDanInfo where Foalt=1";
           return Convert.ToInt32(SqlHerple.GetSingle(sql));
       }

       /// <summary>
       /// 通过条件得到总页数
       /// </summary>
       /// <param name="condition"></param>
       /// <returns></returns>
       public int GetPageCountByCondition(string condition)
       {
           string sql = "select COUNT(*) from BiaoDanInfo where "+condition;
           return Convert.ToInt32(SqlHerple.GetSingle(sql));
       }


       /// <summary>
       /// 分页
       /// </summary>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public IList<Model.BiaoDanInfo> GetPageLists(int start, int end)
       {
           string sql = "select top 10 * from (select num=ROW_NUMBER() over (order by Id),* from BiaoDanInfo where Foalt=1) as b where b.num>=@start and b.num<=@end";
           SqlParameter[] pars = { 
                             new  SqlParameter("@start",start),
                             new SqlParameter("@end",end)
                                 };
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text, pars);
           IList<Model.BiaoDanInfo> lists = new List<Model.BiaoDanInfo>();
           Model.BiaoDanInfo biaodan = null;
           if (da.Rows.Count > 0)
           {
               foreach (DataRow row in da.Rows)
               {
                   biaodan = new Model.BiaoDanInfo();
                   LoadEdity(biaodan, row);
                   lists.Add(biaodan);
               }
           }
           return lists;
       }

       /// <summary>
       /// 通过条件得到相应的分页
       /// </summary>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <param name="foalt"></param>
       /// <returns></returns>
       public IList<Model.BiaoDanInfo> GetPagteListsBycondition(int start, int end, string condition)
       {
           string sql = "select top 10 * from (select num=ROW_NUMBER() over (order by Id),* from BiaoDanInfo where "+condition+") as b where b.num>=@start and b.num<=@end";
           SqlParameter[] pars = { 
                             new SqlParameter("@start",start),
                             new SqlParameter("@end",end)
                                 };
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text, pars);
           IList<Model.BiaoDanInfo> lists = new List<Model.BiaoDanInfo>();
           Model.BiaoDanInfo biaodan = null;
           if (da.Rows.Count > 0)
           {
               foreach (DataRow row in da.Rows)
               {
                   biaodan = new Model.BiaoDanInfo();
                   LoadEdity(biaodan, row);
                   lists.Add(biaodan);
               }
           }
           return lists;

       }



       /// <summary>
       /// 查询所有的表单
       /// </summary>
       /// <returns></returns>
       public IList<Model.BiaoDanInfo> GetLists()
       {
           string sql = "select * from BiaoDanInfo";
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text);
           IList<Model.BiaoDanInfo> lists = new List<Model.BiaoDanInfo>();
           Model.BiaoDanInfo biaodan = null;
           if (da.Rows.Count > 0)
           {
               foreach (DataRow row in da.Rows)
               {
                   biaodan = new Model.BiaoDanInfo();
                   LoadEdity(biaodan, row);
                   lists.Add(biaodan);
               }
           }
           return lists;
       }

       private void LoadEdity(Model.BiaoDanInfo biaodan, DataRow row)
       {
           biaodan.Id = Convert.ToInt32(row["Id"]);
           biaodan.BookType = row["BookType"].ToString();
           biaodan.BookName = row["BookName"].ToString();
           biaodan.Count = Convert.ToInt32(row["Count"]);
           biaodan.Price = Convert.ToInt32(row["Price"]);
           biaodan.Publisher = row["Publisher"].ToString();
           biaodan.PiCi = row["PiCi"].ToString();
           biaodan.DateTime = Convert.ToDateTime(row["DateTime"]);
           biaodan.Foalt = Convert.ToInt32(row["Foalt"]);

       }


       /// <summary>
       /// 添加表单
       /// </summary>
       /// <param name="biaodan"></param>
       /// <returns></returns>
       public int AddBiaoDanInfo(Model.BiaoDanInfo biaodan)
       {
           string sql = "insert into BiaoDanInfo (BookType,BookName,Count,Price,Publisher,PiCi,DateTime,Foalt) values (@BookType,@BookName,@Count,@Price,@Publisher,@PiCi,@DateTime,@Foalt)";
           SqlParameter[] pars = { 
                             new  SqlParameter("@BookType",SqlDbType.NVarChar,50),
                               new  SqlParameter("@BookName",SqlDbType.NVarChar,50),
                                 new  SqlParameter("@Count",SqlDbType.Int,10),
                                   new  SqlParameter("@Price",SqlDbType.Int,50),
                                     new  SqlParameter("@Publisher",SqlDbType.NVarChar,50),
                                       new  SqlParameter("@PiCi",SqlDbType.NVarChar,50),
                                         new  SqlParameter("@DateTime",SqlDbType.DateTime,50),
                                           new  SqlParameter("@Foalt",SqlDbType.Int,50)
                                 };
           pars[0].Value = biaodan.BookType;
           pars[1].Value = biaodan.BookName;
           pars[2].Value = biaodan.Count;
           pars[3].Value = biaodan.Price;
           pars[4].Value = biaodan.Publisher;
           pars[5].Value = biaodan.PiCi;
           pars[6].Value = biaodan.DateTime;
           pars[7].Value = biaodan.Foalt;
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, pars);


       }
    }
}
