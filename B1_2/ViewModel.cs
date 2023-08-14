using B1_2.Models;
using B1_2.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace B1_2
{
    internal class ViewModel: INotifyPropertyChanged
    {
        private ReportInfo selectedReport;
        private int selectedReportId;
        public string SelectedBankName { get; set; }
        public string SelectedStartPeriod { get; set; }
        public string SelectedEndPeriod { get; set; }
        public string SelectedTitle { get; set; }
        public string SelectedReportDate { get; set; }
        public  ObservableCollection<ReportInfo> Reports { get; set; }
        public ObservableCollection<Row> Rows { get; set; }
        private RelayCommand openFileCommand;
        private Services.AppContext context;
        public RelayCommand OpenFileCommand
        {
            get
            {
                return openFileCommand ??
                  (openFileCommand = new RelayCommand(obj =>
                  {
                      OpenFileDialog openFileDialog = new OpenFileDialog();
                      openFileDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                      if (openFileDialog.ShowDialog() == true)
                      {
                          ExcelHandler.AddReport(openFileDialog.FileName, context);
                          Reports.Add(new ReportInfo(context.Reports.OrderBy(e => e.Id).LastOrDefault()));
                      }
                  }));
            }
        }
        public ReportInfo SelectedReport
        {
            get { return selectedReport; }
            set
            {
                selectedReport = value;
                OnPropertyChanged("SelectedReport");
                if (selectedReport != null)
                {
                    Rows.Clear();
                    var rows = ExcelHandler.GetRowsFromDb(selectedReport.Id, context);
                    foreach (var item in rows)
                    {
                        Rows.Add(item);
                    }
                    SelectedBankName = selectedReport.BankName;
                    OnPropertyChanged("SelectedBankName");
                    SelectedStartPeriod = selectedReport.StartPeriod;
                    OnPropertyChanged("SelectedStartPeriod");
                    SelectedEndPeriod = selectedReport.EndPeriod;
                    OnPropertyChanged("SelectedEndPeriod");
                    SelectedReportDate = selectedReport.ReportDate;
                    OnPropertyChanged("SelectedReportDate");
                    SelectedTitle = selectedReport.Title;
                    OnPropertyChanged("SelectedTitle");
                }
            }
        }
        public ViewModel() 
        {
            context = new B1_2.Services.AppContext();
            Reports = new ObservableCollection<ReportInfo>();
            foreach (var item in context.Reports.ToList())
            {
                Reports.Add(new ReportInfo(item));
            }    
            Rows = new ObservableCollection<Row>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
