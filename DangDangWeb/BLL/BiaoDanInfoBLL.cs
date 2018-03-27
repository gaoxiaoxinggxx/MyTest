using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class BiaoDanInfoBLL
    {
       DAL.BiaoDanInfoDAL dal = new DAL.BiaoDanInfoDAL();
       

         /// <summary>
       /// 通过条件得到总页数
       /// </summary>
       /// <param name="condition"></param>
       /// <returns></returns>
       public int GetPageCountByCondition(string condition)
       {
           return dal.GetPageCountByCondition(condition);
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
           return dal.GetPagteListsBycondition(start, end, condition);
       }
        /// <summary>
       /// 修改 表单信息 
       /// </summary>
       /// <param name="biaodan"></param>
       /// <returns></returns>
       public int EditBiaoDanByModel(Model.BiaoDanInfo biaodan)
       {
           return dal.EditBiaoDanByModel(biaodan);
       }

       /// <summary>
       /// 通过id删除 表单信息
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int DeleteModelById(int id)
       {
           return dal.DeleteModelById(id);
       }

         /// <summary>
       /// 通过条件查询对象集
       /// </summary>
       /// <param name="condition"></param>
       /// <returns></returns>
       public IList<Model.BiaoDanInfo> GetListsByCondition(string condition)
       {
           return dal.GetListsByCondition(condition);
       }



       /// <summary>
       /// 分页
       /// </summary>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public IList<Model.BiaoDanInfo> GetPageLists(int start, int end)
       {
           return dal.GetPageLists(start, end);
       }

        /// <summary>
       /// 得到总页数
       /// </summary>
       /// <returns></returns>
       public int GetPageCount()
       {
           return dal.GetPageCount();
       }

        /// <summary>
       /// 查询所有的表单
       /// </summary>
       /// <returns></returns>
       public IList<Model.BiaoDanInfo> GetLists()
       {
           return dal.GetLists();
       }

        /// <summary>
       /// 添加表单
       /// </summary>
       /// <param name="biaodan"></param>
       /// <returns></returns>
       public int AddBiaoDanInfo(Model.BiaoDanInfo biaodan)
       {
           return dal.AddBiaoDanInfo(biaodan);
       }
    }
}
