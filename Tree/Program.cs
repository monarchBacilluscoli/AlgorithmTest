using System;

namespace Tree
{
    static class Program
    {
        delegate TResult SelfApplicable<T, TResult>(SelfApplicable<T, TResult> self, T arg);
        static void Main(string[] args)
        {
            {// 并查集测试
                int[] bandits = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 ,9};
                SelfApplicable<int, int> GetFather = (f, a) =>
                  {
                      if (bandits[a] == a)
                      {
                          return a;
                      }
                      else
                      {
                          bandits[a] = f(f, bandits[a]);
                          return bandits[a];
                      }
                  };
                Action<int, int> Merge = (int a, int b) =>
                {
                    if (GetFather(GetFather, a) != GetFather(GetFather, b))
                    {
                        bandits[GetFather(GetFather, b)] = GetFather(GetFather, a);
                    }
                };
                int[,] clues = new int[,]{
                    {0,1},{2,3},{4,1},{3,5},{1,5},{7,6},{8,6},{0,5},{1,3}
                };
                for (int i = 0; i < clues.GetLength(0); i++) // merge bandits belonging to the same team
                {
                    Merge(clues[i, 0], clues[i, 1]);
                }
                int sum = 0;
                for (int i = 0; i < bandits.Length; i++) // check the team size
                {
                    if (bandits[i] == i) // check if it is the leader of its own team.
                    {
                        ++sum;
                    }
                }
                return;
            }
            {// 二叉堆测试
                BinaryHeap<int> heap = new BinaryHeap<int>(new int[] { 312345, 1125, 15, -1124, 1421 }, false);
                int a = heap.Pop();
                return;
            }
        }
    }
}
