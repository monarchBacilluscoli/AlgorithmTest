using System;

namespace QueueStackLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            {// Queue test
                int[] encrypted_nums = { 6, 3, 1, 7, 5, 8, 9, 2, 4 }; // the array to be decrypted.
                Queue<int> que = new Queue<int>(encrypted_nums, 10);
                while (!que.Empty)
                {
                    System.Console.WriteLine(que.Pop().ToString() + "\t"); // pop the first one, note it as an item in the decrypted qq
                    try
                    {
                        que.Push(que.Pop()); // pop the second one and push it to the tail of the queue.
                    }
                    catch (IndexOutOfRangeException)
                    {
                        break; // queue empty, break.
                    }
                }
                return;
            }
        }
    }
}
