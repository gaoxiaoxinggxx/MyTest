using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class BookTypeTree
    {
      
       public string name { get; set; }
       public int id { get; set; }
       public bool spread { get; set; }
       public IList<BookTypeTree> children;
    }
}
