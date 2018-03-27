using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class BookType
    {
       public int TypeId { get; set; }
       public string TypeTitle { get; set; }
       public int TypeParentId { get; set; }
    }
}
