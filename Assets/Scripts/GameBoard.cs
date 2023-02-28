
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour
{
    public Tilemap tilemapClassic { get; private set; }
    [SerializeField] Tile tile_Unknown;
    [SerializeField] Tile tile_Empty;
    [SerializeField] Tile tile_Mine;
    [SerializeField] Tile tile_Exploded;
    [SerializeField] Tile tile_Flag;
    [SerializeField] Tile tile1;
    [SerializeField] Tile tile2;
    [SerializeField] Tile tile3;
    [SerializeField] Tile tile4;
    [SerializeField] Tile tile5;
    [SerializeField] Tile tile6;
    [SerializeField] Tile tile7;
    [SerializeField] Tile tile8;

    public Tilemap tilemapManuscrit { get; private set; }
    [SerializeField] Tile tileManUnknown;
    [SerializeField] Tile tileManEmpty;
    [SerializeField] Tile tileManMine;
    [SerializeField] Tile tileManExploded;
    [SerializeField] Tile tileManFlag;
    [SerializeField] Tile tileMan1;
    [SerializeField] Tile tileMan2;
    [SerializeField] Tile tileMan3;
    [SerializeField] Tile tileMan4;
    [SerializeField] Tile tileMan5;
    [SerializeField] Tile tileMan6;
    [SerializeField] Tile tileMan7;
    [SerializeField] Tile tileMan8;

    public Tilemap tilemap { get; private set; }
    Tile tileUnknown;
    Tile tileEmpty;
    Tile tileMine;
    Tile tileExploded;
    Tile tileFlag;
    Tile tileNum1;
    Tile tileNum2;
    Tile tileNum3;
    Tile tileNum4;
    Tile tileNum5;
    Tile tileNum6;
    Tile tileNum7;
    Tile tileNum8;
    
    private void Awake()
    {
        tilemapManuscrit = GetComponent<Tilemap>();
        tilemapClassic = GetComponent<Tilemap>();
        tilemap = tilemapClassic;
        tileUnknown = tile_Unknown;
        tileEmpty = tile_Empty;
        tileMine = tile_Mine;
        tileExploded = tile_Exploded;
        tileFlag = tile_Flag;
        tileNum1 = tile1;
        tileNum2 = tile2;
        tileNum3 = tile3;
        tileNum4 = tile4;
        tileNum5 = tile5;
        tileNum6 = tile6;
        tileNum7 = tile7;
        tileNum8 = tile8;

    }

    public void Draw(int _width,int _height, Cell[,] state)
    {
        
        int width = _width;
        int height = _height;
        tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                
                Cell cell = state[x, y];
                tilemap.SetTile(cell.position, GetTile(cell));
            }   
        }
    }


    private Tile GetTile(Cell cell)
    {
        if (cell.revealed)
        {
            return GetReavealedTile(cell);
        }
        else if (cell.flagged)
        {
            return tileFlag;
        }
        else
        {
            return tileUnknown;
        }
    }


    private Tile GetReavealedTile(Cell cell)
    {
        switch (cell.type)
        {
            case Cell.Type.Empty: return tileEmpty;
            case Cell.Type.Mine: return cell.exploded ? tileExploded : tileMine;
            case Cell.Type.Number: return GetNumberTile(cell);
            default: return null;
            
        }
    }

    private Tile GetNumberTile(Cell cell)
    {
        switch (cell.number)
        {
            case 1: return tileNum1;
            case 2: return tileNum2;
            case 3: return tileNum3;
            case 4: return tileNum4;
            case 5: return tileNum5;
            case 6: return tileNum6;
            case 7: return tileNum7;
            case 8: return tileNum8;
            default: return null;
        }
    }

}
