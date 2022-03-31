namespace SimpleTileGame.Model
{
    public class Tile
    {
        public Tile(int row, int column, TerrainType terrain)
        {
            Row = row;
            Column = column;
            Terrain = terrain;
        }

        public int Row { get; set; }
        public int Column { get; set; }
        public TerrainType Terrain { get; set; }
    }
}