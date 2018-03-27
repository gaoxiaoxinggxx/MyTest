using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class BookInfo
    {
       public int Bid { get; set; }
       public string Title { get; set; }
       public string TitleHot { get; set; }
       public decimal PriceOld { get; set; }
       public decimal PriceNew { get; set; }
       public string Author { get; set; }
       public string Publisher { get; set; }
       public DateTime PublishDate { get; set; }
       public DateTime OnSaleDate { get; set; }
       public string ISBN { get; set; }
       public string TypeId { get; set; }
       public string Details { get; set; }
       public string ImgTitle { get; set; }
    }
}
