using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Tanks._Game.GameHelpers;
using Tanks.Enums;
using Tanks.Tools;
using Tanks.Tools.Extensions;

namespace Tanks.Components
{
    class AI
    {
        public EntityType[] typesForAttack;
        public Stack<PathData> path;
        public int visionDistance;
        public int attackDistance;
        public object target;
        public Vector2 nextPos;

        public AI(int visionDistance, int attackDistance, EntityType[] typesForAttack = null)
        {
            this.visionDistance = visionDistance;
            this.attackDistance = attackDistance;
            path = new Stack<PathData>();
            this.typesForAttack = typesForAttack ?? new EntityType[0];
        }
        
        public Stack<PathData> GetPath(Vector2 pos, Vector2 tpos, GameField gameField, int cellSize)
        {
            var map = new PathMap[gameField.Width, gameField.Height];
            for (int i = 0; i < gameField.Width; i++)
            {
                for (int j = 0; j < gameField.Height; j++)
                {
                    map[i, j].type = gameField.GetCell(i, j).Type;
                    map[i, j].StepCoast = 1;
                    map[i, j].cell = new Vector2(i, j);
                }
            }
            var start = pos.Divide(cellSize);
            var finish = tpos.Divide(cellSize);
            var pFinder = new PathFinder(start, finish, map, new[] 
            {
                CellType.Wall,
                CellType.ArmorWall,
                CellType.SteelWall,
                CellType.TitanWall,
                CellType.Obstacle,
                CellType.Tree
            }, gameField.Width, gameField.Height);

            var points = pFinder.GetPath();
            var newPath = new List<PathData>();
            if (points != null)
            {
                var directions = pFinder.GetDirectionsFromPath(points);
                points.RemoveAt(0);
                points.Reverse();
                for (int i = 0; i < points.Count; i++)
                {
                    newPath.Add(new PathData
                    {
                        Direction = directions[i],
                        Point = points[i] * cellSize
                    });
                }
            }
            var a = new Stack<PathData>(newPath);
            return a;
        }

        public Direction GetDirectionToTarget(Vector2 pos, Vector2 tpos)
        {
            var tempDirection = Direction.None;
            if (pos.X == tpos.X && pos.Y < tpos.Y)
                tempDirection = Direction.Down;
            else if (pos.X == tpos.X && pos.Y > tpos.Y)
                tempDirection = Direction.Up;
            else if (pos.Y == tpos.Y && pos.X < tpos.X)
                tempDirection = Direction.Right;
            else if (pos.Y == tpos.Y && pos.X > tpos.X)
                tempDirection = Direction.Left;
            return tempDirection;
        }

        public bool CanAttack(Vector2 pos, Vector2 tpos, GameField gameField, int cellSize)
        {
            var interrupts = new[] { CellType.Wall, CellType.SteelWall, CellType.ArmorWall, CellType.TitanWall };
            var tempDirection = GetDirectionToTarget(pos, tpos);
            if (tempDirection == Direction.None)
                return false;

            var coords = pos.Divide(cellSize);
            var tCoords = tpos.Divide(cellSize);

            var append = tempDirection.ToVector();
            while (coords != tCoords)
            {
                coords += append;
                if (Array.IndexOf(interrupts, gameField.GetCell(coords).Type) >= 0)
                    return false;
            }
            return true;
        }
    }
}
