using System;
using System.Collections.Generic;
using QueueStackLinkedList;

namespace search
{
    /// <summary>
    /// A map respresented by grids (unfinished)
    /// </summary>
    internal class GridMap
    {
        protected int[,] m_mapArray;
        protected Point start;
        protected Point end;
        public GridMap(int[,] array, Point start, Point end)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// A point struct
    /// </summary>
    public struct Point
    {
        public Point(int rx, int ry)
        {
            x = rx;
            y = ry;
        }
        public int x, y;
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }
        public static Point operator -(Point a)
        {
            return new Point(-a.x, -a.y);
        }
        public static Point operator -(Point a, Point b)
        {
            return a + (-b);
        }
        public static bool operator ==(Point a, Point b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
        public override string ToString()
        {
            return String.Format("({0}, {1})", x, y);
        }
    }
    /// <summary>
    /// A static class contains some search algorithms and problems
    /// </summary>
    public static class Search
    {
        /// <summary>
        /// The supported directions array
        /// </summary>
        /// <value>4 direction based on the 2D array index</value>
        static Point[] s_directions = new Point[]{
            new Point(0,1), // right 
            new Point(1,0), // down
            new Point(0,-1),// left
            new Point(-1, 0)// up
            };

        /// <summary>
        /// Find the path of a map, you must send your own method into this
        /// </summary>
        /// <param name="map">The map on which to find the way</param>
        /// <param name="start">The start position</param>
        /// <param name="target">The target position</param>
        /// <param name="pathFinder">The path finding algoritm</param>
        public static void FindPath(int[,] map, Point start, Point target, Func<int[,], Point, Point, LinkedList<Point>> pathFinder)
        {
            // var path = DeepFirstSearch(map, start, target);
            var path = pathFinder(map, start, target);
            foreach (var item in path)
            {
                Console.Write("{0}\t", item);
            }
            System.Console.WriteLine();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    switch (map[i, j])
                    {
                        case 1:
                            {
                                System.Console.Write("* ");
                                break;
                            }
                        case 0:
                            {
                                if (path.Contains(new Point(i, j)))
                                {
                                    System.Console.Write(". ");
                                }
                                else
                                {
                                    System.Console.Write("  ");
                                }
                                break;
                            }
                        default:
                            throw new ApplicationException("not supported code in map");
                    }
                }
                System.Console.Write("\n");
            }
        }
        /// <summary>
        /// Find the shortest way by DFS
        /// </summary>
        /// <param name="map">The map on which to find the way</param>
        /// <param name="start">The start position</param>
        /// <param name="target">The target position</param>
        public static LinkedList<Point> DeepFirstSearch(int[,] map, Point start, Point target)
        {
            LinkedList<Point> path = new LinkedList<Point>();
            LinkedList<Point> shortestPath = new LinkedList<Point>();
            int shortestLength = DeepFirstSearch_(map, start, target, path, Int32.MaxValue, ref shortestPath);
            return shortestPath;
        }
        /// <summary>
        /// Find the shortest way by DFS
        /// </summary>
        /// <param name="map">The map on which to find the way</param>
        /// <param name="start">The start position</param>
        /// <param name="target">The target position</param>
        /// <param name="path">The current path</param>
        /// <param name="shortestLength">The current shortest path length. not using the shortedPath.Count since it is 0 at first, but I need it to be MAX</param>
        /// <param name="shortestPath">The current shortest path, passed between recursion called function to note the shortest way</param>
        /// <returns></returns>
        public static Int32 DeepFirstSearch_(int[,] map, Point start, Point target, LinkedList<Point> path, Int32 shortestLength, ref LinkedList<Point> shortestPath)
        {
            path.AddLast(start);
            map[start.x, start.y] = -1;
            if (start == target)
            {
                if (shortestLength > path.Count)
                {
                    shortestLength = path.Count;
                    shortestPath = new LinkedList<Point>(path);  // value type can use this
                }
            }
            else
            {
                for (int i = 0; i < s_directions.Length; i++) //! maybe the sequence can be modified by the relative positions of the start and target
                {
                    Point next = start + s_directions[i];
                    if (next.x >= 0 && next.y >= 0 && next.x < map.GetLength(0) && next.y < map.GetLength(1) && map[next.x, next.y] == 0) // 没走过(-1), 没障碍(1)，可以走(0)
                    {
                        Int32 newLength = DeepFirstSearch_(map, next, target, path, shortestLength, ref shortestPath);
                        if (newLength < shortestLength)
                        {
                            shortestLength = newLength;
                        }
                    }
                }
            }
            path.RemoveLast();
            map[start.x, start.y] = 0;
            return shortestLength;
        }//todo 可以用栈来避免递归，不过可能需要另一个栈辅助，用来记录是第几个方向。或者直接记录方向好了。

        /// <summary>
        /// Find the shortest way by BFS
        /// </summary>
        /// <param name="map">The map on which to find the way</param>
        /// <param name="start">The start position</param>
        /// <param name="target">The target position</param>
        public static LinkedList<Point> BreadthFirstSearch(Int32[,] map, Point start, Point target)
        {
            LinkedList<Point> path = new LinkedList<Point>();
            Nullable<Point>[,] pre = new Nullable<Point>[map.GetLength(0), map.GetLength(1)];
            MyQueue<Point> queue = new MyQueue<Point>(2501);
            queue.Push(start);
            while (!queue.Empty)
            {
                Point currentUnfoldPoint = queue.Pop();
                for (int i = 0; i < s_directions.Length; i++)
                {
                    Point next = currentUnfoldPoint + s_directions[i];

                    if (next.x >= 0 && next.y >= 0 && next.x < map.GetLength(0) && next.y < map.GetLength(1) && map[next.x, next.y] == 0)
                    {
                        if (pre[next.x, next.y] == null)
                        {
                            if (next == target)
                            {
                                path.AddFirst(target);
                                Point temp = currentUnfoldPoint;
                                while (temp != start)
                                {
                                    path.AddFirst(temp);
                                    temp = (Point)pre[temp.x, temp.y];
                                }
                                path.AddFirst(start);
                                return path;
                            }
                            queue.Push(next);
                            pre[next.x, next.y] = currentUnfoldPoint;
                        }
                    }
                }
            }
            return null;

        }
    }
}