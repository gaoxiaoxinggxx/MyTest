using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
   public class BookTypeDAL
    {

       /// <summary>
       /// 分页查询  
       /// </summary>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public IList<Model.BookType> GetPageLists(int start, int end)
       {
           string sql = "select * from (select row_number() over (order by TypeId) as num,* from BookType ) as t where t.num>=@start and t.num <@end ";
           SqlParameter[] pars = { 
                              new SqlParameter("@start",start),
                              new SqlParameter("@end",end)
                                 };
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text, pars);
           return TableToLists(da);
         
       }

       /// <summary>
       /// 得到总信息条数
       /// </summary>
       /// <returns></returns>
       public int GetCountPages()
       {
           string sql = "select count(*) from BookType";
           return Convert.ToInt32( SqlHerple.GetSingle(sql));
       }

       /// <summary>
       /// 修改  类别 
       /// </summary>
       /// <param name="booktype"></param>
       /// <returns></returns>
       public int EdityBookType(Model.BookType booktype)
       {
           string sql = "update BookType set TypeTitle=@TypeTitle,TypeParentId=@TypeParentId Where TypeId="+booktype.TypeId;
           SqlParameter[] pars = { 
                          new SqlParameter("@TypeTitle",SqlDbType.NVarChar,30),
                          new SqlParameter("@TypeParentId",SqlDbType.Int,10)
                                 };
           pars[0].Value = booktype.TypeTitle;
           pars[1].Value = booktype.TypeParentId;
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, pars);
       }


       /// <summary>
       /// 删除 子节点
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int DelectBookType(int id)
       {
           string sql = "delete from BookType Where TypeId=@TypeId";
           SqlParameter[] pars = { 
                        new SqlParameter("@TypeId",id)
                                 };
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, pars);

       }

       /// <summary>
       /// 添加BookType类别
       /// </summary>
       /// <param name="type"></param>
       /// <returns></returns>
       public int AddBookType(Model.BookType type)
       {
           string sql = "insert into BookType (TypeTitle,TypeParentId) values (@TypeTitle,@TypeParentId)";
           SqlParameter[] spar = { 
                             new SqlParameter("@TypeTitle",SqlDbType.NVarChar,50),
                             new SqlParameter("@TypeParentId",SqlDbType.Int,30)
                                 };
           spar[0].Value = type.TypeTitle;
           spar[1].Value = type.TypeParentId;
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, spar);
       }

       /// <summary>
       /// 根据条件得到  对象集
       /// </summary>
       /// <param name="condition"></param>
       /// <returns></returns>
       public IList<Model.BookType> GetFatherBooktypes(string condition)
       {
           string sql = "select * from BookType where "+condition;
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text);
           IList<Model.BookType> lists = new List<Model.BookType>();
           Model.BookType booktype = null;
           if (da.Rows.Count > 0)
           {
               foreach (DataRow row in da.Rows)
               {
                   booktype = new Model.BookType();
                   LoadEdity(booktype, row);
                   lists.Add(booktype);
               }
           }
           return lists;
       }

       /// <summary>
       /// 得到所有的  分类信息
       /// </summary>
       /// <returns></returns>
       public IList<Model.BookType> GetAllBookTypes()
       {
           string sql = "select * from BookType";
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text);

           IList<Model.BookType> lists = new List<Model.BookType>();
           Model.BookType booktype = null;
           if (da.Rows.Count > 0)
           {
               foreach (DataRow row in da.Rows)
               {
                   booktype = new Model.BookType();
                   LoadEdity(booktype, row);
                   lists.Add(booktype);
               }
           }
           return lists;
       }
       


       /// <summary>
       /// 得到所有父级分类类别
       /// </summary>
       /// <returns></returns>
       public IList<Model.BookType> GetBookTypes()
       {
           string sql = "select * from BookType where TypeParentId=0";
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text);
           IList<Model.BookType> lists = new List<Model.BookType>();
           Model.BookType booktype = null;
           if (da.Rows.Count > 0)
           {
               foreach (DataRow row in da.Rows)
               {
                   booktype = new Model.BookType();
                   LoadEdity(booktype, row);
                   lists.Add(booktype);
               }
           }
           return lists;
       }

       private void LoadEdity(Model.BookType booktype, DataRow row)
       {
           booktype.TypeId = Convert.ToInt32(row["TypeId"]);
           booktype.TypeTitle = row["TypeTitle"].ToString();
           booktype.TypeParentId = Convert.ToInt32(row["TypeParentId"]);
       }

       /// <summary>
       /// 类型转换
       /// </summary>
       /// <param name="da"></param>
       /// <returns></returns>
       private IList<Model.BookType> TableToLists(DataTable da)
       {
           IList<Model.BookType> lists = new List<Model.BookType>();
           Model.BookType booktype = null;
           if (da.Rows.Count > 0)
           {
               foreach (DataRow row in da.Rows)
               {
                   booktype = new Model.BookType();
                   LoadEdity(booktype, row);
                   lists.Add(booktype);
               }
           }
           return lists;
       }
    }
}
