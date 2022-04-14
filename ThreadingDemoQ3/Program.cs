using System;
using System.Threading;
public class EntryPoint
{
    static private int count = 0;
    static private object theLock = new Object();
    private static void threadFunc()
    {
        for (int i = 0; i < 10; i++)
        {
            try
            {
                //int val = Interlocked.Increment(ref count);
                Monitor.Enter(theLock);
                count++;
                Console.WriteLine(count + " printed by thread: " + Thread.CurrentThread.ManagedThreadId);
            }
            finally
            {
                Monitor.Exit(theLock);
            }
        }
    }
    public static void Main(string[] args)
    {
        IAsyncResult[] result = new IAsyncResult[10]; ;
        Action countNumber = threadFunc;
        for (int i = 0; i < 10; i++)
        {
            result[i] = countNumber.BeginInvoke(null, null);
        }

       for (int i = 0; i < 10; i++)
       {
            countNumber.EndInvoke(result[i]);
       }

    }

}