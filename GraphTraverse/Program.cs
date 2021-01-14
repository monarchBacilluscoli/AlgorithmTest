using System;

namespace GraphTraverse
{
    class Program
    {
        static void Main(string[] args)
        {
            {// 自行实现的dijkstra算法测试。本身是BFS转机次数问题，最后干脆改成dijkstra。
                Int32[,] matrix = {
                    {0,1,12,-1,-1,-1},
                    {-1,0,9,3,-1,-1},
                    {-1,-1,0,-1,5,-1},
                    {-1,-1,4,0,13,15},
                    {-1,-1,-1,-1,0,4},
                    {-1,-1,-1,-1,-1,0}
                };
                foreach (var item in AlgoForGraph.BFSFindShortestPath(matrix, 0, 3)) // untested)
                {
                    System.Console.Write("{0}\t", item);
                }
                System.Console.WriteLine();
                return;
            }
            {// 有向有权矩阵测试
                int[,] matrix = {
                    {0,2,-1,-1,10},
                    {-1,0,3,-1,7},
                    {4,-1,0,4,-1},
                    {-1,-1,-1,0,5},
                    {-1,-1,3,-1,0}
                };
            }
            {// 无向无权矩阵测试
                int[,] matrix = {
                    {0,1,1,-1,1},
                    {1,0,-1,1,-1},
                    {1,-1,0,-1,1},
                    {-1,1,-1,0,-1},
                    {1,-1,1,-1,0}
                };
                AlgoForGraph.Traverse(matrix, 0, AlgoForGraph.DFSTraverse);
                return;
            }
            Console.WriteLine("Hello World!");
        }
    }
}
