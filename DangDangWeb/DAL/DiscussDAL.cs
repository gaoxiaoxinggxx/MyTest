using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
   public class DiscussDAL
    {

       Page<Model.Discuss> page = new Page<Model.Discuss>();
       

       /// <summary>
       /// 分页
       /// </summary>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public IList<Model.Discuss> GetPageCount( int start, int end)
       {
           string sql = "select  *  from (select ROW_NUMBER() over (order by Bid) as num,* from discuss) as b  where b.num>@start and b.num<@end";
           SqlParameter[] pars = { 
                             new  SqlParameter("@start",start),
                             new  SqlParameter("@end",end)
                                 };
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text, pars);
           return TableToLists(da);
       }

       /// <summary>
       /// 得到表格中所有的的数据条数
       /// </summary>
       /// <param name="tablename"></param>
       /// <returns></returns>
       public int GetAllRowCount()
       {
           return   page.GetAllRowCount("Discuss");
       }
       

       /// <summary>
       /// 修改评论信息
       /// </summary>
       /// <param name="modeldis"></param>
       /// <returns></returns>
       public int EdityDiscuss(Model.Discuss modeldis)
       {
           string sql = "update discuss set Bid=@Bid,Discusses=@Discusses,JycCount=@JycCount,JgcCount=@JgcCount,DisName=@DisName,Date=@Date,HfDiscussId=@HfDiscussId,Float=@Float where Did="+modeldis.Did;
           SqlParameter[] pars = { 
                           new  SqlParameter("@Bid",SqlDbType.Int,10),
                           new  SqlParameter("@Discusses",SqlDbType.NVarChar,500),
                           new  SqlParameter("@JycCount",SqlDbType.Int,10),
                           new  SqlParameter("@JgcCount",SqlDbType.Int,10),
                           new  SqlParameter("@DisName",SqlDbType.NVarChar,50),
                           new  SqlParameter("@Date",SqlDbType.DateTime,50),
                           new  SqlParameter("@HfDiscussId",SqlDbType.NVarChar,50),
                           new  SqlParameter("@Float",SqlDbType.Int,10)
                                 };
           pars[0].Value = modeldis.Bid;
           pars[1].Value = modeldis.Discusses;
           pars[2].Value = modeldis.JycCount;
           pars[3].Value = modeldis.JgcCount;
           pars[4].Value = modeldis.DisName;
           pars[5].Value = modeldis.Date;
           pars[6].Value = modeldis.HfDiscussId;
           pars[7].Value = modeldis.Float;
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, pars);

       }



       /// <summary>
       /// 通过id删除对象
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int DeleteDiscussById(int id)
       {
           string sql = "delete discuss where Bid=@id";
           SqlParameter[] pars = { 
                       new SqlParameter ("@id",id)
                                 };
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, pars);
       }


       /// <summary>
       /// 通过条件得到对象集
       /// </summary>
       /// <param name="condition"></param>
       /// <returns></returns>
       public IList<Model.Discuss> GetListsByCondition(string condition)
       {
           string sql = "select * from  Discuss where "+condition;
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text);
           return TableToLists(da);
       }
       /// <summary>
       /// 将表格生成对象集
       /// </summary>
       /// <param name="da"></param>
       /// <returns></returns>
       public IList<Model.Discuss> TableToLists(DataTable da)
       {
           IList<Model.Discuss> lists = new List<Model.Discuss>();
           if (da.Rows.Count > 0)
           {
               foreach (DataRow row in da.Rows)
               {
                   lists.Add(DataRowToModel(row));
               }
           }
           return lists;
       }

       /// <summary>
       /// 将行变成对象
       /// </summary>
       /// <param name="row"></param>
       /// <returns></returns>
       private Model.Discuss DataRowToModel(DataRow row)
       {
           DAL.BookInfoDAL bookinfodal=new BookInfoDAL ();
           Model.Discuss dis = new Model.Discuss();
           if (row != null)
           {
               if (row["Did"] != null && row["Did"].ToString() != "")
               {
                   dis.Did = int.Parse(row["Did"].ToString());
               }
               if (row["Bid"] != null && row["Bid"].ToString() != "")
               {
                   dis.Bid = int.Parse(row["Bid"].ToString());
               }
               if (row["Discusses"] != null && row["Discusses"].ToString() != "")
               {
                   dis.Discusses = row["Discusses"].ToString();
               }
               if (row["JycCount"] != null && row["JycCount"].ToString() != "")
               {
                   dis.JycCount = int.Parse(row["JycCount"].ToString());
               }
               if (row["JgcCount"] != null && row["JgcCount"].ToString() != "")
               {
                   dis.JgcCount = int.Parse(row["JgcCount"].ToString());
               }
               if (row["DisName"] != null && row["DisName"].ToString() != "")
               {
                   dis.DisName = row["DisName"].ToString();
               }
               if (row["Date"] != null && row["Date"].ToString() != "")
               {
                   dis.Date = Convert.ToDateTime(row["Date"].ToString());
               }
               if (row["HfDiscussId"] != null && row["HfDiscussId"].ToString() != "")
               {
                   dis.HfDiscussId =row["HfDiscussId"].ToString();
               }
               if (row["Float"] != null && row["Float"].ToString() != "")
               {
                   dis.Float = int.Parse(row["Float"].ToString());
               }
               dis.BookInfo = bookinfodal.GetBookInfosByCondition("Bid=" + row["Bid"].ToString())[0];
               
           }
           return dis;
       }



    }
}
