// 栈，队列和链表的实验
using System;

namespace QueueStackLinkedList
{
    class Program
    {
        static bool IsPalindrome(String s)
        {
            if (s.Length <= 1)
            {
                return true;
            }
            Stack<Int32> st = new Stack<Int32>();
            Int32 mid = s.Length / 2;
            for (int i = 0; i < mid; i++)
            {
                st.Push(s[i]);
            }
            Int32 start = s.Length % 2 == 0 ? mid : mid + 1;

            for (int i = start; i < s.Length; i++)
            {
                Int32 temp = st.Pop();
                if (temp != s[i])
                {
                    return false;
                }
            }
            return true;
        }
        static void LinkedListTest()
        {
            LinkedList<Int32> list = new LinkedList<int>(new int[] { 11, 14, 16, 98, 192 });
            var foreNode = list.Find((ListNode<Int32> data) => { return data.Next.Data.CompareTo(14) > 0; });
            list.Insert(foreNode, 15);
            ListNode<Int32> temp = list.Head;
            for (int i = 0; i < list.Length; i++)
            {
                System.Console.WriteLine(temp.Data);
                temp = temp.Next;
            }
        }
        static void Main(string[] args)
        {
            {
                LinkedListTest();
                return;
            }
            {// small cat fishing
                Queue<Int32> xiaoHeng = new Queue<Int32>(new Int32[] { 2, 4, 1, 2, 5, 6 }, 100), xiaoHa = new Queue<Int32>(new int[] { 3, 1, 3, 5, 6, 4 }, 100);
                Stack<Int32> table = new Stack<Int32>();

                bool isXiaoHengTurn = true;
                while (!(xiaoHa.Empty || xiaoHeng.Empty)) // Both hands are not empty, continue
                {
                    //todo plays
                    Queue<Int32> player = isXiaoHengTurn ? xiaoHeng : xiaoHa;
                    Int32 card = player.Pop();
                    int sameCardIndex = -1;
                    for (int i = 0; i < table.Size; i++)
                    {
                        if (table.Top(i) == card)
                        {
                            sameCardIndex = i;
                        }
                    }
                    if (sameCardIndex != -1)
                    {
                        player.Push(card);
                        for (int i = 0; i <= sameCardIndex; i++)
                        {
                            player.Push(table.Pop());
                        }
                    }
                    else
                    {
                        table.Push(card);
                    }
                    isXiaoHengTurn = !isXiaoHengTurn;
                }
                System.Console.WriteLine("{0} wins", xiaoHa.Empty ? "xiaoHeng" : "xiaoHa");
                return;
            }
            {// stack handles HUIWEN
                System.Console.WriteLine(IsPalindrome("asdfghjkllkjhgfdsa"));
                System.Console.WriteLine(IsPalindrome("11"));
                System.Console.WriteLine(IsPalindrome("1"));
                System.Console.WriteLine(IsPalindrome(""));
                return;
            }
            {
                float a = 0;
                for (int i = 0; i < 10; i++)
                {
                    a += 0.1f;
                }
                System.Console.WriteLine(a == 1f);
                System.Console.WriteLine(a == 1f);
                return;
            }
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
