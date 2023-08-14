using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1_2.Models
{
    internal class Account
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public decimal InBalanceActive { get; set; }
        public decimal InBalancePassive { get; set; }
        public decimal TurnoverDebit { get; set; }
        public decimal TurnoverCredit { get; set; }
        public decimal OutBalanceActive { get; set; }
        public decimal OutBalancePassive { get; set; }
        public int AccountSubclassId { get; set; }
        public AccountSubclass? AccountSubclass { get; set; }
    }
}
