using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public class BookInfoDAL
    {

       /// <summary>
       /// 修改 商品信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public int EdityBookInfo(Model.BookInfo model)
       {
           string sql = "update  BookInfo set Title=@Title,TitleHot=@TitleHot,PriceOld=@PriceOld,PriceNew=@PriceNew,Author=@Author,Publisher=@Publisher,PublishDate=@PublishDate,OnSaleDate=@OnSaleDate,ISBN=@ISBN,TypeId=@TypeId,Details=@Details,ImgTitle=@ImgTitle where Bid=" + model.Bid;
           SqlParameter[] pars = { 
                         new SqlParameter("@Title",SqlDbType.NVarChar,50),
                           new SqlParameter("@TitleHot",SqlDbType.NVarChar,100),
                             new SqlParameter("@PriceOld",SqlDbType.Int,50),
                               new SqlParameter("@PriceNew",SqlDbType.Int,50),
                                 new SqlParameter("@Author",SqlDbType.NVarChar,50),
                                   new SqlParameter("@Publisher",SqlDbType.NVarChar,100), 
                                     new SqlParameter("@PublishDate",SqlDbType.DateTime,50),
                                       new SqlParameter("@OnSaleDate",SqlDbType.DateTime,50),
                                         new SqlParameter("@ISBN",SqlDbType.NVarChar,50),
                                           new SqlParameter("@TypeId",SqlDbType.NVarChar,50),
                                              new SqlParameter("@Details",SqlDbType.NVarChar,300),   
                                              new SqlParameter("@ImgTitle",SqlDbType.NVarChar,100)
                                };
          
           pars[0].Value = model.Title;
           pars[1].Value = model.TitleHot;
           pars[2].Value = model.PriceOld;
           pars[3].Value = model.PriceNew;
           pars[4].Value = model.Author;
           pars[5].Value = model.Publisher;
           pars[6].Value = model.PublishDate;
           pars[7].Value = model.OnSaleDate;
           pars[8].Value = model.ISBN;
           pars[9].Value = model.TypeId;
           pars[10].Value = model.Details;
           pars[11].Value = model.ImgTitle;
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, pars);
       }

       /// <summary>
       /// 通过id删除 对象
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int DeleteBookInfo(int id)
       {
           string sql = "delete BookInfo where Bid=@id";
           SqlParameter[] pars = {
                     new SqlParameter("@id",id)
                                 };
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, pars);

       }

       /// <summary>
       /// 得到数据库信息总条数
       /// </summary>
       /// <returns></returns>
       public int GetCountPages()
       {
           string sql = "select count(*) from bookinfo";
           return Convert.ToInt32(SqlHerple.GetSingle(sql));
       }

       /// <summary>
       /// 分页 起始页  终止页
       /// </summary>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public IList<Model.BookInfo> GetPagesBookInfos(int start, int end)
       {
           string sql = "select * from (select ROW_NUMBER() over (order by Bid) as num,* from BookInfo ) as t where t.num>=@start and t.num<=@end";
           SqlParameter[] pars = { 
                            new  SqlParameter("@start",start),
                            new SqlParameter("@end",end)
                                 };
          DataTable da= SqlHerple.GetTable(sql, CommandType.Text, pars);
          return  TableToLists(da);
       }

       /// <summary>
       /// 通过条件得到集合
       /// </summary>
       /// <param name="condition"></param>
       /// <returns></returns>
       public IList<Model.BookInfo> GetBookInfosByCondition(string condition)
       {
           string sql = "select * from BookInfo where "+condition;
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text);

           return TableToLists(da);

       }

       /// <summary>
       /// 将表格  转换成  lists集合
       /// </summary>
       /// <param name="da"></param>
       /// <returns></returns>
       private IList<Model.BookInfo> TableToLists(DataTable da)
       {
           IList<Model.BookInfo> lists = new List<Model.BookInfo>();
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
       /// 把数据模型转换成概念模型
       /// </summary>
       /// <param name="row"></param>
       /// <returns></returns>
       private Model.BookInfo DataRowToModel(DataRow row)
       {
           Model.BookInfo model = new Model.BookInfo();
           if (row != null)
           {
               if (row["Bid"] != null && row["Bid"].ToString() != "")
               {
                   model.Bid = int.Parse(row["Bid"].ToString());
               }
               if (row["Title"] != null)
               {
                   model.Title = row["Title"].ToString();
               }
               if (row["TitleHot"] != null)
               {
                   model.TitleHot = row["TitleHot"].ToString();
               }
               if (row["PriceOld"] != null && row["PriceOld"].ToString() != "")
               {
                   model.PriceOld = decimal.Parse(row["PriceOld"].ToString());
               }
               if (row["PriceNew"] != null && row["PriceNew"].ToString() != "")
               {
                   model.PriceNew = decimal.Parse(row["PriceNew"].ToString());
               }
               if (row["Author"] != null)
               {
                   model.Author = row["Author"].ToString();
               }
               if (row["Publisher"] != null)
               {
                   model.Publisher = row["Publisher"].ToString();
               }
               if (row["PublishDate"] != null && row["PublishDate"].ToString() != "")
               {
                   model.PublishDate = DateTime.Parse(row["PublishDate"].ToString());
               }
               if (row["OnSaleDate"] != null && row["OnSaleDate"].ToString() != "")
               {
                   model.OnSaleDate = DateTime.Parse(row["OnSaleDate"].ToString());
               }
               if (row["ISBN"] != null)
               {
                   model.ISBN = row["ISBN"].ToString();
               }
               if (row["TypeId"] != null)
               {
                   model.TypeId = row["TypeId"].ToString();
               }
               if (row["Details"] != null)
               {
                   model.Details = row["Details"].ToString();
               }
               if (row["ImgTitle"] != null)
               {
                   model.ImgTitle = row["ImgTitle"].ToString();
               }
           }
           return model;
       }



       /// <summary>
       /// 添加商品信息
       /// </summary>
       /// <param name="bookinfo"></param>
       /// <returns></returns>
       public int AddBookInfo(Model.BookInfo bookinfo)
       {
           string sql = "insert into BookInfo (Title,TitleHot,PriceOld,PriceNew,Author,Publisher,PublishDate,OnSaleDate,ISBN,TypeId,Details,ImgTitle) values(@Title,@TitleHot,@PriceOld,@PriceNew,@Author,@Publisher,@PublishDate,@OnSaleDate,@ISBN,@TypeId,@Details,@ImgTitle)";
           SqlParameter[] pars = { 
                         new SqlParameter("@Title",SqlDbType.NVarChar,50),
                           new SqlParameter("@TitleHot",SqlDbType.NVarChar,100),
                             new SqlParameter("@PriceOld",SqlDbType.Int,50),
                               new SqlParameter("@PriceNew",SqlDbType.Int,50),
                                 new SqlParameter("@Author",SqlDbType.NVarChar,50),
                                   new SqlParameter("@Publisher",SqlDbType.NVarChar,100), 
                                     new SqlParameter("@PublishDate",SqlDbType.DateTime,50),
                                       new SqlParameter("@OnSaleDate",SqlDbType.DateTime,50),
                                         new SqlParameter("@ISBN",SqlDbType.NVarChar,50),
                                           new SqlParameter("@TypeId",SqlDbType.NVarChar,50),
                                              new SqlParameter("@Details",SqlDbType.NVarChar,300),   
                                              new SqlParameter("@ImgTitle",SqlDbType.NVarChar,100)
                                };
           pars[0].Value = bookinfo.Title;
           pars[1].Value = bookinfo.TitleHot;
           pars[2].Value = bookinfo.PriceOld;
           pars[3].Value = bookinfo.PriceNew;
           pars[4].Value = bookinfo.Author;
           pars[5].Value = bookinfo.Publisher;
           pars[6].Value = bookinfo.PublishDate;
           pars[7].Value = bookinfo.OnSaleDate;
           pars[8].Value = bookinfo.ISBN;
           pars[9].Value = bookinfo.TypeId;
           pars[10].Value = bookinfo.Details;
           pars[11].Value = bookinfo.ImgTitle;
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, pars);
       }
    }
}
