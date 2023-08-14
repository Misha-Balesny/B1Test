using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1_2.Models
{
    internal class Report
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string Title { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public DateTime ReportDate { get; set; }
        public decimal InBalanceActive { get; set; }
        public decimal InBalancePassive { get; set; }
        public decimal TurnoverDebit { get; set; }
        public decimal TurnoverCredit { get; set; }
        public decimal OutBalanceActive { get; set; }
        public decimal OutBalancePassive { get; set; }
        public List<AccountClass> Classes{get; set;}

        public Report() 
        {
            Classes = new List<AccountClass>();
        }

    }
}
