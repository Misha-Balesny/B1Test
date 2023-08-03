using System.Text;

namespace B1_1
{
    internal class FileHandler
    {
        private static readonly SemaphoreSlim writeFileSemaphore = new SemaphoreSlim(0, 1);
        public static int Count = 0;
        internal static void Generate()
        {
            Task[] tasks = new Task[100];
            for (int i = 0; i < 100; i++)
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
            Task task = new Task(() =>
            {
                FileStream fs = new FileStream(string.Format("C:\\Users\\миша\\source\\repos\\B1\\B1_1\\Files\\file{0}.txt", c.ToString()), FileMode.Create);
                for (int i = 0; i < 100000; i++)
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
            if (str == null)
            {
                str = string.Empty;
            }
            Count = 0;
            Task[] tasks = new Task[100];
            for (int i = 0; i < 100; i++)
            {
                tasks[i] = MergeFiles(i, str);                
            }   
            writeFileSemaphore.Release();
            foreach(Task task in tasks)
            {
                task.Start();
            }
            Task.WaitAll(tasks);
        }
        private static Task MergeFiles(int c, string str)
        {
            FileStream fs = new FileStream("C:\\Users\\миша\\source\\repos\\B1\\B1_1\\Files\\file.txt", FileMode.Create);
            fs.Close();
            Task task = new Task(async () =>
            {
                string filePath = string.Format("C:\\Users\\миша\\source\\repos\\B1\\B1_1\\Files\\file{0}.txt", c.ToString());
                List<string> line = File.ReadAllLines(filePath).ToList();
                line.RemoveAll(s => s.IndexOf(str) > 0);
                Count += 100000 - line.Count();

                await writeFileSemaphore.WaitAsync().ConfigureAwait(false);
                File.AppendAllLines("C:\\Users\\миша\\source\\repos\\B1\\B1_1\\Files\\file.txt", line.ToArray());
                writeFileSemaphore.Release();
            });
            return task;
        }
        internal static void Import()
        {


        }
    }
}
