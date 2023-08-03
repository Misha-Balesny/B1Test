using System.Diagnostics;

namespace B1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool iscreated = false;
            while (true)
            {
                Console.WriteLine("type \"g\" to generate files, \"m\" to merge files, \"i\" to import files");
                Stopwatch sw = new Stopwatch();
                switch (Console.ReadLine())
                {
                    case "g":
                        {
                            Console.WriteLine("Generating...");
                            sw.Start();
                            FileHandler.Generate();
                            sw.Stop();
                            TimeSpan ts = sw.Elapsed;
                            string elapsedTime = String.Format("{0:00}s {1:00}ms",ts.Seconds, ts.Milliseconds / 10);
                            Console.WriteLine("generated "+ elapsedTime);
                            break;
                        }
                    case "m":
                        {                               
                            Console.WriteLine("delete string:");
                            string line = Console.ReadLine();
                            Console.WriteLine("Merging...");
                            sw.Start();
                            FileHandler.Merge(line);
                            Console.WriteLine("lines deleted: "+FileHandler.Count);
                            sw.Stop();
                            TimeSpan ts = sw.Elapsed;
                            string elapsedTime = String.Format("{0:00}m {1:00}s {2:00}ms",ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                            Console.WriteLine(elapsedTime);                                                           
                            break;
                        }
                    case "i": 
                        { 
                            FileHandler.Import(); 
                            break; 
                        }
                    default: break;
                }
            }
        }
    }
}