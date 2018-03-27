using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class Discuss
    {
       public int Did { get; set; }
       public int Bid { get; set; }
       public string Discusses { get; set; }
       public int JycCount { get; set; }
       public int JgcCount { get; set; }
       public string DisName { get; set; }
       public DateTime Date { get; set; }
       public string HfDiscussId { get; set; }
       public int Float { get; set; }
       public BookInfo BookInfo { get; set; }
    }
}
