using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeProject.Entity.Model
{
    public class FtpServerModel
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Pwd { get; set; }
    }
}
