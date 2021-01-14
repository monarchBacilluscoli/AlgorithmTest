using System.Collections.Generic;
using System;
using System.Linq;

/// <summary>
/// The 5th chapter of 哦吼算法
/// </summary>
namespace GraphTraverse
{
    public static class AlgoForGraph
    {
        /// <summary>
        /// Traverse the graph presented in matrix from start
        /// </summary>
        /// <param name="matrix">Adjacency matrix to represent the graph</param>
        /// <param name="start">Start point to traverse</param>
        /// <param name="traverser">Traversing algorithm</param>
        public static void Traverse(int[,] matrix, int start, Func<int[,], int, LinkedList<int>> traverser)
        {
            if (start > matrix.GetLength(0) || start < 0)
            {
                throw new ArgumentOutOfRangeException("start must be whthin [0,matrix.GetLength(0))");
            }
            LinkedList<int> path = traverser(matrix, start);
            foreach (var item in path)
            {
                System.Console.Write("{0}\t", item);
                System.Console.WriteLine();
            }
        }
        public static LinkedList<int> DFSTraverse(int[,] matrix, int start)
        {
            LinkedList<Int32> path = new LinkedList<int>();
            int[] book = new int[matrix.GetLength(0)];
            DFSTraverse_(matrix, start, path, book);
            return path;
        }
        /// <summary>
        /// Traverse the graph represented in matrix from start, Implementation.
        /// </summary>
        /// <param name="matrix">Adjacency matrix</param>
        /// <param name="start">Start point</param>
        private static void DFSTraverse_(int[,] matrix, int start, LinkedList<int> path, int[] book)
        {
            path.AddLast(start);
            book[start] = 1;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[start, i] >= 0 && book[i] != 1)
                {
                    DFSTraverse_(matrix, i, path, book);
                }
            }
            return;
        }
        /// <summary>
        /// Find shortest path on a graph presented by adjacency matrix
        /// </summary>
        /// <param name="matrix">The adjacency matrix to represent the graph</param>
        /// <param name="start">The start point of the path</param>
        /// <param name="target">The target point of the path</param>
        /// <param name="pathFinder">The path finding algorithm</param>
        public static void FindShortestPath(int[,] matrix, int start, int target, Func<int[,], int, int, LinkedList<int>> pathFinder)
        {
            if (start > matrix.GetLength(0) || start < 0 || target < 0 || target > matrix.GetLength(0))
            {
                throw new ArgumentOutOfRangeException("start and end must be whthin [0,matrix.GetLength(0))");
            }
            LinkedList<int> path = pathFinder(matrix, start, target);
            foreach (var item in path)
            {
                System.Console.Write("{0}\t", item);
            }
            return;
        }
        /// <summary>
        /// It may be a dijkstra algorithm's implementation, since I think the only one difference of the BFS and dijkstra is the unfolding operation.
        /// </summary>
        /// <param name="matrix">The adjacency matrix to represent the graph</param>
        /// <param name="start">The start point of the path</param>
        /// <param name="target">The target point of the path</param>
        /// <returns></returns>
        public static LinkedList<int> BFSFindShortestPath(int[,] matrix, int start, int target)
        {
            LinkedList<Int32> path = new LinkedList<int>();
            //todo 广度优先需要更新到这个节点的最短距离。
            //todo 还需要记录每一个节点的前驱节点。
            List<Tuple<int, int, int>> unhandledPoints = new List<Tuple<int, int, int>>(); // point, distance, pre
            List<Tuple<int, int, int>> handledPoints = new List<Tuple<int, int, int>>(); // point, distance, pre
            // Tuple<int, int>[] unhandledPoints1 = new Tuple<int, int>[matrix.GetLength(0)]; // point, distance, pre
            // Tuple<int, int>[] hadledPoints1 = new Tuple<int, int>[matrix.GetLength(0)]; // point, distance, pre

            unhandledPoints.Add(new Tuple<int, int, int>(start, 0, -1));
            while (unhandledPoints.Count != 0) //todo 找到unhandled之中不为-1的最小的值
            {
                Tuple<int, int, int> minDisPoint = unhandledPoints.First();
                foreach (var item in unhandledPoints)
                {
                    if (minDisPoint.Item2 > item.Item2)
                    {
                        minDisPoint = item;
                    }
                }
                if (minDisPoint.Item1 == target)
                {
                    Tuple<int, int, int> temp = minDisPoint;
                    while (temp.Item3 != -1) // if the pre point is not set to -1
                    {
                        path.AddFirst(temp.Item1);
                        temp = handledPoints.Find((Tuple<int, int, int> item) => { return item.Item1 == temp.Item3; });
                    }
                    path.AddFirst(start);
                    return path;
                }
                unhandledPoints.Remove(minDisPoint);
                handledPoints.Add(minDisPoint);
                int ind = minDisPoint.Item1;
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    Tuple<int, int, int> nextPoint = new Tuple<int, int, int>(matrix[ind, i], 0, 0);
                    if (unhandledPoints.Exists((Tuple<int, int, int> item) => { return item.Item1 == nextPoint.Item1; }))
                    {
                        var t = unhandledPoints.Find((Tuple<int, int, int> item) => { return item.Item1 == nextPoint.Item1; });
                        unhandledPoints.Remove(t);
                        unhandledPoints.Add(new Tuple<int, int, int>(i, minDisPoint.Item2 + matrix[ind, i], ind));
                    }
                    else
                    {
                        if (matrix[ind, i] != 0 && matrix[ind, i] != -1)
                        {
                            unhandledPoints.Add(new Tuple<int, int, int>(i, minDisPoint.Item2 + matrix[ind, i], ind));
                        }
                    }
                }
            }

            return new LinkedList<int>();
        }
    }
}