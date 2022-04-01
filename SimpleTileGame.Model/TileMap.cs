using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleTileGame.Model
{
    public class TileMap
    {
        private static readonly Random rnd = new();
        private readonly Tile[,] tiles;
        private static readonly List<TerrainType> allTerrainTypes = Enum.GetValues(typeof(TerrainType)).Cast<TerrainType>().ToList();

        public TileMap(int columns, int rows)
        {
            tiles = new Tile[columns, rows];
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    tiles[x, y] = new Tile(x, y, GetRandomTerrain());
                }
            }
        }

        public int Columns
        {
            get { return tiles.GetLength(0); }
        }

        public int Rows
        {
            get { return tiles.GetLength(1); }

        }

        public Tile GetTile(int column, int row)
        {
            return tiles[column, row];
        }

        private TerrainType GetRandomTerrain()
        {
            return allTerrainTypes[rnd.Next(allTerrainTypes.Count)];
        }
    }
}