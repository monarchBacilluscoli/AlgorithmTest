// 第一章的简单排序算法实现
using System.Collections;
using System;

namespace sort
{
    // 冒泡和快速排序算法的实现
    public static class Sort
    {
        /// <summary>
        /// A more friendly quick sort function
        /// </summary>
        /// <param name="nums">The array to be sorted</param>
        /// <typeparam name="T">item type</typeparam>
        static public void QuickSort<T>(T[] nums) where T : IComparable<T> //! 可以包装一下成为一个友好的接口。
        {
            QuickSort<T>(nums, 0, nums.Length);
        }
        /// <summary>
        /// A not so friendly quick sort function
        /// </summary>
        /// <param name="nums">The array to be sorted</param>
        /// <param name="start">The start position of sorting</param>
        /// <param name="end">The end position of sorting</param>
        /// <typeparam name="T">item type</typeparam>
        static public void QuickSort<T>(T[] nums, int start, int end) where T : IComparable<T>
        {
            if (start < 0)
            {
                throw new ArgumentException("start must greater than 0");
            }
            if (end > nums.Length)
            {
                throw new ArgumentException("end must less than length of nums");
            }
            if (end <= start)
            {
                return;
            }
            T target = nums[start];
            int i = start, j = end - 1;
            while (i < j)
            {
                while (nums[j].CompareTo(target) >= 0 && j > i) // 先check j，因为j初始脚下是啥不知道。//! 等于的情况下也进行移动可以少交换一点吧，但是要注意一定要check临界
                {
                    j--;
                }
                while (nums[i].CompareTo(target) <= 0 && i < j)
                {
                    i++;
                }
                T temp2 = nums[j];
                nums[j] = nums[i];
                nums[i] = temp2;
            }
            //! 别忘了将i位置的对象和target交换。
            T temp = nums[i];
            nums[i] = nums[start];
            nums[start] = temp;
            QuickSort(nums, start, i);
            QuickSort(nums, j + 1, end);
        }

        /// <summary>
        /// An implementation of bubble sort
        /// </summary>
        /// <param name="nums">array to be sorted</param>
        /// <typeparam name="T">item type</typeparam>
        public static void BubbleSort<T>(T[] nums) where T : IComparable<T>
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = 0; j < nums.Length - i - 1; j++) // !注意极限条件不是-2
                {
                    // 如果后面的数小于前面的数，那么就交换
                    if (nums[j].CompareTo(nums[j + 1]) > 0)
                    {
                        T temp = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = temp;
                    }
                }
            }
        }
    }
}