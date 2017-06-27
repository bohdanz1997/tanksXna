using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml;
using Tanks.Enums;

namespace Tanks.Managers
{
    class MapManager
    {
        public static MapBlock[] ReadMapLayer(string levelName, string layerName, Vector2 tileSize, out Vector2 mapSize)
        {
            var reader = new XmlTextReader(levelName);
            var objects = new List<MapBlock>();
            mapSize = new Vector2();
            int i = 0;
            bool canReadLayer = false;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "layer")
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.Name == "name" && reader.Value == layerName)
                                {
                                    canReadLayer = true;
                                }
                                if (reader.Name == "width")
                                    mapSize.X = Convert.ToInt32(reader.Value);
                                else if (reader.Name == "height")
                                    mapSize.Y = Convert.ToInt32(reader.Value);
                            }
                        }
                        if (reader.Name == "tile" && canReadLayer)
                        {
                            reader.MoveToNextAttribute();
                            if (reader.Name == "gid")
                            {
                                var value = Convert.ToInt32(reader.Value);
                                if (value > 0)
                                {
                                    var type = CellType.None;
                                    switch (value)
                                    {
                                        case 1: type = CellType.GreenTank; break;
                                        case 2: type = CellType.RedTank; break;
                                        case 3: type = CellType.GreenSpawner; break;
                                        case 4: type = CellType.RedSpawner; break;
                                        case 5: type = CellType.Tower; break;

                                        case 7: type = CellType.Wall; break;
                                        case 8: type = CellType.SteelWall; break;
                                        case 9: type = CellType.TitanWall; break;
                                        case 10: type = CellType.ArmorWall; break;
                                        case 11: type = CellType.Tree; break;
                                        case 12: type = CellType.Obstacle; break;                                      
                                    }

                                    if (value > 24)
                                    {
                                        type = CellType.Ground;
                                    }

                                    int x = (int)(i % mapSize.X);
                                    int y = (int)(i / mapSize.X);

                                    objects.Add(new MapBlock
                                    {
                                        Type = type,
                                        X = x * tileSize.X,
                                        Y = y * tileSize.Y,
                                        Position = new Vector2(x, y),
                                        GID = value - 1
                                    });
                                }
                            }
                            i++;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "layer" && canReadLayer)
                        {
                            canReadLayer = false;
                        }
                        break;
                }
            }
            return objects.ToArray();
        }
    }

    public struct MapBlock
    {
        public Vector2 Position { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int GID { get; set; }
        public int Type { get; set; }
    }
}
