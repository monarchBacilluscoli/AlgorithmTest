// 排序算法的实验
using System;

namespace sort
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                int[] array = new Int32[] { 1, 215, 16, 26464, 7 };
                Sort.BubbleSort(array);
                foreach (var item in array)
                {
                    System.Console.WriteLine(item + "\t");
                }
            }
            {
                int[] array = new Int32[] { 1, 215, 16, 26464, 7 };
                Sort.QuickSort(array);
                foreach (var item in array)
                {
                    System.Console.WriteLine(item + "\t");
                }
            }
        }
    }
}
