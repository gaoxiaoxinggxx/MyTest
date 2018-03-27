using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class DiscussController : Controller
    {
        //
        // GET: /Discuss/ 
        BLL.DiscussBLL bll = new BLL.DiscussBLL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DiscussCaoZou()
        {
            int RowCount= bll.GetAllRowCount();
            ViewData["PageCount"] = Convert.ToInt32(Math.Ceiling(RowCount * 1.0 / 10));
            return View();
        }          //主页面

        public ActionResult DiscussTable()          //查询表格
        {
            IList<Model.Discuss> lists = new List<Model.Discuss>();
                int pageIndex = 1;
                int pageSize = 10;
                if (!string.IsNullOrEmpty(Request["pageIndex"]))
                {
                    pageIndex = int.Parse(Request["pageIndex"]);
                }
                int rowsCount = bll.GetAllRowCount();  //得到总条数
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
                lists = bll.GetPageCount(startIndex, endIndex);
            return PartialView(lists);
        }

        [HttpPost]
        public ActionResult EdiytDiscuss()
        {
            Model.Discuss modeldis = new Model.Discuss();
            int id = Convert.ToInt32(Request["id"]);
            modeldis= bll.GetListsByCondition("Did=" + id)[0];
            if (modeldis.Float == 0)
            {
                modeldis.Float = 1;
            }
            else
            {
                modeldis.Float = 0;
            }
            if (bll.EdityDiscuss(modeldis) > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("2");
            }
          
        }

    }
}
