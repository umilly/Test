using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;


public class SyncQueue<T>:IDisposable
{
    private Queue<T> queue=new Queue<T>();
    EventWaitHandle hasSomething = new EventWaitHandle(false,EventResetMode.ManualReset);
    public void Push(T obj)
    {
        lock (queue)
        {
            queue.Enqueue(obj);
            hasSomething.Set();//оповещаем что что то пришло, если кому интересно
            //Console.WriteLine($"=>{queue.Count()}");
        }
        
    }
    //Данная реализация Pop не гарантирует что мы получим что либо в том же хронологическом порядке 
    //в котором было обращение если таких Pop ов будет несколько из разных потоков в ожидании чуда(push)
    //но этого и не требовалось в задании, а усложнять алгоритм не охота..
    public T Pop()
    {
        bool succes = false;
        T res=default(T);
        while (!succes)
        {
            lock (queue)
            {
                if (queue.Any())
                {
                    res = queue.Dequeue();
                    //Console.WriteLine($"<={queue.Count()}");
                    if (!queue.Any())
                        hasSomething.Reset();//говорим что взяли последнее и ловить нечего 
                    succes = true;
                }
            }
            if (!succes)
            {
                hasSomething.WaitOne();//подождём... вдруг что появится.
            }
        }
        return res;
    }    
    public void Dispose()
    {
        hasSomething.Dispose();
    }
}
