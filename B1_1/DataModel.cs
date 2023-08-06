using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1_1
{
    internal class DataModel
    {
        public int Id{ get; set; }
        public DateTime Date { get; set; }
        public string Lat { get; set; }
        public string Kir { get; set; }
        public int Num1 { get; set; }
        public double Num2 { get; set; }
        [NotMapped]
        public string Separator { get; set; }

        public DataModel(string str) 
        {
                string[] data = str.Split("||");
                Date = DateTime.Parse(data[0]);
                Lat = data[1];
                Kir = data[2];
                Num1 = int.Parse(data[3]);
                Num2 = double.Parse(data[4], CultureInfo.InvariantCulture);
                Separator = "||";           
        }
        public DataModel() 
        {
            Random random = new();            
            Date = DateTime.Today.AddYears(-5).AddDays(random.Next((DateTime.Today - DateTime.Today.AddYears(-5)).Days));
            Lat = GenerateString("lat");
            Kir = GenerateString("kir");
            Num1 = random.Next(1, 100000001);
            Num2 = random.NextDouble()+random.Next(1, 20);
            Separator = "||";
        }
        public override string ToString()
        {
            return(this.Date.ToString("dd/MM/yyyy")+this.Separator+this.Lat+this.Separator+this.Kir+this.Separator+this.Num1.ToString()+this.Separator+this.Num2.ToString("0.00000000", CultureInfo.InvariantCulture)+"\n");
        }

        private static string GenerateString(string s)
        {
            Random random = new();
            string symbolsLat = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string symbolsKir = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            StringBuilder sb = new();
            if (s == "lat")
            {
                for (int i = 0; i < 10; i++)
                {
                    var index = random.Next(symbolsLat.Length);
                    sb.Append(symbolsLat[index]);
                }               
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    var index = random.Next(symbolsKir.Length);
                    sb.Append(symbolsKir[index]);
                }
            }           
            return sb.ToString();
        }
        
    }
}
