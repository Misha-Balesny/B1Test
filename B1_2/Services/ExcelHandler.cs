using B1_2.Models;
using ExcelDataReader;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace B1_2.Services
{
    internal class ExcelHandler
    {
        public static void AddReport(string path, AppContext context)
        {
            try
            {
                
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                FileStream fs = new(path, FileMode.Open);
            
                var reader = ExcelReaderFactory.CreateBinaryReader(fs);

                DataSet ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (x) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = false
                    }
                });
                Report report = new Report();
                List<Account> accounts = new List<Account>();
                List<AccountSubclass> subclasses = new List<AccountSubclass>();
                List<AccountClass> classes = new List<AccountClass>();
                foreach (DataTable table in ds.Tables)
                {
                    var arr = table.Select();
                    report.BankName = arr[0][0].ToString();
                    report.ReportDate = DateTime.Parse(arr[5][0].ToString());
                    report.Title = arr[1][0].ToString();
                    string str = arr[2][0].ToString().Trim("абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ".ToCharArray());
                    report.StartPeriod = DateTime.Parse(str.Remove(10, 14));
                    report.EndPeriod = DateTime.Parse(str.Remove(0, 14));
                    report.InBalanceActive = decimal.Parse(arr[arr.Length - 1][1].ToString());
                    report.InBalancePassive = decimal.Parse(arr[arr.Length - 1][2].ToString());
                    report.TurnoverDebit = decimal.Parse(arr[arr.Length - 1][3].ToString());
                    report.TurnoverCredit = decimal.Parse(arr[arr.Length - 1][4].ToString());
                    report.OutBalanceActive = decimal.Parse(arr[arr.Length - 1][5].ToString());
                    report.OutBalancePassive = decimal.Parse(arr[arr.Length - 1][6].ToString());
                    report.Classes = FindClasses(8, arr, report, context);
                }
                context.Reports.Add(report);
                context.SaveChanges();
            }
            catch (Exception ex) { }
        }
        private static List<Account> FindAccounts(int i, DataRow[] arr, AccountSubclass asc, AppContext context)
        {
            i--;
            int num;
            List<Account> accounts = new();
            while (int.TryParse(arr[i][0].ToString(), out num) && num > 100)
            {
                Account account = new Account();
                account.Number = int.Parse(arr[i][0].ToString());
                account.InBalanceActive = decimal.Parse(arr[i][1].ToString());
                account.InBalancePassive = decimal.Parse(arr[i][2].ToString());
                account.TurnoverDebit = decimal.Parse(arr[i][3].ToString());
                account.TurnoverCredit = decimal.Parse(arr[i][4].ToString());
                account.OutBalanceActive = decimal.Parse(arr[i][5].ToString());
                account.OutBalancePassive = decimal.Parse(arr[i][6].ToString());
                account.AccountSubclass = asc;
                accounts.Add(account); 
                i--;
            }
            context.Accounts.AddRange(accounts);
            return accounts;
        }
        private static List<AccountSubclass> FindSubclasses(int i, DataRow[] arr, AccountClass ac, AppContext context)
        {
            i--;
            List<AccountSubclass> accounts = new();
            while (!arr[i][0].ToString().StartsWith('К'))
            {
                if (int.Parse(arr[i][0].ToString()) < 100)
                {
                    AccountSubclass account = new AccountSubclass();
                    account.Number = int.Parse(arr[i][0].ToString());
                    account.InBalanceActive = decimal.Parse(arr[i][1].ToString());
                    account.InBalancePassive = decimal.Parse(arr[i][2].ToString());
                    account.TurnoverDebit = decimal.Parse(arr[i][3].ToString());
                    account.TurnoverCredit = decimal.Parse(arr[i][4].ToString());
                    account.OutBalanceActive = decimal.Parse(arr[i][5].ToString());
                    account.OutBalancePassive = decimal.Parse(arr[i][6].ToString());
                    account.AccountClass = ac;
                    account.Accounts = FindAccounts(i, arr, account, context);
                    accounts.Add(account);                    
                }
                i--;
            }
            context.AccountSubclasses.AddRange(accounts);
            return accounts;
        }
        private static List<AccountClass> FindClasses(int i, DataRow[] arr, Report rp, AppContext context)
        {
            int num;
            List<AccountClass> accounts = new();
            while (!arr[i][0].ToString().StartsWith('Б'))
            {
                if (arr[i][0].ToString().StartsWith('К'))
                {
                    AccountClass account = new AccountClass();
                    account.Name = arr[i][0].ToString();
                    while (!arr[i][0].ToString().StartsWith('П'))
                    {
                        i++;
                    }
                    account.Number = int.Parse(arr[i-1][0].ToString())/10;
                    account.InBalanceActive = decimal.Parse(arr[i][1].ToString());
                    account.InBalancePassive = decimal.Parse(arr[i][2].ToString());
                    account.TurnoverDebit = decimal.Parse(arr[i][3].ToString());
                    account.TurnoverCredit = decimal.Parse(arr[i][4].ToString());
                    account.OutBalanceActive = decimal.Parse(arr[i][5].ToString());
                    account.OutBalancePassive = decimal.Parse(arr[i][6].ToString());
                    account.Report = rp;
                    account.Subclasses = FindSubclasses(i, arr, account, context);
                    accounts.Add(account);                   
                }
                i++;
            }
            context.AccountClasses.AddRange(accounts);
            return accounts;
        }
        public static ReportInfo GetReportInfo(int id, AppContext context)
        {
            ReportInfo reportInfo = new ReportInfo();
            var report = context.Reports.FirstOrDefault(x => x.Id == id);
            reportInfo.Id= report.Id;   
            reportInfo.BankName = report.BankName;
            reportInfo.StartPeriod = report.StartPeriod.ToString();
            reportInfo.EndPeriod = report.EndPeriod.ToString();
            reportInfo.ReportDate = report.ReportDate.ToString();
            reportInfo.Title = report.Title;
            return reportInfo;
        }

        public static List<Row> GetRowsFromDb(int id, AppContext context)
        {
            try
            {
                List<Row> rows = new List<Row>();
                List<AccountClass> classes = new List<AccountClass>();
                classes.AddRange(context.AccountClasses.Where(p => p.ReportId == id));
                List<AccountSubclass> subclasses = new List<AccountSubclass>();
                List<Account> accounts = new List<Account>();
                foreach (var item in classes)
                {

                    var list = context.AccountSubclasses.Where(s => s.AccountClassId == item.Id);
                    subclasses.AddRange(list);
                }
                foreach (var item in subclasses)
                {
                    var list = context.Accounts.Where(a => a.AccountSubclassId == item.Id);
                    accounts.AddRange(list);
                }
                foreach (var item in classes)
                {
                    Row row = new Row();
                    row.Number = $"{item.Name}";
                    row.InBalanceActive = item.InBalanceActive.ToString();
                    row.InBalancePassive = item.InBalancePassive.ToString();
                    row.TurnoverDebit = item.TurnoverDebit.ToString();
                    row.TurnoverCredit = item.TurnoverCredit.ToString();
                    row.OutBalanceActive = item.OutBalanceActive.ToString();
                    row.OutBalancePassive = item.OutBalancePassive.ToString();
                    rows.Add(row);
                    foreach (var subclass in subclasses.Where(s => s.AccountClassId == item.Id))
                    {
                        Row subClassRow = new Row();
                        subClassRow.Number = subclass.Number.ToString();
                        subClassRow.InBalanceActive = subclass.InBalanceActive.ToString();
                        subClassRow.InBalancePassive = subclass.InBalancePassive.ToString();
                        subClassRow.TurnoverDebit = subclass.TurnoverDebit.ToString();
                        subClassRow.TurnoverCredit = subclass.TurnoverCredit.ToString();
                        subClassRow.OutBalanceActive = subclass.OutBalanceActive.ToString();
                        subClassRow.OutBalancePassive = subclass.OutBalancePassive.ToString();
                        rows.Add(subClassRow);
                        foreach (var account in accounts.Where(a => a.AccountSubclassId == subclass.Id))
                        {
                            Row accountRow = new Row();
                            accountRow.Number = account.Number.ToString();
                            accountRow.InBalanceActive = account.InBalanceActive.ToString();
                            accountRow.InBalancePassive = account.InBalancePassive.ToString();
                            accountRow.TurnoverDebit = account.TurnoverDebit.ToString();
                            accountRow.TurnoverCredit = account.TurnoverCredit.ToString();
                            accountRow.OutBalanceActive = account.OutBalanceActive.ToString();
                            accountRow.OutBalancePassive = account.OutBalancePassive.ToString();
                            rows.Add(accountRow);
                        }
                    }
                }
                var report = context.Reports.FirstOrDefault(x => x.Id == id);
                Row sum = new Row();
                sum.Number = "по банку:";
                sum.InBalanceActive = report.InBalanceActive.ToString();
                sum.InBalancePassive = report.InBalancePassive.ToString();
                sum.TurnoverDebit = report.TurnoverDebit.ToString();
                sum.TurnoverCredit = report.TurnoverCredit.ToString();
                sum.OutBalanceActive = report.OutBalanceActive.ToString();
                sum.OutBalancePassive = report.OutBalancePassive.ToString();
                rows.Add(sum);
                return rows;
            }
            catch { return null; }            
        }
    }
}
