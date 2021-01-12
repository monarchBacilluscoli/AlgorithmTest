using System;
using System.Collections.Generic;

namespace enumeration
{
    class Program
    {
        /// <summary>
        /// Print all permutation of n numbers
        /// </summary>
        /// <param name="n">the count of all numbers</param>
        internal static void PrintAllPermutation(Int32 n)
        {
            if (n == 0)
            {
                return;
            }
            Int32[] nums = new Int32[n], finished = new Int32[n];
            Array.Fill<Int32>(nums, 1);

            PrintAllPermutation_(nums, finished, 0);
            return;
        }
        /// <summary>
        /// Recursive func to get permutation
        /// </summary>
        /// <param name="set">the nums unused</param>
        /// <param name="finished">the finished number part</param>
        /// <param name="n">the finished length</param>
        private static void PrintAllPermutation_(Int32[] set, Int32[] finished, int n)
        {
            if (n == set.Length - 1)
            {
                for (int i = 0; i < set.Length - 1; i++)
                {
                    System.Console.Write(finished[i]);
                }
                for (int i = 0; i < set.Length; i++)
                {
                    if (set[i] == 1)
                    {
                        System.Console.WriteLine("{0} \t", i + 1);
                    }
                }
                return;
            }
            for (int i = 0; i < set.Length; i++)
            {
                if (set[i] == 1)
                {
                    set[i] = 0;
                    finished[n++] = i + 1;
                    PrintAllPermutation_(set, finished, n);
                    set[i] = 1;
                    finished[n--] = 0;
                }
            }
            return;
        }
        static void Main(string[] args)
        {
            {
                PrintAllPermutation(1);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
