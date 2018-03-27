using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public class SqlHerple
    {
       public static readonly string strsql = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;


       /// <summary>
       /// 得到一个表格
       /// </summary>
       /// <param name="sql"></param>
       /// <param name="type"></param>
       /// <param name="pars"></param>
       /// <returns></returns>
       public static DataTable GetTable(string sql, CommandType type, params SqlParameter[] pars)
       {
           using (SqlConnection con = new SqlConnection(strsql))   //先链接字符串
           {
               using (SqlDataAdapter sda = new SqlDataAdapter(sql, con))   
               {
                   sda.SelectCommand.CommandType = type;   //sda中的穿进去的 sql类型  是文本类型还是 存储类型
                   if (pars != null)            //判断 数据类型是不是 空
                   {
                       sda.SelectCommand.Parameters.AddRange(pars);   //
                   }
                   DataTable da = new DataTable();  //得到一个表格
                   sda.Fill(da);    //吧数据放在  表格中
                   return da;  //最后返回一个 表格
               }
           }
       }
       /// <summary>
       /// 增删改查  
       /// </summary>
       /// <param name="sql"></param>
       /// <param name="type"></param>
       /// <param name="pars"></param>
       /// <returns></returns>
       public static int ExecuteNonQuery(string sql, CommandType type, params SqlParameter[] pars)
       {
           using (SqlConnection con = new SqlConnection(strsql))
           {
               using (SqlCommand cmd = new SqlCommand(sql, con))
               {
                   cmd.CommandType = type;
                   if (pars != null)
                   {
                       cmd.Parameters.AddRange(pars);
                   }
                   con.Open();
                   return cmd.ExecuteNonQuery();
               }
           }
       }

       /// <summary>
       /// 返回首行首列
       /// </summary>
       /// <param name="sql"></param>
       /// <param name="type"></param>
       /// <param name="pars"></param>
       /// <returns></returns>
       public static object ExecuteScalar(string sql, CommandType type, params SqlParameter[] pars)
       {
           using (SqlConnection con = new SqlConnection(strsql))
           {
               using (SqlCommand cmd = new SqlCommand(sql, con))
               {
                   cmd.CommandType = type;
                   if (pars != null)
                   {
                       cmd.Parameters.AddRange(pars);
                   }
                   con.Open();
                   return cmd.ExecuteScalar();
               }
           }
       }
       /// <summary>
       /// 返回一个 sqldatareader
       /// </summary>
       /// <param name="sql"></param>
       /// <param name="type"></param>
       /// <param name="pars"></param>
       /// <returns></returns>
       public static SqlDataReader ExecuteReader(string sql, CommandType type, params SqlParameter[] pars)
       {
           using (SqlConnection con = new SqlConnection(strsql))
           {
               using (SqlCommand cmd = new SqlCommand(sql, con))
               {
                   cmd.CommandType = type;
                   if (pars != null)
                   {
                       cmd.Parameters.AddRange(pars);
                   }
                   con.Open();
                   return cmd.ExecuteReader();
                       
               }
           }
       }
       /// <summary>
       /// 执行一条查询结果语句，返回查询结果（object）
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public static object GetSingle(string sql)
       {
           using (SqlConnection con = new SqlConnection(strsql))   //链接数据库
           {
               using (SqlCommand cmd = new SqlCommand(sql, con))  //执行语句
               {
                   try
                   {
                       con.Open();
                       object obj = cmd.ExecuteScalar();
                       if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                       {
                           return null;
                       }
                       else
                       {
                           return obj;
                       }
                   }
                   catch (System.Data.SqlClient.SqlException e)
                   {
                       con.Close();
                       throw e;
                   }
               }
           }
       }

    }
}
