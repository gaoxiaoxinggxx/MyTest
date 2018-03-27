using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace UI.Controllers
{
    public class BookInfoController : Controller
    {
        //
        // GET: /BookInfo/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()  //添加商品信息展示页面
        {
            BLL.BookTypeBLL bll = new BLL.BookTypeBLL();
            IList<Model.BookType> lists= bll.GetFatherBooktypes("TypeParentId=0");
           // ViewData.Model = lists;
            ViewData["One"] = new SelectList(lists, "TypeId", "TypeTitle");
            return View();
        }

        public void SelectCommon(string num)
        {
              BLL.BookTypeBLL bll = new BLL.BookTypeBLL();

              if (Request["typeid"] != null)
              {
                  int id = Convert.ToInt32(Request["typeid"]);
                  IList<Model.BookType> ilisttwo = bll.GetFatherBooktypes(" TypeParentId=" + id);
                  List<Model.BookType> listtwo = new List<Model.BookType>();
                  foreach (Model.BookType type in ilisttwo)
                  {
                      listtwo.Add(type);
                  }
                  ViewData[num] = new SelectList(listtwo, "TypeId", "TypeTitle");
                
              }
              else
              {
                  IList<Model.BookType> ilisttwo = bll.GetFatherBooktypes(" TypeId=0 ");
                  List<Model.BookType> listtwo = new List<Model.BookType>();
                  foreach (Model.BookType type in ilisttwo)
                  {
                      listtwo.Add(type);
                  }
                  ViewData[num] = new SelectList(listtwo, "TypeId", "TypeTitle");
               
              }
        }    //是下面三级联动的公共方法

        public ActionResult AddSelectTwo()
        {
            SelectCommon("two");
            return PartialView();
           
        }   //添加商品信息页中的三级联动

        public ActionResult AddSelectThree()
        {
            SelectCommon("three");
            return PartialView();
        }    //添加商品信息页中的三级联动

        [ValidateInput(false)]      //取消对标签的检查
        public ActionResult AddBookInfoForm()     //添加商品信息提交表单的处理
        {
            Model.BookInfo bookinfo = new Model.BookInfo();
            BLL.BooInfoBLL bll = new BLL.BooInfoBLL();
            bookinfo.Title = Request["title"].ToString();
            bookinfo.TitleHot = Request["TitleHot"].ToString();
            bookinfo.PriceOld = Convert.ToDecimal(Request["PriceOld"]);
            bookinfo.PriceNew = Convert.ToDecimal(Request["PriceNew"]);
            bookinfo.Author = Request["Author"].ToString();
            bookinfo.Publisher = Request["Publisher"].ToString();
            bookinfo.PublishDate= Convert.ToDateTime(Request["PublishDate"]);
            bookinfo.OnSaleDate = Convert.ToDateTime(Request["OnSaleDate"]);
            bookinfo.ISBN = Request["Isbn"].ToString();
            string quiz1 = Request["quiz1"].ToString();
            string three = Request["three"].ToString();
            string two = Request["two"].ToString();
            bookinfo.TypeId = quiz1 + "," + two + "," + three;
            bookinfo.ImgTitle = Request["imgurl"].ToString();
            bookinfo.Details = Request["Details"].ToString();
            if (bll.AddBookInfo(bookinfo) > 0)
            {
                return Content("1");             //添加成功
            }
            else
            {
                return Content("2");             //添加失败
            }
        }

        public ActionResult EditDeleteBookInfo()         //删除查询 页面展示
        {
            BLL.BooInfoBLL bookinfobll = new BLL.BooInfoBLL();
            int RowCounts=bookinfobll.GetCountPages();      //总条数
            ViewData["pageCount"] = Convert.ToInt32(Math.Ceiling(RowCounts * 1.0 / 10));                             //总页数
            return View();
        }


        public ActionResult EditDeleteTableShow()  //删除修改 页 的表格展示 我们是要分页展示的
        {
            BLL.BooInfoBLL bookinfobll = new BLL.BooInfoBLL();
            int pageIndex = 1;
            int pageSize = 10;
            if (!string.IsNullOrEmpty(Request["pIndex"]))
            {
                pageIndex = int.Parse(Request["pIndex"]);
            }
            int rowsCount = bookinfobll.GetCountPages();  //得到总条数
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
            IList<Model.BookInfo> lists = bookinfobll.GetPagesBookInfos(startIndex, endIndex);
            ViewData.Model = lists;
            ViewData["PageCount"] = pageCount;
            return PartialView(lists);     //向我们的分部页面  传过去一个对象集
        }

        public ActionResult DeleteBookInfo()             //删除商品信息
        {
            BLL.BooInfoBLL bll = new BLL.BooInfoBLL();
            int id = Convert.ToInt32(Request["id"]);
            if (bll.DeleteBookInfo(id) > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("2");
            }
            
        }

        [HttpGet]
        public ActionResult EdityBookInfoShow()      //修改页面的展示页
        {
            BLL.BooInfoBLL bookinfobll = new BLL.BooInfoBLL();
            BLL.BookTypeBLL booktypebll = new BLL.BookTypeBLL();

            int id = Convert.ToInt32(Request["id"]);
            Model.BookInfo bookinfomodel= bookinfobll.GetBookInfosByCondition("Bid="+id)[0];
            ViewData.Model = bookinfomodel;

            IList<Model.BookType> lists= booktypebll.GetFatherBooktypes("TypeParentId=0");
            ViewData["One"] = new SelectList(lists, "TypeId", "TypeTitle");

           string TypeIds=bookinfomodel.TypeId;
           string typenames="";
           string[] strs = TypeIds.Split(',');
           for (int i = 0; i < strs.Length; i++)
           {
               if (strs[i] != "")
               {
                   typenames += booktypebll.GetFatherBooktypes("TypeId=" + strs[i])[0].TypeTitle + "###";
               }
           }
           ViewData["TypeNames"] = typenames;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)] 
        public ActionResult EdityBookInfo()         //修改商品信息
        {
            Model.BookInfo bookinfo = new Model.BookInfo();
            BLL.BooInfoBLL bll = new BLL.BooInfoBLL();
            bookinfo.Bid = Convert.ToInt32(Request["Bid"]);
            bookinfo.Title = Request["title"].ToString();
            bookinfo.TitleHot = Request["TitleHot"].ToString();
            bookinfo.PriceOld = Convert.ToDecimal(Request["PriceOld"]);
            bookinfo.PriceNew = Convert.ToDecimal(Request["PriceNew"]);
            bookinfo.Author = Request["Author"].ToString();
            bookinfo.Publisher = Request["Publisher"].ToString();
            bookinfo.PublishDate = Convert.ToDateTime(Request["PublishDate"]);
            bookinfo.OnSaleDate = Convert.ToDateTime(Request["OnSaleDate"]);
            bookinfo.ISBN = Request["Isbn"].ToString();
            string quiz1 = Request["quiz1"].ToString();
            if (quiz1 == "")
            {
                bookinfo.TypeId = Request["TypeId"].ToString();
            }
            else 
            {
                string two = Request["two"].ToString();
                if (two == "")
                {
                    bookinfo.TypeId = quiz1 + "," + two;
                }
                else
                {
                    string three = Request["three"].ToString();
                    bookinfo.TypeId = quiz1 + "," + two + "," + three;
                }
            }
            bookinfo.ImgTitle = Request["imgurl"].ToString();
            bookinfo.Details = Request["Details"].ToString();
            if (bll.EdityBookInfo(bookinfo) > 0)
            {
                return Content("1");             //添加成功
            }
            else
            {
                return Content("2");             //添加失败
            }
        }


        ///////////////////////////////////////////////////////下面是详细页面展示
        [HttpGet]
        public ActionResult DetailsShow()
        {
            BLL.BooInfoBLL bookinfobll = new BLL.BooInfoBLL();
            int id = Convert.ToInt32(Request["id"]);
            ViewData.Model = bookinfobll.GetBookInfosByCondition("Bid="+id)[0];
            return View();
        }


        /////////////////////////////////////////////////////////////////////查询操作

        public ActionResult FindBookInfoShow()          //查询操作页面展示
        {
           BLL.BookTypeBLL bll = new BLL.BookTypeBLL();
           IList<Model.BookType> lists = bll.GetFatherBooktypes("TypeParentId=0");
            // ViewData.Model = lists;
           ViewData["One"] = new SelectList(lists, "TypeId", "TypeTitle");
            return View(); 
        }


        public ActionResult FindBookInfo()
        {
            BLL.BooInfoBLL bll = new BLL.BooInfoBLL();
            IList<Model.BookInfo> lists= bll.GetBookInfosByCondition("TypeId='0'");
            return PartialView(lists);
        }

      
        public ActionResult FindBookInfoButton()
        {
            IList<Model.BookInfo> lists = new List<Model.BookInfo>();
            BLL.BooInfoBLL bll = new BLL.BooInfoBLL();
            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1 ");
            string title = Request["title"].ToString();
            if (title != "")
            {
                sb.Append("and Title like '%"+title+"%' ");
            }
            string author = Request["author"].ToString();
            if (author != "")
            {
                sb.Append("and author like '%"+author+"%' ");
            }
            string publisher = Request["publisher"].ToString();
            if (publisher != "")
            {
                sb.Append("and publisher like '%" + publisher + "%' ");
            }
            string quiz1 = Request["quiz1"].ToString();
            string two=Request["two"].ToString();
            string three=Request["three"].ToString();
            if (three != "")
            {
                sb.Append("and TypeId='" + quiz1 + "," + two + "," + three + ",' ");
            }
            else if (two != "")
            {
                sb.Append("and TypeId='" + quiz1 + "," + two + ",' ");
            }
            else if (quiz1 != "")
            {
                sb.Append("and TypeId='" + quiz1 + ",' ");
            }

            string pricemin = Request["price_min"].ToString();
            if (pricemin != "")
            {
                sb.Append("and PriceNew>" + pricemin + " ");
            }
            string  pricemax = Request["price_max"].ToString();
            if (pricemax != "")
            {
                sb.Append("and PriceNew<"+pricemax+" ");
            }
            string date1 = Request["date1"].ToString();
            if (date1 != "")
            {
                sb.Append("and PublishDate>'" + date1 + "' ");
            }
            string date2 = Request["date2"].ToString();
            if (date2 != "")
            {
                sb.Append("and PublishDate<'" + date2 + "' ");
            }
            string condition = sb.ToString();

            if (condition != " ")
            {
                lists = bll.GetBookInfosByCondition(condition);
            }
            else
            {
                lists = bll.GetBookInfosByCondition("TypeId='0'");
            }
            ViewData.Model = lists;
            return PartialView("FindBookInfo");
        }









    }
}
