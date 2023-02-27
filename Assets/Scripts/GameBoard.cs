
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    [SerializeField] Tile tileUnknown;
    [SerializeField] Tile tileEmpty;
    [SerializeField] Tile tileMine;
    [SerializeField] Tile tileExploded;
    [SerializeField] Tile tileFlag;
    [SerializeField] Tile tileNum1;
    [SerializeField] Tile tileNum2;
    [SerializeField] Tile tileNum3;
    [SerializeField] Tile tileNum4;
    [SerializeField] Tile tileNum5;
    [SerializeField] Tile tileNum6;
    [SerializeField] Tile tileNum7;
    [SerializeField] Tile tileNum8;
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();

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
