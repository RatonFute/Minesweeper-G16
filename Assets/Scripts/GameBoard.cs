
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{


    [SerializeField] TMPro.TMP_Dropdown dropdown;
    public Tilemap tilemapClassic { get; private set; }
    [SerializeField] Tile tile_Unknow;
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
    [SerializeField] Tile tileManUnknow;
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

    public Tilemap tilemapSimple { get; private set; }
    [SerializeField] Tile tileSimUnknow;
    [SerializeField] Tile tileSimEmpty;
    [SerializeField] Tile tileSimMine;
    [SerializeField] Tile tileSimExploded;
    [SerializeField] Tile tileSimFlag;
    [SerializeField] Tile tileSim1;
    [SerializeField] Tile tileSim2;
    [SerializeField] Tile tileSim3;
    [SerializeField] Tile tileSim4;
    [SerializeField] Tile tileSim5;
    [SerializeField] Tile tileSim6;
    [SerializeField] Tile tileSim7;
    [SerializeField] Tile tileSim8;

    public Tilemap tilemapRTX { get; private set; }
    [SerializeField] Tile tileRTXUnknow;
    [SerializeField] Tile tileRTXEmpty;
    [SerializeField] Tile tileRTXMine;
    [SerializeField] Tile tileRTXExploded;
    [SerializeField] Tile tileRTXFlag;
    [SerializeField] Tile tileRTX1;
    [SerializeField] Tile tileRTX2;
    [SerializeField] Tile tileRTX3;
    [SerializeField] Tile tileRTX4;
    [SerializeField] Tile tileRTX5;
    [SerializeField] Tile tileRTX6;
    [SerializeField] Tile tileRTX7;
    [SerializeField] Tile tileRTX8;

    /*public Tilemap tilemapCustom { get; private set; }
    [SerializeField] Tile tileCustomUnknow;
    [SerializeField] Tile tileCustomEmpty;
    [SerializeField] Tile tileCustomMine;
    [SerializeField] Tile tileCustomExploded;
    [SerializeField] Tile tileCustomFlag;
    [SerializeField] Tile tileCustom1;
    [SerializeField] Tile tileCustom2;
    [SerializeField] Tile tileCustom3;
    [SerializeField] Tile tileCustom4;
    [SerializeField] Tile tileCustom5;
    [SerializeField] Tile tileCustom6;
    [SerializeField] Tile tileCustom7;
    [SerializeField] Tile tileCustom8;*/

    public Tilemap tilemapMine { get; private set; }
    [SerializeField] Tile tileMineUnknow;
    [SerializeField] Tile tileMineEmpty;
    [SerializeField] Tile tileMineMine;
    [SerializeField] Tile tileMineExploded;
    [SerializeField] Tile tileMineFlag;
    [SerializeField] Tile tileMine1;
    [SerializeField] Tile tileMine2;
    [SerializeField] Tile tileMine3;
    [SerializeField] Tile tileMine4;
    [SerializeField] Tile tileMine5;
    [SerializeField] Tile tileMine6;
    [SerializeField] Tile tileMine7;
    [SerializeField] Tile tileMine8;


    public Tilemap tilemap { get; private set; }
    Tile tileUnknow;
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
        tilemapSimple = GetComponent<Tilemap>();
        tilemapRTX = GetComponent<Tilemap>();
        tilemapMine = GetComponent<Tilemap>();
        //tilemapCustom = GetComponent<Tilemap>();

        ChangeSkin();
    }

    public void ChangeSkin()
    {
        switch (dropdown.value)
        {
            case 0:
                tilemap = tilemapClassic;
                tileUnknow = tile_Unknow;
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
                break;
            case 1:
                tilemap = tilemapManuscrit;
                tileUnknow = tileManUnknow;
                tileEmpty = tileManEmpty;
                tileMine = tileManMine;
                tileExploded = tileManExploded;
                tileFlag = tileManFlag;
                tileNum1 = tileMan1;
                tileNum2 = tileMan2;
                tileNum3 = tileMan3;
                tileNum4 = tileMan4;
                tileNum5 = tileMan5;
                tileNum6 = tileMan6;
                tileNum7 = tileMan7;
                tileNum8 = tileMan8;
                break;
            case 2:
                tilemap = tilemapSimple;
                tileUnknow = tileSimUnknow;
                tileEmpty = tileSimEmpty;
                tileMine = tileSimMine;
                tileExploded = tileSimExploded;
                tileFlag = tileSimFlag;
                tileNum1 = tileSim1;
                tileNum2 = tileSim2;
                tileNum3 = tileSim3;
                tileNum4 = tileSim4;
                tileNum5 = tileSim5;
                tileNum6 = tileSim6;
                tileNum7 = tileSim7;
                tileNum8 = tileSim8;
                break;

            case 3:
                tilemap = tilemapRTX;
                tileUnknow = tileRTXUnknow;
                tileEmpty = tileRTXEmpty;
                tileMine = tileRTXMine;
                tileExploded = tileRTXExploded;
                tileFlag = tileRTXFlag;
                tileNum1 = tileRTX1;
                tileNum2 = tileRTX2;
                tileNum3 = tileRTX3;
                tileNum4 = tileRTX4;
                tileNum5 = tileRTX5;
                tileNum6 = tileRTX6;
                tileNum7 = tileRTX7;
                tileNum8 = tileRTX8;
                break;

            case 4:
                tilemap = tilemapMine;
                tileUnknow = tileMineUnknow;
                tileEmpty = tileMineEmpty;
                tileMine = tileMineMine;
                tileExploded = tileMineExploded;
                tileFlag = tileMineFlag;
                tileNum1 = tileMine1;
                tileNum2 = tileMine2;
                tileNum3 = tileMine3;
                tileNum4 = tileMine4;
                tileNum5 = tileMine5;
                tileNum6 = tileMine6;
                tileNum7 = tileMine7;
                tileNum8 = tileMine8;
                break;

            /*case 5:
                tilemap = tilemapCustom;
                tileUnknow = tileCustomUnknow;
                tileEmpty = tileCustomEmpty;
                tileMine = tileCustomMine;
                tileExploded = tileCustomExploded;
                tileFlag = tileCustomFlag;
                tileNum1 = tileCustom1;
                tileNum2 = tileCustom2;
                tileNum3 = tileCustom3;
                tileNum4 = tileCustom4;
                tileNum5 = tileCustom5;
                tileNum6 = tileCustom6;
                tileNum7 = tileCustom7;
                tileNum8 = tileCustom8;
                break;*/
        }
    }

    public void Draw(int _width, int _height, Cell[,] state)
    {

        int width = _width;
        int height = _height;
        tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
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
            return tileUnknow;
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
