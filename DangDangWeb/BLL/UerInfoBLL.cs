using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class UerInfoBLL
    {
       DAL.UserInfoDAL dal = new DAL.UserInfoDAL();



         /// <summary>
       /// 修改用户信息
       /// </summary>
       /// <param name="user"></param>
       /// <returns></returns>
       public int EdityUserInfo(Model.UserInfo user)
       {
           return dal.EdityUserInfo(user);
       }

          /// <summary>
       /// 通过id得到对象
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public Model.UserInfo GetUserInfoById(int id)
       {
           return dal.GetUserInfoById(id);
       }

         /// <summary>
       /// 登录  验证用户名和密码
       /// </summary>
       /// <param name="name"></param>
       /// <param name="pass"></param>
       /// <returns></returns>
       public Model.UserInfo Login(string name, string pass)
       {
           return dal.Login(name, pass);
       }



        /// <summary>
       /// 添加用户信息
       /// </summary>
       /// <param name="user"></param>
       /// <returns></returns>
       public int AddUserInfo(Model.UserInfo user)
       {
           return dal.AddUserInfo(user);
       }
    }
}
