using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Common;
using Newtonsoft.Json;


namespace UI.ashx
{
    /// <summary>
    /// BookImageUpLoad 的摘要说明
    /// </summary>
    public class BookImageUpLoad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["file"];    //file是file的id
            // Image image = Image.FromStream(file.InputStream);
            string fileExtension = System.IO.Path.GetExtension(file.FileName);   //得到文件后缀
           
            Bitmap bitmap = (Bitmap)Image.FromStream(file.InputStream);
            Common.ImageSize imagesize = new ImageSize();
            System.Drawing.Image i=imagesize.resizeImage(bitmap, new Size(500, 500));
            //随机产生一个图片名称
            string fileName = DateTime.Now.ToFileTime().ToString() + new Random().Next(1000, 10000);
            string path1 = "/BookFace/BookInfoImage/" + fileName + fileExtension;   //虚拟路径
            string path2 = context.Request.MapPath(path1);   //  将虚拟路径映射成真是路径
            i.Save(path2);
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(new { d = path1 }));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}