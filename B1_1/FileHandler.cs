using Microsoft.Data.SqlClient;
using System.Text;

namespace B1_1
{
    internal class FileHandler
    {
        private static readonly int filesCount = 100;
        private static readonly int linesCount = 100000;
        public static int Count = 0;
        private static readonly string selectNumbersSum = "SELECT SUM(CAST(num1 AS decimal)) FROM [B1_1].[dbo].[Lines]";
        private static readonly string selectNumbersMed = "SELECT TOP(1) Percentile_Disc(0.5) WITHIN GROUP(ORDER BY num2) OVER() FROM[B1_1].[dbo].[Lines]";
        private static Mutex mutex = new();
        internal static void Generate()
        {
            Task[] tasks = new Task[filesCount];
            for (int i = 0; i < filesCount; i++)
            {                
                tasks[i] = GenerateFile(i);
            }
            foreach (Task task in tasks)
            {
                task.Start();
            }
            Task.WaitAll(tasks);
        }
        private static Task GenerateFile(int c)
        {
            Task task = new(() =>
            {
                FileStream fs = new(string.Format("file{0}.txt", c.ToString()), FileMode.Create);
                for (int i = 0; i < linesCount; i++)
                {
                    var text = new DataModel();
                    fs.Write(Encoding.Default.GetBytes(text.ToString()));
                }
                fs.Close();
            });           
            return task;
        }
        internal static void Merge(string str)
        {
            FileStream fs = new("file.txt", FileMode.Create);

            str ??= string.Empty;
            Count = 0;
            Task[] tasks = new Task[filesCount];
            for (int i = 0; i < filesCount; i++)
            {
                tasks[i] = MergeFile(i, str, fs);                
            }   
            
            foreach(Task task in tasks)
            {
                task.Start();
            }
            Task.WaitAll(tasks);
            fs.Close();
            Console.WriteLine("merged");
        }
        private static Task MergeFile(int c, string str, FileStream fs)
        {            
            Task task = new(() =>
            {
                string filePath = string.Format("file{0}.txt", c.ToString());
                List<string> line = File.ReadAllLines(filePath).ToList();
                line.RemoveAll(s => s.IndexOf(str) > 0);
                Count += linesCount - line.Count;
                mutex.WaitOne();
                fs.Write(Encoding.Default.GetBytes(string.Join("\n", line) + "\n"));   
                mutex.ReleaseMutex();
            });
            return task;
        }       
        internal static void Import(string path)
        {            
            using AppContext context = new();
            int i = 1;
            try
            {
                List<string> lines = File.ReadAllLines(path).ToList();
                foreach (var item in lines)
                {                   
                    context.Add(new DataModel(item));
                    Console.WriteLine($"{i++} / {lines.Count}");
                    context.SaveChanges();
                }
                Console.WriteLine("Imported");
            }
            catch (Exception e)
            {
                Console.WriteLine($"!exeption! {e}");
            }            
        }
        internal static string CallStored()
        {            
            return $"sum = {CalculateNumbersSum()} median = {CalculateDoubleNumbersMedian()}";
        }
        public static decimal CalculateNumbersSum()
        {
            decimal res = 0;
            AppContext db = new();
            using (var connection = new SqlConnection(db.ConnectionString))
            {
                connection.Open();
                var selectSumCommand = new SqlCommand(selectNumbersSum, connection);
                var prob_res = selectSumCommand.ExecuteScalar();
                res = Convert.ToDecimal(prob_res);
            }
            return res;
        }
        public static double CalculateDoubleNumbersMedian()
        {
            double res = 0.0;
            AppContext db = new();
            using (var connection = new SqlConnection(db.ConnectionString))
            {
                connection.Open();
                var selectSumCommand = new SqlCommand(selectNumbersMed, connection);
                var prob_res = selectSumCommand.ExecuteScalar();
                res = Convert.ToDouble(prob_res);
            }
            return res;
        }
    }
}
