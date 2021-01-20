using System;
using System.Collections.Generic;
using search;
using System.Linq;

namespace FindShortestPath
{
    /// <summary>
    /// 哦吼算法的最短路径章节，这部分算法无论邻接矩阵还是边uvw表示都假设最小index是从0开始。
    /// </summary>
    public static class PathFinder
    {
        /// <summary>
        /// The A* algorithm
        /// </summary>
        /// <param name="map">The map on which we find a path</param>
        /// <param name="start">The start point</param>
        /// <param name="target">The target point</param>
        /// <returns></returns>
        public static LinkedList<Point> AStar(int[,] map, Point start, Point target)
        {
            var path = new LinkedList<Point>();
            if (start == target)
            {
                path.AddLast(start);
                return path;
            }
            Point[,] prePoint = new Point[map.GetLength(0), map.GetLength(1)]; // the value is the order of the 4 directions: d,s,a,w
            int[,] g = new int[map.GetLength(0), map.GetLength(1)]; // from staring point
            int[,] h = new int[map.GetLength(0), map.GetLength(1)]; // to target (approximate distance)
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    g[i, j] = int.MaxValue / 2;
                    h[i, j] = int.MaxValue / 2;
                }
            }
            LinkedList<Point> open = new LinkedList<Point>();
            bool[,] close = new bool[map.GetLength(0), map.GetLength(1)]; // used to check if the 

            Func<Point, int> GetFValue = (Point p) => { return g[p.x, p.y] + h[p.x, p.y]; };
            Func<LinkedListNode<Point>> GetMinFPoint = () =>
            {
                LinkedListNode<Point> selectedPoint = open.First;
                int minF = g[selectedPoint.Value.x, selectedPoint.Value.y] + h[selectedPoint.Value.x, selectedPoint.Value.y];
                LinkedListNode<Point> current = selectedPoint.Next;
                while (current != null)
                {
                    if (GetFValue(current.Value) < minF)
                    {
                        minF = GetFValue(current.Value);
                        selectedPoint = current;
                    }
                    current = current.Next;
                }
                return selectedPoint;
            };
            Func<Point, bool> isPointInMap = (Point p) => { return p.x >= 0 && p.x < map.GetLength(0) && p.y >= 0 && p.y < map.GetLength(1); };

            open.AddLast(start);
            g[start.x, start.y] = 0;
            h[start.x, start.y] = Point.GetManhattanDis(start, target);
            prePoint[start.x, start.y] = start;
            while (open.Count != 0)
            {
                LinkedListNode<Point> pn = GetMinFPoint();
                Point p = pn.Value;
                if (pn.Value == target)
                {
                    // LinkedList<Point> path = new LinkedList<Point>();
                    while (p != start)
                    {
                        path.AddFirst(p);
                        p = prePoint[p.x, p.y];
                    }
                    path.AddFirst(start);
                    return path;
                }
                close[p.x, p.y] = true;
                open.Remove(pn);
                for (int i = 0; i < Search.s_directions.Length; i++)
                {
                    Point next = p + Search.s_directions[i];
                    if (isPointInMap(next) && map[next.x, next.y] != 1 && !close[next.x, next.y])
                    {
                        if (GetFValue(next) > GetFValue(p) + 1)
                        {
                            prePoint[next.x, next.y] = p;
                            g[next.x, next.y] = g[p.x, p.y] + 1;
                        }
                        if (!open.Contains(next))
                        {
                            h[next.x, next.y] = Point.GetManhattanDis(next, target);
                            open.AddLast(next);
                        }
                    }
                }
            }
            return new LinkedList<Point>(); // return empty path, which means there is no way out
        }
        public static int[] BellmanFord(int[,] vw, int start)
        {
            int pointCount = GetThePointCountFromVW(vw);
            int[] minDisFromStart = new int[pointCount + 1];
            for (int i = 0; i < vw.GetLength(1) - 1; i++)
            {
                for (int j = 0; j < pointCount; j++)
                {
                    minDisFromStart[vw[i, 1]] = Math.Min(minDisFromStart[vw[i, 0]] + vw[i, 2], minDisFromStart[vw[i, 1]]);
                }
            }
            return minDisFromStart;
        }
        private static int GetThePointCountFromVW(int[,] vw)
        {
            if (vw == null)
            {
                throw new ArgumentNullException("The graph can not be null");
            }
            if (vw.Length == 0)
            {
                throw new ArgumentException("There is no vertex in this graph");
            }
            int maxIndex = 0;
            for (int i = 0; i < vw.GetLength(0); i++)
            {
                maxIndex = Math.Max(maxIndex, Math.Max(vw[i, 0], vw[i, 1]));
            }
            return maxIndex;
        }
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