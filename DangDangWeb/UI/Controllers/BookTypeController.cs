using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace UI.Controllers
{
    public class BookTypeController : Controller
    {
        //
        // GET: /BookType/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()       //添加页面的展示  并且将 tree树加载到页面上
        {
            JavaScriptSerializer json=new JavaScriptSerializer ();
            var getdata = findAllParents().ToArray<Model.BookTypeTree>();
            var data = json.Serialize(findAllParents());
            ViewData["json"] = data;
            return View();
        }

        /// <summary>
        /// 得到所有的父节点的方法
        /// </summary>
        /// <returns></returns>
        public IList<Model.BookTypeTree> findAllParents()
        {
            //首先得到父级对象
            BLL.BookTypeBLL bll = new BLL.BookTypeBLL();
            IList<Model.BookType> fatherlists = bll.GetFatherBooktypes(" TypeParentId = 0 ");
            Model.BookTypeTree tree = null;
            IList<Model.BookTypeTree> lists = new List<Model.BookTypeTree>();
            foreach (Model.BookType booktype in fatherlists)  //遍历循环所有的父节点对象
            {
                tree = new Model.BookTypeTree();
                tree.id = booktype.TypeId;
                tree.name = booktype.TypeTitle;
                tree.spread = false;
                tree.children = findChildByPid(booktype.TypeId);

                lists.Add(tree);

            }
            return lists;
        }

        /// <summary>
        /// 得到所有父节点的下的孩子对象
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public IList<Model.BookTypeTree> findChildByPid(int pid)
        {
            BLL.BookTypeBLL bll = new BLL.BookTypeBLL();
            IList<Model.BookType> childlists = bll.GetFatherBooktypes(" TypeParentId="+pid);
            Model.BookTypeTree tree = null;
            IList<Model.BookTypeTree> lists = new List<Model.BookTypeTree>();
            foreach (Model.BookType booktype in childlists)
            {
                 tree = new Model.BookTypeTree()
                {
                    id = booktype.TypeId,
                    name = booktype.TypeTitle,
                    spread = false,
                    children = findChildByPid(booktype.TypeId)      //找到所有的子节点  包括子节点下的子节点都能得到
                };
                lists.Add(tree);
            }
            return lists;
        }


        public ActionResult AddBookType()       //添加类别的弹出窗  展示页面
        {
            return PartialView();
        }

        public ActionResult BookTypeAdd()       //类别添加
        {
            Model.BookType type = new Model.BookType();
            BLL.BookTypeBLL bll = new BLL.BookTypeBLL();
            type.TypeTitle = Request["name"].ToString();
            type.TypeParentId = 0;
            if (bll.AddBookType(type) > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("2");
            }
           
        }


        public ActionResult CaoZuoBookType()        //弹窗页面的 的展示
        {
            int id = Convert.ToInt32(Request["id"]);
            ViewData["id"] = id;
            return PartialView();
        }


        public ActionResult BookTypeCaoZuo()        //对tree的操作  添加和修改    
        {
            BLL.BookTypeBLL bll = new BLL.BookTypeBLL();
            Model.BookType booktype = new Model.BookType();
            string name = Request["name"].ToString();
            int id = Convert.ToInt32(Request["id"]);  //得到要操作的的id
            int type = Convert.ToInt32(Request["type"]);   //判断要操作的类型
            if (type == 1)    //添加子项
            {
                booktype.TypeTitle = name;
                booktype.TypeParentId = id;
                if (bll.AddBookType(booktype) > 0)
                {
                    return Content("1");        //添加子项添加成功
                }
                else
                {
                    return Content("2");       //添加失败
                }
            }
            if (type == 3)      //修改子项
            {
               booktype= bll.GetFatherBooktypes(" TypeId=" + id)[0];
               booktype.TypeTitle = name;
               if (bll.EdityBookType(booktype) > 0)
               {
                   return Content("11");     //修改类别名称  成功
               }
               else
               {
                   return Content("22");    //失败
               }
               
            }
            return Content("3");          //出现了异常
        }

        public ActionResult DeleteBookType()       //删除  子项
        {
            BLL.BookTypeBLL bll=new BLL.BookTypeBLL ();
            int id = Convert.ToInt32(Request["id"]);
            if (bll.GetFatherBooktypes(" TypeParentId=" + id).Count > 0)
            {
                return Content("0");    //下面有子项不能删除
            }
            else    //否则是下面没有子项的
            {
                if (bll.DelectBookType(id) > 0)    //如何  大于0  着删除成功
                {
                    return Content("1");
                }
                else
                {
                    return Content("2");
                }
            }
        }

        ///////////////////////////////////////分类查询
        public ActionResult FindShow()             //页面加载的时候我们会把一级的   信息加载进去
        {
            BLL.BookTypeBLL bll = new BLL.BookTypeBLL();
           IList<Model.BookType> ilistone= bll.GetFatherBooktypes(" TypeParentId=0 ");
           List<Model.BookType> listone = new List<Model.BookType>();
           foreach (Model.BookType type in ilistone)
           {
               listone.Add(type);
           }
           ViewData["One"] = new SelectList(listone, "TypeId", "TypeTitle");
           int rowsCount = bll.GetCountPages();  //得到总条数
           int pageCount = Convert.ToInt32(Math.Ceiling(rowsCount * 1.0 / 10));
           ViewData["PageCount"] = pageCount;
            return View();
        }

        public ActionResult TwoBookType()
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
                ViewData["two"] = new SelectList(listtwo, "TypeId", "TypeTitle");
                return PartialView();
            }
            else
            {
                IList<Model.BookType> ilisttwo = bll.GetFatherBooktypes(" TypeId=0 ");
                List<Model.BookType> listtwo = new List<Model.BookType>();
                foreach (Model.BookType type in ilisttwo)
                {
                    listtwo.Add(type);
                }
                ViewData["two"] = new SelectList(listtwo, "TypeId", "TypeTitle");
                return PartialView();
            }
          
        }

        public ActionResult ThreeBookType()
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
                ViewData["three"] = new SelectList(listtwo, "TypeId", "TypeTitle");
                return PartialView();
            }
            else
            {
                IList<Model.BookType> ilisttwo = bll.GetFatherBooktypes(" TypeId=0 ");
                List<Model.BookType> listtwo = new List<Model.BookType>();
                foreach (Model.BookType type in ilisttwo)
                {
                    listtwo.Add(type);
                }
                ViewData["three"] = new SelectList(listtwo, "TypeId", "TypeTitle");
                return PartialView();
            }

        }


        public ActionResult SjldLookUp()                  //分类信息  分类查询 三级联动查询   我们已经把编号传过来了
        {
            BLL.BookTypeBLL booktypebll = new BLL.BookTypeBLL();
            string strid = Request["data"];
          // string.IsNullOrEmpty(strid)
            if (string.IsNullOrEmpty(strid))
            {
                //IList<Model.BookType> lists = booktypebll.GetAllBookTypes();
                //return PartialView(lists);
                int pageIndex = 1;
                int pageSize = 10;
                if (!string.IsNullOrEmpty(Request["pIndex"]))
                {
                    pageIndex = int.Parse(Request["pIndex"]);
                }
                int rowsCount = booktypebll.GetCountPages();  //得到总条数
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
                IList<Model.BookType> lists = booktypebll.GetPageLists(startIndex, endIndex);
                ViewData.Model = lists;
                ViewData["PageCount"] =pageCount;
                return PartialView(lists);     //向我们的分部页面  传过去一个对象集


            }
            else
            {
                string[] strs = strid.Split(',');           //得到编号 我们要查询的编号
                IList<Model.BookType> lists = null;
                if (strs[2] != "") 
                {
                  lists= booktypebll.GetFatherBooktypes("TypeId="+strs[2]);
               
                }
                else if (strs[1] != "")
                {
                   lists = booktypebll.GetFatherBooktypes("TypeParentId=" +strs[1]);
                  
                }
                else
                {
                  lists = booktypebll.GetFatherBooktypes("TypeParentId=" + strs[0]);
                }
                ViewData.Model = lists;
                return PartialView(lists);
              
            }
        }

      

    }
}
