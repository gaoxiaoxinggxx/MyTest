using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class BooInfoBLL
    {
       DAL.BookInfoDAL dal = new DAL.BookInfoDAL();


          /// <summary>
       /// 修改 商品信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public int EdityBookInfo(Model.BookInfo model)
       {
           return dal.EdityBookInfo(model);
       }

         /// <summary>
       /// 通过id删除 对象
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int DeleteBookInfo(int id)
       {
           return dal.DeleteBookInfo(id);
       }


         /// <summary>
       /// 通过条件得到集合
       /// </summary>
       /// <param name="condition"></param>
       /// <returns></returns>
       public IList<Model.BookInfo> GetBookInfosByCondition(string condition)
       {
           return dal.GetBookInfosByCondition(condition);
       }

         /// <summary>
       /// 分页 起始页  终止页
       /// </summary>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public IList<Model.BookInfo> GetPagesBookInfos(int start, int end)
       {
           return dal.GetPagesBookInfos(start, end);
       }

         /// <summary>
       /// 得到数据库信息总条数
       /// </summary>
       /// <returns></returns>
       public int GetCountPages()
       {
          return  dal.GetCountPages();
       }

     


        /// <summary>
       /// 添加商品信息
       /// </summary>
       /// <param name="bookinfo"></param>
       /// <returns></returns>
       public int AddBookInfo(Model.BookInfo bookinfo)
       {
           return dal.AddBookInfo(bookinfo);
       }
    }
}
