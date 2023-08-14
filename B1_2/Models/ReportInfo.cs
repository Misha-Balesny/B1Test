using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1_2.Models
{
    internal class ReportInfo
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string Title { get; set; }
        public string StartPeriod { get; set; }
        public string EndPeriod { get; set; }
        public string ReportDate { get; set; }
        public string InBalanceActive { get; set; }
        public string InBalancePassive { get; set; }
        public string TurnoverDebit { get; set; }
        public string TurnoverCredit { get; set; }
        public string OutBalanceActive { get; set; }
        public string OutBalancePassive { get; set; }
        public ReportInfo() { } 

        public ReportInfo(Report report) 
        {
            Id = report.Id;
            BankName = report.BankName;
            Title = report.Title;
            ReportDate = report.ReportDate.ToString();
            StartPeriod = report.StartPeriod.ToString();
            EndPeriod = report.EndPeriod.ToString();
            InBalanceActive = report.InBalanceActive.ToString();
            InBalancePassive = report.InBalancePassive.ToString();
            TurnoverDebit = report.TurnoverDebit.ToString();
            TurnoverCredit = report.TurnoverCredit.ToString();
            OutBalanceActive = report.OutBalanceActive.ToString();
            OutBalancePassive = report.OutBalancePassive.ToString();
        }
    }
}
