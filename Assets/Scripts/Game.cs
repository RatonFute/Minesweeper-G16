using UnityEngine;

public class Game : MonoBehaviour
{
    public int width;
    public int height;
    public int mineCount;

    private enum Difficulty
    {
        easy,
        medium,
        hard,
    }
    private Difficulty difficulty;

    private GameBoard gameBoard = new GameBoard();
    private Cell[,] state;

    private bool gameOver;

    public TMPro.TMP_Dropdown dropdown;
    public UnityEngine.UI.Text timerText;
    float timePass = 0;

    private void Awake()
    {
        gameBoard = GetComponentInChildren<GameBoard>();
    }

  
    public void DropDownSelectDifficulty()
    {
        switch (dropdown.value)
        {
            case 0: difficulty = Game.Difficulty.easy; Debug.Log("easy"); break;
            case 1: difficulty = Game.Difficulty.medium; Debug.Log("medium"); break;
            case 2: difficulty = Game.Difficulty.hard; Debug.Log("hard"); break;
        }
    }
    public void setDifficulty()
    {
        //get dropdown option for the switch

        switch (difficulty)
        {
            case Game.Difficulty.easy: width = 8; height = 8; mineCount = 10; break;
            case Game.Difficulty.medium: width = 11; height = 11; mineCount = 25; break;
            case Game.Difficulty.hard: width = 16; height = 16; mineCount = 40; break; 

        }
    }
    public void NewGame()
    {
        setDifficulty(); 
        state = new Cell[width,height];
        gameOver = false;

        GenerateCelles();
        GenerateMines();
        GenerateNumber();
        Camera.main.orthographicSize = 10;
        Camera.main.transform.position = new Vector3(width/2,height/2,-10);

        gameBoard.Draw(width,height,state);
    }

    private void GenerateCelles()
    {
        for(int x =0; x<width; x++)
        {
            for(int y=0;y<height;y++) 
            {
                Cell cell = new Cell();
                cell.position = new Vector3Int(x, y, 0);
                cell.type = Cell.Type.Empty;
                state[x,y] = cell;
            }
        }
    }

    private void GenerateMines()
    {
        for (int i = 0; i < mineCount; i++)
        {
            int x = UnityEngine.Random.Range(0, width);
            int y = UnityEngine.Random.Range(0, height);

            while (state[x, y].type == Cell.Type.Mine)
            {
                x++;
                if (x >= width)
                {
                    x = 0; y++;
                    if (y >= height)
                    {
                        y = 0;
                    }
                }
            }
            state[x, y].type = Cell.Type.Mine;
        }
    }

    private void GenerateNumber()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    continue;
                }

                cell.number = CountMines(x, y);

                if(cell.number > 0)
                {
                    cell.type = Cell.Type.Number;
                }

                state[x, y] = cell;
            }

        }

    }
    private int CountMines(int cellX, int cellY)
    {
        int count = 0;

        for(int adjacentX = -1; adjacentX <=1; adjacentX++)
        {
            for(int adjacentY = -1; adjacentY <=1; adjacentY++)
            {
                if(adjacentX == 0 && adjacentY == 0)
                {
                    continue;
                }

                int x = cellX + adjacentX;
                int y = cellY + adjacentY;

                

                if (GetCell(x,y).type == Cell.Type.Mine)
                {
                    count++;
                }
            }
        }

        return count;
    }


    private void Update()
    {

        

        if (Input.GetKeyDown(KeyCode.R))
        {
            NewGame();
        }else if (!gameOver)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Flag();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Reveal();
            }
            timePass += Time.deltaTime;
            int tempTime = (int)timePass;
            timerText.text = "Time : " + tempTime.ToString();
        }

        
    }


    private void Flag()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = gameBoard.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        if(cell.type == Cell.Type.Invalid)
        {
            return;
        }
        cell.flagged = !cell.flagged;
        state[cellPosition.x, cellPosition.y] = cell;
        gameBoard.Draw(width, height, state);

    }


    private void Reveal()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = gameBoard.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        if( cell.type == Cell.Type.Invalid || cell.revealed || cell.flagged)
        {
            return;
        }

        switch (cell.type)
        {
            case Cell.Type.Mine:
                Explode(cell);
                break;
            case Cell.Type.Empty:
                Flood(cell);
                CheckWinCondition();
                break;
            default:
                cell.revealed = true;
                state[cellPosition.x, cellPosition.y] = cell;
                CheckWinCondition();
                break;
        }

       

        
        gameBoard.Draw(width, height, state);
    }

    private void Flood(Cell cell)
    {
        if(cell.revealed) return;
        if(cell.type == Cell.Type.Mine || cell.type == Cell.Type.Invalid) return;


        cell.revealed = true;
        state[cell.position.x, cell.position.y] = cell;

        if(cell.type == Cell.Type.Empty) 
        { 
            Flood(GetCell(cell.position.x-1,cell.position.y)); 
            Flood(GetCell(cell.position.x+1,cell.position.y)); 
            Flood(GetCell(cell.position.x,cell.position.y-1)); 
            Flood(GetCell(cell.position.x,cell.position.y+1)); 
        }
    }

    private void Explode(Cell cell)
    {
        Debug.Log("game over");
        gameOver = true;

        cell.revealed = true;
        cell.exploded = true;

        state[cell.position.x, cell.position.y] = cell;

        for(int x =0; x< width; x++)
        {
            for(int y =0; y< height; y++)
            {
                cell = state[x,y];

                if(cell.type == Cell.Type.Mine)
                {
                    cell.revealed = true;
                    state[x,y] = cell;
                }
            }
        }
    }


    private void CheckWinCondition()
    {
        for(int x = 0; x< width;x++)
        {
            for( int y = 0; y< height;y++)
            {
                Cell cell = state[x, y];
                if(cell.type != Cell.Type.Mine && !cell.revealed)
                {
                    return;
                }
            }
        }

        Debug.Log("you won");
        gameOver = true;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    cell.flagged = true;
                    state[x, y] = cell;
                }
            }
        }
    }

    private Cell GetCell(int x, int y)
    {
        if (IsValid(x,y))
        {
            return state[x,y];
        }
        else
        {
            return new Cell();
        }
    }

    private bool IsValid(int x,int y)
    {
        return x>=0 && x<width && y>=0 && y<height;
    }

}
