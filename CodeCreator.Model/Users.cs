using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCreator.Model
{
    [Serializable]
    public class Users
    {
        public Int32 id { get; set; }
        public string user { get; set; }
        public string name { get; set; }
        public string pwd { get; set; }
        public DateTime newdate { get; set; }
        public DateTime lastdate { get; set; }
        public Boolean lockuser { get; set; }
        public Int32 order { get; set; }
        public Boolean delete { get; set; }
        public Int32 admin { get; set; }

    }
}
