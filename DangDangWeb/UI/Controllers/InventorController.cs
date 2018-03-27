using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class InventorController : Controller
    {
        //
        // GET: /Inventor/

        public ActionResult Index()   //分布页  信息展示部分
        {
            if (!string.IsNullOrEmpty(Request["quiz1"]))
            {
                if (Request["quiz1"] == "0")
                {
                    return Content("1");
                }
                else
                {
                    BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(" BookType='" + Request["quiz1"] + "' ");
                    sb.Append("and Foalt=1 ");
                    IList<Model.BiaoDanInfo> lists = bll.GetListsByCondition(sb.ToString());
                    return PartialView(lists);
                }
            }
            else
            {
                BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
                int pageIndex = 1;
                int pageSize = 10;
                if (!string.IsNullOrEmpty(Request["pIndex"]))
                {
                    pageIndex = int.Parse(Request["pIndex"]);
                }
                int rowsCount = bll.GetPageCount();  //得到总条数
                int pageCount = Convert.ToInt32(Math.Ceiling(rowsCount * 1.0 / pageSize));
                //检查索引的有效性
                if (pageIndex <= 1)
                {
                    pageIndex = 1;
                }
                if (pageIndex >= pageCount)
                {
                    pageIndex = pageCount;
                }
                //查询当前页数据
                int startIndex = (pageIndex - 1) * pageSize + 1;
                int endIndex = pageIndex * pageSize;
                IList<Model.BiaoDanInfo> lists = bll.GetPageLists(startIndex, endIndex);
                ViewData.Model = lists;
                return PartialView(lists);     //向我们的分部页面  传过去一个对象集
            }

        }
       
        //查询表单
        public ActionResult List()
        {
            BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
            BLL.BookTypeBLL typebll = new BLL.BookTypeBLL();
            int pageSize = 10;
            int rowsCount = bll.GetPageCount();  //得到总条数
            int pageCount = Convert.ToInt32(Math.Ceiling(rowsCount * 1.0 / pageSize));
            ViewData["PageCount"] = pageCount;  //总页数 
            ViewData.Model= typebll.GetBookTypes();
            return View();        
        }
       



       
        //第一次加载  添加页面的时候  
        public ActionResult Add()
        {
            BLL.BookTypeBLL bll = new BLL.BookTypeBLL();
            ViewData.Model = bll.GetBookTypes();
            return View();
        }
        //添加表单信息  后台处理
        [HttpPost]
        public ActionResult AddInfo()
        {
            BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
            Model.BiaoDanInfo biaodan = new Model.BiaoDanInfo();
            biaodan.BookType= Request["modules"].ToString();
            biaodan.BookName = Request["name"].ToString();
            biaodan.Count = Convert.ToInt32(Request["count"]);
            biaodan.Price= Convert.ToInt32(Request["price_min"]);
            biaodan.Publisher = Request["publisher"].ToString();
            biaodan.PiCi = Request["pc"].ToString();
            biaodan.DateTime = Request["date"]!=null?Convert.ToDateTime(Request["date"]):DateTime.Now;
            biaodan.Foalt = 2;   //未审核状态

            if (bll.AddBiaoDanInfo(biaodan) > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("2");
            }
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////操作表单页面   星哥注释
        /// </summary>
        /// <returns></returns>


        public ActionResult Edit()        //操作表单页面
        {
            BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
            BLL.BookTypeBLL typebll = new BLL.BookTypeBLL();
            int pageSize = 10;
            int rowsCount = bll.GetPageCountByCondition(" 1=1 ");  //得到总条数
            int pageCount = Convert.ToInt32(Math.Ceiling(rowsCount * 1.0 / pageSize));
            ViewData["PageCount"] = pageCount;  //总页数 
            ViewData.Model = typebll.GetBookTypes();  //给页面的搜索部分 动态添加上类别
            return View(); 
        }

        public ActionResult EditTable()      //分部页面  表格
        {
            //当表单 不是请选择  或者是   有批次的时候
            if (!string.IsNullOrEmpty(Request["quiz1"]) || !string.IsNullOrEmpty(Request["number"]) || Request["ispass"]!=null)
            {
                BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(" 1=1 ");
                if (Request["quiz1"] != "0")
                {
                    sb.Append("and BookType='" + Request["quiz1"] + "' ");
                }
                if (Request["number"] != "")
                {
                    sb.Append("and PiCi like '%"+Request["number"]+"%' ");
                }
                if (Request["ispass"] != null)
                {
                    sb.Append("and Foalt=" + Request["ispass"] + " ");
                }
                IList<Model.BiaoDanInfo> lists=  bll.GetListsByCondition(sb.ToString());
                return PartialView(lists);
            }
            else
            {
                BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
                int pageIndex = 1;
                int pageSize = 10;
                if (!string.IsNullOrEmpty(Request["pIndex"]))
                {
                    pageIndex = int.Parse(Request["pIndex"]);
                }
                int rowsCount = bll.GetPageCountByCondition(" 1=1 ");  //得到总条数
                int pageCount = Convert.ToInt32(Math.Ceiling(rowsCount * 1.0 / pageSize));
                //检查索引的有效性
                if (pageIndex <= 1)
                {
                    pageIndex = 1;
                }
                if (pageIndex >= pageCount)
                {
                    pageIndex = pageCount;
                }
                //查询当前页数据
                int startIndex = (pageIndex - 1) * pageSize + 1;
                int endIndex = pageIndex * pageSize;
                IList<Model.BiaoDanInfo> lists = bll.GetPagteListsBycondition(startIndex, endIndex," 1=1 ");
                ViewData.Model = lists;
                return PartialView(lists);     //向我们的分部页面  传过去一个对象集
            }
        }

        public ActionResult DeleteBiaoDanInfo()     //删除表格信息
        {
            BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
            int id = Convert.ToInt32(Request["id"]);
            if (bll.DeleteModelById(id) > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("2");
            }
        }

        public ActionResult SHBiaoDanInfo()              //审核页面  的弹窗传值
        {
            BLL.BookTypeBLL typebll = new BLL.BookTypeBLL();
            BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
            IList<Model.BookType> lists = typebll.GetBookTypes();
            int id = Convert.ToInt32(Request["id"]);
            Model.BiaoDanInfo biaodan = bll.GetListsByCondition("Id=" + id)[0];
            ViewData["id"] = biaodan.Id;
            ViewData["booktype"] = biaodan.BookType;
            ViewData["bookname"] = biaodan.BookName;
            ViewData["pici"] = biaodan.PiCi;
            ViewData["count"] = biaodan.Count;
            ViewData["price"] = biaodan.Price;
            ViewData["publisher"] = biaodan.Publisher;
            ViewData["datetime"] = biaodan.DateTime;
            ViewData["foalt"] = biaodan.Foalt;
            return PartialView(lists);
        }
        public ActionResult BiaoDanInfoSH()          //通过审核之后我们进行处理的程序
        {
            BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
            int id = Convert.ToInt32(Request["id"]);
            int foalt = Convert.ToInt32(Request["foalt"]);
            Model.BiaoDanInfo biaodan= bll.GetListsByCondition(" Id=" + id)[0];
            if (foalt == 1)
            {
                biaodan.Foalt = 2;
            }
            else
            {
                biaodan.Foalt = 1;
            }
            if (bll.EditBiaoDanByModel(biaodan) > 0)
            {
                return Content("1");        
            }
            else
            {
                return Content("2");
            }
            
        }


        public ActionResult EditBiaoDanInfo()         //展示修改的 分部页面
        {
            BLL.BookTypeBLL typebll = new BLL.BookTypeBLL();
            BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
            IList<Model.BookType> lists= typebll.GetBookTypes();
            int id = Convert.ToInt32(Request["id"]);
            Model.BiaoDanInfo biaodan= bll.GetListsByCondition("Id=" + id)[0];

            ViewData["id"] = biaodan.Id;
            ViewData["booktype"] = biaodan.BookType;
            ViewData["bookname"] = biaodan.BookName;
            ViewData["pici"] = biaodan.PiCi;
            ViewData["count"] = biaodan.Count;
            ViewData["price"] = biaodan.Price;
            ViewData["publisher"] = biaodan.Publisher;
            ViewData["datetime"] = biaodan.DateTime;
            ViewData["foalt"] = biaodan.Foalt;
            return PartialView(lists);
        }

        public ActionResult BiaoDanInfoEdit()      //修改  表中的信息
        {
            Model.BiaoDanInfo biaodan = new Model.BiaoDanInfo();
            BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
            biaodan.Id = Convert.ToInt32(Request["id"]);
            biaodan.BookType = Request["modules"].ToString();
            biaodan.BookName = Request["name"].ToString();
            biaodan.Count = Convert.ToInt32(Request["count"]);
            biaodan.Price = Convert.ToInt32(Request["price_min"]);
            biaodan.Publisher = Request["publisher"].ToString();
            biaodan.PiCi = Request["pc"].ToString();
            biaodan.DateTime = Convert.ToDateTime(Request["date"]);
            biaodan.Foalt = Convert.ToInt32(Request["foalt"]);   //表示 已经审核通过的表单信息
            if (bll.EditBiaoDanByModel(biaodan) > 0)
            {
                return Content("1");       //表示修改成功
            }
            else
            {
                return Content("2");         //表示修改失败
            }
        }



        /// <summary>
        /// /////////////////////////////////////////////////////////////////////审核表单  /////////////////////////////////  
        /// </summary>
        /// <returns></returns>
        public ActionResult AuditingList()
        {
            BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
            //BLL.BookTypeBLL typebll = new BLL.BookTypeBLL();
            int pageSize = 10;
            int rowsCount = bll.GetPageCountByCondition(" Foalt=2 ");  //得到总条数  没有被审核的 条数
            int pageCount = Convert.ToInt32(Math.Ceiling(rowsCount * 1.0 / pageSize));
            ViewData["PageCount"] = pageCount;  //总页数 
            //ViewData.Model = typebll.GetBookTypes();  //给页面的搜索部分 动态添加上类别
            return View(); 
        }

        public ActionResult AuditingTable()
        {
            //BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
            //IList<Model.BiaoDanInfo> lists= bll.GetListsByCondition(" Foalt=2 ");   //表示  没有被审核的列表信息
            //return PartialView(lists);

            BLL.BiaoDanInfoBLL bll = new BLL.BiaoDanInfoBLL();
            int pageIndex = 1;
            int pageSize = 10;
            if (!string.IsNullOrEmpty(Request["pIndex"]))
            {
                pageIndex = int.Parse(Request["pIndex"]);
            }
            int rowsCount = bll.GetPageCountByCondition(" Foalt=2 ");  //得到总条数
            int pageCount = Convert.ToInt32(Math.Ceiling(rowsCount * 1.0 / pageSize));
            //检查索引的有效性
            if (pageIndex <= 1)
            {
                pageIndex = 1;
            }
            if (pageIndex >= pageCount)
            {
                pageIndex = pageCount;
            }
            //查询当前页数据
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            IList<Model.BiaoDanInfo> lists = bll.GetPagteListsBycondition(startIndex, endIndex, " Foalt=2 ");
            ViewData.Model = lists;
            return PartialView(lists);     //向我们的分部页面  传过去一个对象集
        }

    }
}
