using System;

namespace FindShortestPath
{
    class Program
    {

        static void Main(string[] args)
        {
            const int MAX = Int32.MaxValue / 2;
            {// dijkstra算法测试
                int[,] matrix = {
                    {0,1,12,MAX,MAX,MAX},
                    {MAX,0,9,3,MAX,MAX},
                    {MAX,MAX,0,MAX,5,MAX},
                    {MAX,MAX,4,0,13,15},
                    {MAX,MAX,MAX,MAX,0,4},
                    {MAX,MAX,MAX,MAX,MAX,0}
                };
                int[] minDisFromStart = PathFinder.Dijkstra(matrix, 0);
                foreach (var item in minDisFromStart)
                {
                    System.Console.Write("{0}\t", item);
                    System.Console.WriteLine();
                }
                return;
            }
            {// Floyd 算法测试
                int[,] matrix = {
                    {0,2,6,4},
                    {MAX, 0,3,MAX},
                    {7,MAX,0,1},
                    {5,MAX,12,0}
                };
                int[,] disMatrix = PathFinder.Floyd(matrix);
                for (int i = 0; i < disMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < disMatrix.GetLength(1); j++)
                    {
                        System.Console.Write("{0}\t", disMatrix[i, j]);
                    }
                    System.Console.WriteLine();
                }
                return;
            }
        }
    }
}
