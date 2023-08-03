using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1_1
{
    internal class DataModel
    {
        public DateTime date;
        public string lat;
        public string kir;
        public int num1;
        public double num2;
        public string separator;

        public DataModel(string str) 
        {
            string[] data = str.Split("||", 1);
            date = DateTime.Parse(data[0]);
            lat = data[1];
            kir = data[2];
            num1 = int.Parse(data[3]);
            num2 = double.Parse(data[4]);
            separator = "||";
        }
        public DataModel() 
        {
            Random random = new Random();            
            date = DateTime.Today.AddYears(-5).AddDays(random.Next((DateTime.Today - DateTime.Today.AddYears(-5)).Days));
            lat = generateString("lat");
            kir = generateString("kir");
            num1 = random.Next(1, 100000001);
            num2 = random.NextDouble()+random.Next(1, 20);
            separator = "||";
        }
        public override string ToString()
        {
            return(this.date.ToString("dd/MM/yyyy")+this.separator+this.lat+this.separator+this.kir+this.separator+this.num1.ToString()+this.separator+this.num2.ToString("0.00000000", CultureInfo.InvariantCulture)+"\n");
        }

        private string generateString(string s)
        {
            Random random = new Random();
            string symbolsLat = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string symbolsKir = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            StringBuilder sb = new StringBuilder();
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
