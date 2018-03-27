using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace UI.ashx
{
    /// <summary>
    /// Code 的摘要说明
    /// </summary>
    public class Code : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //验证码  我们需要返回一种图片
            context.Response.ContentType = "image/jpeg";
            int codeLength = 4;   //定义一个长度
            string sourceString = "abcdef0123456789";   //验证码从这里面随机出来
            string temp = "";
            Color[] Colors = { Color.Yellow, Color.Blue, Color.Black, Color.Red, Color.Green };
            string[] Fonts = { "黑体", "宋体", "仿宋", "楷体", "幼圆", "华文中宋", "微软雅黑体", "隶书", "华文细黑" };
            Random r = new Random();   //声明一个随机书变量
            for (int i = 0; i < codeLength; i++)
            {
                temp += sourceString[r.Next(0, sourceString.Length)];
            }
            context.Session["ValidateCode"] = temp;  //存到我们的服务器端的存储机制
           
            Bitmap bitmap = new Bitmap(80, 32);   //定义一个画布  
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(ColorTranslator.FromHtml("#F5F5F5"));  //自定义背景颜色
            g.DrawRectangle(new Pen(ColorTranslator.FromHtml("#DDDDDD")), 0, 0, bitmap.Width - 1, bitmap.Height - 1);
            for (int i = 0; i < codeLength; i++)
            {
                Point p = new Point(i * 18, 0);
                g.DrawString(temp[i].ToString(), new Font(Fonts[r.Next(0, 9)], 20, FontStyle.Bold), new SolidBrush(Colors[r.Next(0, 5)]), p);
            }
            bitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg);
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