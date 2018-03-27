using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class BookTypeBLL
    {
       DAL.BookTypeDAL dal = new DAL.BookTypeDAL();

         /// <summary>
       /// 分页查询  
       /// </summary>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public IList<Model.BookType> GetPageLists(int start, int end)
       {
           return dal.GetPageLists(start, end);
       }


          /// <summary>
       /// 得到总信息条数
       /// </summary>
       /// <returns></returns>
       public int GetCountPages()
       {
           return dal.GetCountPages();
       }

      /// <summary>
       /// 修改  类别 
       /// </summary>
       /// <param name="booktype"></param>
       /// <returns></returns>
       public int EdityBookType(Model.BookType booktype)
       {
           return dal.EdityBookType(booktype);
       }

         /// <summary>
       /// 删除 子节点
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int DelectBookType(int id)
       {
           return dal.DelectBookType(id);
       }

         /// <summary>
       /// 添加BookType类别
       /// </summary>
       /// <param name="type"></param>
       /// <returns></returns>
       public int AddBookType(Model.BookType type)
       {
           return dal.AddBookType(type);
       }

        /// <summary>
       /// 根据条件得到  对象集
       /// </summary>
       /// <param name="condition"></param>
       /// <returns></returns>
       public IList<Model.BookType> GetFatherBooktypes(string condition)
       {
           return dal.GetFatherBooktypes(condition);
       }

        /// <summary>
       /// 得到所有的  分类信息
       /// </summary>
       /// <returns></returns>
       public IList<Model.BookType> GetAllBookTypes()
       {
           return dal.GetAllBookTypes();
       }






        /// <summary>
       /// 得到分类类别
       /// </summary>
       /// <returns></returns>
       public IList<Model.BookType> GetBookTypes()
       {
           return dal.GetBookTypes();
       }
    }
}
