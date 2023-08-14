using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1_2.Models
{
    internal class AccountSubclass
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public decimal InBalanceActive { get; set; }
        public decimal InBalancePassive { get; set; }
        public decimal TurnoverDebit { get; set; }
        public decimal TurnoverCredit { get; set; }
        public decimal OutBalanceActive { get; set; }
        public decimal OutBalancePassive { get; set; }
        public int AccountClassId { get; set; }
        public AccountClass? AccountClass { get; set; }
        public List<Account> Accounts { get; set; }
        public AccountSubclass()
        {
            Accounts = new List<Account>();
        }
    }
}
