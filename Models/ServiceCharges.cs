using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ServiceCharges
    {
        public int RTGS { get; set; }
        public int IMPS { get; set; }
        public ServiceCharges(int RTGS,int IMPS) 
        {
            this.RTGS = RTGS;
            this.IMPS = IMPS;
        }
    }
}
