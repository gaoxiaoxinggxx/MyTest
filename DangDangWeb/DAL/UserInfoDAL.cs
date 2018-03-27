using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public class UserInfoDAL
    {

       /// <summary>
       /// 修改用户信息
       /// </summary>
       /// <param name="user"></param>
       /// <returns></returns>
       public int EdityUserInfo(Model.UserInfo user)
       {
           string sql = "update  UserInfo set Name=@Name,Age=@Age,Sex=@Sex,Photo=@Photo,Email=@Email,PassWord=@PassWord,Address=@Address,ImgUrl=@ImgUrl,UserLive=@UserLive,VProblem=@VProblem,Resume=@Resume,Role=@Role,ComeInTime=@ComeInTime,Foalt=@Foalt where Id=" + user.Id;
           SqlParameter[] pars = { 
                       new SqlParameter("@Name",SqlDbType.NVarChar,10),
                        new SqlParameter("@Age",SqlDbType.Int,10),
                         new SqlParameter("@Sex",SqlDbType.NVarChar,10),
                          new SqlParameter("@Photo",SqlDbType.NVarChar,50),
                           new SqlParameter("@Email",SqlDbType.NVarChar,50),
                            new SqlParameter("@PassWord",SqlDbType.NVarChar,50),
                             new SqlParameter("@Address",SqlDbType.NVarChar,100),
                              new SqlParameter("@ImgUrl",SqlDbType.NVarChar,200),
                               new SqlParameter("@UserLive",SqlDbType.NVarChar,50),
                                new SqlParameter("@VProblem",SqlDbType.NVarChar,50),
                                 new SqlParameter("@Resume",SqlDbType.NVarChar,200),
                                  new SqlParameter("@Role",SqlDbType.NVarChar,50),
                                  new SqlParameter("@ComeInTime",SqlDbType.DateTime,50),
                                   new SqlParameter("@Foalt",SqlDbType.Int,10)
                                 };
           pars[0].Value = user.Name;
           pars[1].Value = user.Age;
           pars[2].Value = user.Sex;
           pars[3].Value = user.Photo;
           pars[4].Value = user.Email;
           pars[5].Value = user.PassWord;
           pars[6].Value = user.Address;
           pars[7].Value = user.ImgUrl;
           pars[8].Value = user.UserLive;
           pars[9].Value = user.VProblem;
           pars[10].Value = user.Resume;
           pars[11].Value = user.Role;
           pars[12].Value = user.ComeInTime;
           pars[13].Value = user.Foalt;
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, pars);
       }


       /// <summary>
       /// 通过id得到对象
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public Model.UserInfo GetUserInfoById(int id)
       {
           string sql = "select * from UserInfo Where Id=@id";
           SqlParameter[] pars = { 
                           new SqlParameter("@id",id) 
                                 };
           Model.UserInfo user = null;
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text, pars);
           if (da.Rows.Count > 0)
           {
               foreach (DataRow row in da.Rows)
               {
                   user = new Model.UserInfo();
                   LoadEdity(user,row);
               }
           }
           return user;
       }

       /// <summary>
       /// 登录  验证用户名和密码
       /// </summary>
       /// <param name="name"></param>
       /// <param name="pass"></param>
       /// <returns></returns>
       public Model.UserInfo Login(string name, string pass)
       {
           string sql = "select * from UserInfo where Name=@Name and PassWord=@PassWord";
           SqlParameter[] pars = { 
                           new SqlParameter("@Name",SqlDbType.NVarChar,50),
                             new SqlParameter("@PassWord",SqlDbType.NVarChar,50),
                                 };
           pars[0].Value = name;
           pars[1].Value = pass;
           DataTable da = SqlHerple.GetTable(sql, CommandType.Text, pars);
           Model.UserInfo user = null;
           if (da.Rows.Count > 0)
           {
               foreach (DataRow row in da.Rows)
               {
                   user = new Model.UserInfo();
                   LoadEdity(user, row);
               }
           }
           return user;
       }

       private void LoadEdity(Model.UserInfo user, DataRow row)
       {
           user.Id = Convert.ToInt32(row["Id"]);
           user.Name = row["Name"].ToString();
           user.Age = Convert.ToInt32(row["Age"]);
           user.Sex = row["Sex"].ToString();
           user.Photo = row["Photo"].ToString();
           user.Email = row["Email"].ToString();
           user.PassWord = row["PassWord"].ToString();
           user.Address = row["Address"].ToString();
           user.ImgUrl = row["ImgUrl"].ToString();
           user.UserLive = row["UserLive"].ToString();
           user.VProblem = row["VProblem"].ToString();
           user.Resume = row["Resume"].ToString();
           user.Role = row["Role"].ToString();
           user.ComeInTime = Convert.ToDateTime(row["ComeInTime"]);
           user.Foalt = Convert.ToInt32(row["Foalt"]);
         
       }




       /// <summary>
       /// 添加用户信息
       /// </summary>
       /// <param name="user"></param>
       /// <returns></returns>
       public int AddUserInfo(Model.UserInfo user)
       {
           string sql = "insert into UserInfo (Name,Age,Sex,Photo,Email,PassWord,Address,ImgUrl,UserLive,VProblem,Resume,Role,ComeInTime,Foalt) values (@Name,@Age,@Sex,@Photo,@Email,@PassWord,@Address,@ImgUrl,@UserLive,@VProblem,@Resume,@Role,@ComeInTime,@Foalt)";
           SqlParameter[] pars = { 
                       new SqlParameter("@Name",SqlDbType.NVarChar,10),
                        new SqlParameter("@Age",SqlDbType.Int,10),
                         new SqlParameter("@Sex",SqlDbType.NVarChar,10),
                          new SqlParameter("@Photo",SqlDbType.NVarChar,50),
                           new SqlParameter("@Email",SqlDbType.NVarChar,50),
                            new SqlParameter("@PassWord",SqlDbType.NVarChar,50),
                             new SqlParameter("@Address",SqlDbType.NVarChar,100),
                              new SqlParameter("@ImgUrl",SqlDbType.NVarChar,200),
                               new SqlParameter("@UserLive",SqlDbType.NVarChar,50),
                                new SqlParameter("@VProblem",SqlDbType.NVarChar,50),
                                 new SqlParameter("@Resume",SqlDbType.NVarChar,200),
                                  new SqlParameter("@Role",SqlDbType.NVarChar,50),
                                  new SqlParameter("@ComeInTime",SqlDbType.DateTime,50),
                                   new SqlParameter("@Foalt",SqlDbType.Int,10)
                                 };
           pars[0].Value = user.Name;
           pars[1].Value = user.Age;
           pars[2].Value = user.Sex;
           pars[3].Value = user.Photo;
           pars[4].Value = user.Email;
           pars[5].Value = user.PassWord;
           pars[6].Value = user.Address;
           pars[7].Value = user.ImgUrl;
           pars[8].Value = user.UserLive;
           pars[9].Value = user.VProblem;
           pars[10].Value = user.Resume;
           pars[11].Value = user.Role;
           pars[12].Value = user.ComeInTime;
           pars[13].Value = user.Foalt;
           return SqlHerple.ExecuteNonQuery(sql, CommandType.Text, pars);

       }

    }
}
