using System.Diagnostics;

namespace B1_1
{
    internal class Program
    {
        static void Main()
        {
            bool iscreated = false;
            while (true)
            {
                Console.WriteLine("type \"g\" to generate files, \"m\" to merge files, \"i\" to import files, \"s\" to calculate sum and median");
                Stopwatch sw = new();
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
                            iscreated = true;
                            break;
                        }
                    case "m":
                        {
                            if (iscreated)
                            {
                                Console.WriteLine("delete string:");
                                string line = Console.ReadLine();
                                Console.WriteLine("Merging...");
                                sw.Start();
                                FileHandler.Merge(line);
                                Console.WriteLine("lines deleted: "+FileHandler.Count);
                                sw.Stop();
                                TimeSpan ts = sw.Elapsed;
                                string elapsedTime = String.Format("{0:00}m {1:00}s {2:00}ms", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine(elapsedTime);
                            }
                            else
                            {
                                Console.WriteLine("generate files first");
                            }
                            break;
                        }
                    case "i": 
                        {
                            //"C:\\Users\\миша\\source\\repos\\B1\\B1_1\\Files\\file.txt"
                            Console.WriteLine("Enter file path:");
                            string path = Console.ReadLine();
                            sw.Start();
                            FileHandler.Import(path);
                            sw.Stop();
                            TimeSpan ts = sw.Elapsed;
                            string elapsedTime = String.Format("{0:00}m {1:00}s {2:00}ms", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                            Console.WriteLine(elapsedTime);
                            break; 
                        }
                    case "s":
                        {
                            Console.WriteLine(FileHandler.CallStored());
                            break;
                        }
                    default: break;
                }
            }
        }
    }
}