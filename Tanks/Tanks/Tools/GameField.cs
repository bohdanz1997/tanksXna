using Tanks.Enums;
using Microsoft.Xna.Framework;
using Ash.Core;

namespace Tanks.Tools
{
    public struct Cell
    {
        public int Type;
        public Ground Ground;
        public Entity Entity;

        public Cell(int t, Ground p)
        {
            Type = t;
            Ground = p;
            Entity = null;
        }

        public override string ToString()
        {
            return $"Type={Type}, Passability={Ground}";
        }
    }

    public enum Ground
    {
        Ground, Sand, Asphalt
    }

    public class GameField
    {
        public Cell[,] Field { get; set; }
        public int Width
        {
            get { return Field.GetLength(0); }
        }
        public int Height
        {
            get { return Field.GetLength(1); }
        }

        public GameField(int width, int height)
        {
            Field = new Cell[width, height];
            ClearAll();
        }

        public Cell GetCell(float x, float y)
        {
            if (x < 0 || x >= Field.GetLength(0))
                return new Cell(CellType.Wall, Ground.Ground);
            if (y < 0 || y >= Field.GetLength(1))
                return new Cell(CellType.Wall, Ground.Ground);
            return Field[(int)x, (int)y];
        }
        
        public Cell GetCell(Vector2 pos)
        {
            return GetCell(pos.X, pos.Y);
        }
        
        public bool IsCellEmpty(Vector2 pos)
        {
            return GetCell(pos).Type == CellType.None;
        }

        public bool IsCellEmpty(float x, float y)
        {
            return GetCell(x, y).Type == CellType.None;
        }
        
        public void SetCell(float x, float y, int cell)
        {
            SetCell(new Vector2(x, y), cell);
        }

        public void SetCell(Vector2 pos, int cell)
        {
            if (CheckRange(pos.X, pos.Y))
                Field[(int)pos.X, (int)pos.Y].Type = cell;
        }

        public void SetCell(Vector2 pos, Ground pas, Entity e)
        {
            if (CheckRange(pos.X, pos.Y))
            {
                Field[(int)pos.X, (int)pos.Y].Ground = pas;
                Field[(int)pos.X, (int)pos.Y].Entity = e;
            }
        }

        public void ClearCell(Vector2 pos)
        {
            if (CheckRange(pos.X, pos.Y))
                Field[(int)pos.X, (int)pos.Y].Type = CellType.None;
        }

        private bool CheckRange(float x, float y)
        {
            return
                x >= 0 && x < Field.GetLength(0) &&
                y >= 0 && y < Field.GetLength(1);
        }

        public void ClearAll()
        {
            for (int i = 0; i < Field.GetLength(0); i++)
                for (int j = 0; j < Field.GetLength(1); j++)
                    ClearCell(new Vector2(i, j));
        }
    }
}
