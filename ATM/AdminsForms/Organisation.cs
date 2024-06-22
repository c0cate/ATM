using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.AdminsForms
{
    public class Organisation
    {
        public int ID { get; set; }
        public string organisationName { get; set; }
        public string organizationAdress { get; set; }
        public decimal balance { get; set; }

    }
}
