using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1_2.Models
{
    internal class AccountClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public decimal InBalanceActive { get; set; }
        public decimal InBalancePassive { get; set; }
        public decimal TurnoverDebit { get; set; }
        public decimal TurnoverCredit { get; set; }
        public decimal OutBalanceActive { get; set; }
        public decimal OutBalancePassive { get; set; }
        public int ReportId { get; set; }
        public Report? Report { get; set; }
        public List<AccountSubclass> Subclasses { get; set; }
        public AccountClass() 
        {
            Subclasses = new List<AccountSubclass>();
        }
    }
}
