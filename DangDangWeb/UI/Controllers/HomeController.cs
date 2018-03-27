using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        BLL.UerInfoBLL bll = new BLL.UerInfoBLL();
        //
        // GET: /Home/
        //登录页面的加载
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login()            //登录
        {
            Model.UserInfo user = new Model.UserInfo();
            string name = Request["name"].ToString();   //用户名
            string pass = Request["password"].ToString();   //密码
            string vcode = Request["passcode"].ToString();    //验证码
            string isadmin = Request["isadmin"].ToString();   //是否是管理员

            string ValidateCode = Session["ValidateCode"] == null ? "" : Session["ValidateCode"].ToString();
            if (ValidateCode == vcode)   //验证码如果正确
            {
                Session["ValidateCode"] = null;
              
                if ( bll.Login(name, pass)!=null)   //判断用户名是否正确
                {
                    user = bll.Login(name, pass);
                    Session["UserInfo"] = user;
                    if (isadmin == "是")
                    {
                        if (user.Role == "5")       //超级管理员
                        {
                            return Content("3");  //登录成功
                        }
                        else
                        {
                            return Content("4");     //不是管理员身份 
                        }
                    }
                    return Content("3");   //登录成功
                }
                else
                {
                    return Content("2");    //用户名密码不正确
                }

            }
            else  //验证码不正确
            {
                return Content("1");
            }
        }

        //注册页面
        public ActionResult ZhuCe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ZhuCeDispose()  //注册提交
        {
            Model.UserInfo user = new Model.UserInfo();
            user.Name = Request["name"].ToString();
            user.Age = Convert.ToInt32(Request["age"]);
            user.Sex = Request["sex"].ToString();
            user.Photo = Request["phone"].ToString();
            user.Email = Request["email"].ToString();
            user.PassWord = Request["password"].ToString();
            user.Address = Request["address"].ToString();
            user.ImgUrl = Request["Imageaddress"].ToString();
            user.UserLive = Request["live"].ToString();
            user.VProblem = Request["quiz"].ToString();
            user.Resume = Request["resume"].ToString();
            user.ComeInTime = DateTime.Now;
            user.Role = "1";    //库存员
            user.Foalt = 0;    //表示 没有被删除
            if (bll.AddUserInfo(user) > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("2");
            }
          
        }
        //主页
        public ActionResult Main()
        {
            Model.UserInfo user = Session["UserInfo"] as Model.UserInfo;
            ViewData["name"] = user.Name;
            return View();
        }

        public ActionResult MainIndex()
        {
            return View();
        }


        /// <summary>
        /// ////////////////////////导航栏上的基本资料
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfo()
        {
            Model.UserInfo user = Session["UserInfo"] as Model.UserInfo;
            ViewData.Model = user;
            return View();
        }

        /// <summary>
        /// 安全设置
        /// </summary>
        /// <returns></returns>
        public ActionResult PassWord()
        {
            Model.UserInfo user = Session["UserInfo"] as Model.UserInfo;
            ViewData["pass"] = user.PassWord;
            ViewData["id"] = user.Id;
            return View();
        }

        public ActionResult PassWordEdity()
        {
            int id = Convert.ToInt32(Request["id"]);
            string newpass = Request["newpassword"].ToString();
            BLL.UerInfoBLL userbll = new BLL.UerInfoBLL();
            Model.UserInfo userinfo= userbll.GetUserInfoById(id);
            userinfo.PassWord = newpass;
            if (userbll.EdityUserInfo(userinfo)>0)
            {
                return Content("1");
            }
            else 
            {
                return Content("2");
            };
           
        }


    }
}
