using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
   public class Page<T>
       where T:class
    {
       /// <summary>
       /// 分页查询
       /// </summary>
       /// <param name="tablename"></param>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable GetPageCount(string tablename, int start, int end)
       {
           string sql = "select  *  from (select ROW_NUMBER() over (order by Bid) as num,* from @tablename) as b  where b.num>@start and b.num<@end";
           SqlParameter[] pars = { 
                             new  SqlParameter("@tablename",tablename),
                             new  SqlParameter("@start",start),
                             new  SqlParameter("@end",end)
                                 };
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text, pars);
           return da;
       }

      


       /// <summary>
       /// 得到表中所有的条数信息
       /// </summary>
       /// <param name="tablename"></param>
       /// <returns></returns>
       public int GetAllRowCount(string tablename)
       {
           string sql = "select count(*) from "+tablename;
           return Convert.ToInt32(SqlHerple.GetSingle(sql));
       }
    }
}
