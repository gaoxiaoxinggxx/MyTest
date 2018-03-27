using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class DiscussBLL
    {
       DAL.DiscussDAL dal = new DAL.DiscussDAL();


       /// <summary>
       /// 分页
       /// </summary>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public IList<Model.Discuss> GetPageCount(int start, int end)
       {
           return dal.GetPageCount(start, end);
       }

       /// <summary>
       /// 得到表格中所有的的数据条数
       /// </summary>
       /// <param name="tablename"></param>
       /// <returns></returns>
       public int GetAllRowCount()
       {
           return dal.GetAllRowCount();
       }


        /// <summary>
       /// 修改评论信息
       /// </summary>
       /// <param name="modeldis"></param>
       /// <returns></returns>
       public int EdityDiscuss(Model.Discuss modeldis)
       {
           return dal.EdityDiscuss(modeldis);
       }


        /// <summary>
       /// 通过id删除对象
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int DeleteDiscussById(int id)
       {
           return dal.DeleteDiscussById(id);
       }

          /// <summary>
       /// 通过条件得到对象集
       /// </summary>
       /// <param name="condition"></param>
       /// <returns></returns>
       public IList<Model.Discuss> GetListsByCondition(string condition)
       {
           return dal.GetListsByCondition(condition);
       }
    }
}
