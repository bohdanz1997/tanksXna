using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Tanks.Enums;
using Tanks.Tools.Extensions;

namespace Tanks.Tools
{
    class PathFinder
    {
        Vector2 nullPoint = new Vector2(-1, -1);
        List<PathMap> OpenList;
        List<PathMap> CloseList;
        Vector2 start, finish;
        PathMap[,] map;
        public List<Vector2> path;
        private int fieldWidth;
        private int fieldHeight;
        private int[] exceptions;

        public PathFinder() { }
        public PathFinder(Vector2 start, Vector2 finish, PathMap[,] map, int[] exceptions, int fieldWidth, int fieldHeight)
        {
            OpenList = new List<PathMap>(100);
            CloseList = new List<PathMap>(100);
            this.start = start;
            this.finish = finish;
            this.map = map;
            this.fieldWidth = fieldWidth;
            this.fieldHeight = fieldHeight;
            this.exceptions = exceptions;
        }

        private int Heruistic(Vector2 p1, Vector2 p2)
        {
            int dx = Math.Abs((int)p1.X - (int)p2.X);
            int dy = Math.Abs((int)p1.Y - (int)p2.Y);

            return 10 * Math.Abs(dx - dy) + 14 * Math.Min(dx, dy);
        }
        private Vector2 PopMinFromOpen(List<PathMap> openList)
        {
            if (openList.Count > 0)
            {
                int i = 0;
                for (int j = 0; j < openList.Count; j++)
                {
                    if (openList[j].fValue <= openList[i].fValue)
                        i = j;
                }
                return openList[i].cell;
            }
            return nullPoint;
        }

        private bool IsPointInOpenList(Vector2 cell, IEnumerable<PathMap> openList)
        {
            return openList.Any(m => m.cell == cell);
        }
        private bool IsPointInCloseList(Vector2 cell, IEnumerable<PathMap> closeList)
        {
            return closeList.Any(m => m.cell == cell);
        }

        private void RemoveFromOpenList(Vector2 p)
        {
            for (int i = 0; i < OpenList.Count; i++)
                if (OpenList[i].cell.X == p.X && OpenList[i].cell.Y == p.Y)
                    OpenList.Remove(OpenList[i]);
        }
        private void RemoveFromCloseList(Vector2 p)
        {
            for (int i = 0; i < CloseList.Count; i++)
                if (CloseList[i].cell.X == p.X && CloseList[i].cell.Y == p.Y)
                    CloseList.Remove(CloseList[i]);
        }

        private void GetSuccesors(Vector2 cell, out Vector2[] succesors)
        {
            succesors = new[]
            {
                new Vector2(0, -1), new Vector2(1, 0),
                new Vector2(0, 1), new Vector2(-1, 0)
            };
            for (int i = 0; i < 4; i++)
            {
                succesors[i].X += cell.X;
                succesors[i].Y += cell.Y;

                while (succesors[i].X < 0 || succesors[i].Y < 0)
                {
                    if (succesors[i].X < 0)
                        succesors[i].X++;
                    if (succesors[i].Y < 0)
                        succesors[i].Y++;
                }
                while (succesors[i].X >= fieldWidth || succesors[i].Y >= fieldHeight)
                {
                    if (succesors[i].X >= fieldWidth)
                        succesors[i].X--;
                    if (succesors[i].Y >= fieldHeight)
                        succesors[i].Y--;
                }
            }
        }

        private void AddToOpenList(PathMap mPoint)
        {
            OpenList.Add(mPoint);
        }
        private void AddToCloseList(PathMap mPoint)
        {
            CloseList.Add(mPoint);
            RemoveFromOpenList(mPoint.cell);

        }

        public List<Vector2> GetPath()
        {
            int ListCap = Math.Abs((int)start.X - (int)finish.X) + Math.Abs((int)start.Y - (int)finish.Y);

            var p = new Vector2(start.X, start.Y);
            map[(int)p.X, (int)p.Y].cell = p;
            map[(int)p.X, (int)p.Y].gValue = 0;
            map[(int)p.X, (int)p.Y].fValue = Heruistic(start, finish);
            map[(int)p.X, (int)p.Y].parent = nullPoint;

            AddToOpenList(map[(int)p.X, (int)p.Y]);
            while (OpenList.Count != 0)
            {
                //Извлекаем из открытого списка точку с наименьшей общей стоимость прохода до финиша:
                p = PopMinFromOpen(OpenList);
                // Если извлечённая точка - финишная, конструируем путь:
                if (p == finish)
                {
                    //Конструируем путь:
                    path = new List<Vector2>(ListCap) { finish, finish };

                    Vector2 s = map[(int)p.X, (int)p.Y].parent;
                    if (s != nullPoint)
                    {
                        while (s != start)
                        {
                            path.Add(s);
                            s = map[(int)s.X, (int)s.Y].parent;
                        }
                    }

                    path.Add(start);
                    path.Reverse();
                    path.Remove(finish);

                    return path;
                }
                // Исследуем соседей:
                Vector2[] succesors;

                GetSuccesors(p, out succesors);
                for (int i = 0; i < 4; i++)
                {
                    int x = (int)succesors[i].X;
                    int y = (int)succesors[i].Y;
                    // Если препятствие стена - пропускаем точку:
                    if (Array.IndexOf(exceptions, map[x, y].type) != -1)
                        continue;

                    if (IsPointInCloseList(succesors[i], OpenList) || IsPointInOpenList(succesors[i], CloseList))
                        continue;
                    map[x, y].fValue = Heruistic(succesors[i], finish);

                    map[x, y].parent = p;
                    if (IsPointInCloseList(succesors[i], CloseList))
                        RemoveFromCloseList(succesors[i]);

                    if (!IsPointInOpenList(succesors[i], OpenList))
                    {
                        map[x, y].cell = succesors[i];
                        AddToOpenList(map[x, y]);
                    }
                }

                map[(int)p.X, (int)p.Y].cell = p;
                AddToCloseList(map[(int)p.X, (int)p.Y]);
            }
            path = new List<Vector2> { nullPoint };

            return null;
        }

        public List<Direction> GetDirectionsFromPath(List<Vector2> path)
        {
            var dirs = new List<Direction>();
            for (int i = 0; i < path.Count - 1; i++)
            {
                var t = path[i + 1] - path[i];
                dirs.Add(t.ToDirection());
            }
            dirs.Reverse();

            return dirs;
        }
    }

    public struct PathMap
    {
        public Vector2 parent;
        public Vector2 cell;
        public int gValue;
        public int fValue;
        public int StepCoast;
        public int type;
    }
}
