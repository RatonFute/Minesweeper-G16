using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CellsGeneration : MonoBehaviour
{
    private Game game;
    private void Awake()
    {
        game = GetComponentInParent<Game>();
    }
    public void GenerateCelles()
    {
        for (int x = 0; x < game.Width; x++)
        {
            for (int y = 0; y < game.Height; y++)
            {
                Cell cell = new Cell();
                cell.position = new Vector3Int(x, y, 0);
                cell.type = Cell.Type.Empty;
                game.state[x, y] = cell;
            }
        }
    }

    public void GenerateMines()
    {
        for (int i = 0; i < game.MineCount; i++)
        {
            int x = UnityEngine.Random.Range(0, game.Width);
            int y = UnityEngine.Random.Range(0, game.Height);

            while (game.state[x, y].type == Cell.Type.Mine)
            {
                x++;
                if (x >= game.Width)
                {
                    x = 0; y++;
                    if (y >= game.Height)
                    {
                        y = 0;
                    }
                }
            }
            if (!(x == game.MousePositionOnGameBoard().x && y == game.MousePositionOnGameBoard().y))
            {
                game.state[x, y].type = Cell.Type.Mine;
            }
        }
    }

    public void GenerateNumber()
    {
        for (int x = 0; x < game.Width; x++)
        {
            for (int y = 0; y < game.Height; y++)
            {
                Cell cell = game.state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    continue;
                }

                cell.number = CountMines(x, y);

                if (cell.number > 0)
                {
                    cell.type = Cell.Type.Number;
                }

                game.state[x, y] = cell;
            }

        }

    }
    public int CountMines(int cellX, int cellY)
    {
        int count = 0;

        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0)
                {
                    continue;
                }

                int x = cellX + adjacentX;
                int y = cellY + adjacentY;



                if (game.GetCell(x, y).type == Cell.Type.Mine)
                {
                    count++;
                }
            }
        }

        return count;
    }

}
