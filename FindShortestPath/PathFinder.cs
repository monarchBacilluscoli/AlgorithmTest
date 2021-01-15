using System;
using System.Collections.Generic;

namespace FindShortestPath
{
    public static class PathFinder
    {
        public static int[,] Floyd(int[,] matrix)
        {
            int[,] shortestDis = matrix;
            for (int i = 0; i < matrix.GetLength(0); i++) // 这个中继节点必须是第一行，否则后面的更新无法利用之前的更新
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    for (int k = 0; k < matrix.GetLength(0); k++)
                    {
                        shortestDis[j, k] = Math.Min(shortestDis[j, i] + shortestDis[i, k], shortestDis[j, k]);
                    }
                }
            }
            return shortestDis;
        }
        public static int[] Dijkstra(int[,] matrix, int start)
        {
            int[] minDisFromStart = new int[matrix.GetLength(1)];
            bool[] isHandled = new bool[matrix.GetLength(1)];
            Array.Fill(isHandled, false);
            for (int i = 0; i < minDisFromStart.Length; i++)
            {
                minDisFromStart[i] = matrix[start, i];
            }
            //todo find the unhandled point which has minimus distance from start
            int minDisUnhandledIndex = start;
            int minDis = 0;
            while (minDisUnhandledIndex != -1)
            {
                minDisFromStart[minDisUnhandledIndex] = minDis;
                isHandled[minDisUnhandledIndex] = true;
                //todo update the points' distance around it
                for (int i = 0; i < minDisFromStart.Length; i++)
                {
                    if (matrix[minDisUnhandledIndex, i] <= Int32.MaxValue && isHandled[i] == false) // there is a way from the current point
                    {
                        minDisFromStart[i] = minDisFromStart[minDisUnhandledIndex] + matrix[minDisUnhandledIndex, i];
                    }
                }
                minDisUnhandledIndex = -1;
                minDis = Int32.MaxValue;
                for (int i = 0; i < minDisFromStart.Length; i++)
                {
                    if (minDis > minDisFromStart[i] && isHandled[i] == false)
                    {
                        minDisUnhandledIndex = i;
                        minDis = minDisFromStart[i];
                    }
                }
            }
            return minDisFromStart;
        }
    }
}