using System;
using System.Threading;

namespace testApp
{
    public class Task1
    {
        static SyncQueue<string> queue = new SyncQueue<string>();
        private const int readWaitMs = 1000;//константы что бы поиграться
        private const int writeWaitMs = 200;

        public Task1()
        {
            Thread t1 = new Thread(new ParameterizedThreadStart(Reader));
            t1.Start("Reader 1");
            Thread t2 = new Thread(new ParameterizedThreadStart(Reader));
            t2.Start("Reader 2");
            Thread t3 = new Thread(new ParameterizedThreadStart(Writer));
            t3.Start("Writer 1");
            Thread t4 = new Thread(new ParameterizedThreadStart(Writer));
            t4.Start("Writer 2");
        }
        public static void Reader(object t)
        {
            while (true)
            {
                var res = queue.Pop();
                var sleepMs = new Random().Next(100, readWaitMs);
                Console.WriteLine($"{t}#{DateTime.Now.ToLongTimeString()}:got    '{res}':going sleep for {sleepMs} ms");
                Thread.Sleep(sleepMs);
            }
        }
        public static void Writer(object t)
        {
            while (true)
            {
                var random = new Random();
                var randstr = random.Next(100, 200).ToString();
                queue.Push(randstr);
                var sleepMs = random.Next(100, writeWaitMs);
                Console.WriteLine($"{t}#{DateTime.Now.ToLongTimeString()}:pushed '{randstr}':going sleep for {sleepMs} ms");
                Thread.Sleep(sleepMs);
            }
        }
    }
}
