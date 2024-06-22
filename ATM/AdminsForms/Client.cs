using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.AdminsForms
{
    public class CardTable
    {
        public int ID { get; set; }
        public string number { get; set; }
        public string dateCard { get; set; }
        public string cvc { get; set; }
        public string passwordCard { get; set; }
        public string ownerName{ get; set; }
        public decimal balance { get; set; }

    }
}
