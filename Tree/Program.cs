using System;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            {// 最大堆测试
                BinaryHeap<int> heap = new BinaryHeap<int>(new int[] { 3, 1, 15, 1124, 1421 }, false);
                int a = heap.Pop();
                return;
            }
        }
    }
}
