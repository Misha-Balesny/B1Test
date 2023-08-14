using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1_2.Models
{
    internal class Row
    {
        public string Number { get; set; }
        public string InBalanceActive { get; set; }
        public string InBalancePassive { get; set; }
        public string TurnoverDebit { get; set; }
        public string TurnoverCredit { get; set; }
        public string OutBalanceActive { get; set; }
        public string OutBalancePassive { get; set; }
    }
}
