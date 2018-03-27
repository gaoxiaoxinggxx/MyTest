using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace UI.ashx
{
    /// <summary>
    /// ImageUpLoad 的摘要说明
    /// </summary>
    public class ImageUpLoad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["file"];
            //获取图片的后缀名
            string fileExtension = System.IO.Path.GetExtension(file.FileName);
            //随机产生一个图片名称
            string fileName = DateTime.Now.ToFileTime().ToString() + new Random().Next(1000, 10000);
            string path1 = "/BookFace/"+fileName+fileExtension;
            string path2 = context.Request.MapPath(path1);
            file.SaveAs(path2);
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