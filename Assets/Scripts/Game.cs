using Unity.VisualScripting;
using UnityEngine;


public class Game : MonoBehaviour
{
    int width;
    int height;
    int mineCount;
    float score;
    int tempScore;
    bool firstClick;
    int tempTime;
    int timeLastGame;

    private enum Difficulty
    {
        easy,
        medium,
        hard,
        random,
    }
    private Difficulty difficulty;

    private GameBoard gameBoard = new GameBoard();
    private Cell[,] state;

    private bool gameOver = true;

    [SerializeField] TMPro.TMP_Dropdown dropdown;
    [SerializeField] UnityEngine.UI.Text timerText;
    [SerializeField] UnityEngine.UI.Text winLoseText;
    [SerializeField] UnityEngine.UI.Text scoreText;
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
            case 3: difficulty = Game.Difficulty.random; Debug.Log("Random"); break;
        }
    }
    public void setDifficulty()
    {
        //get dropdown option for the switch

        switch (difficulty)
        {
            case Game.Difficulty.easy: width = 10; height = 10; mineCount = 10; break;
            case Game.Difficulty.medium: width = 12; height = 12; mineCount = 25; break;
            case Game.Difficulty.hard: width = 16; height = 16; mineCount = 40; break; 
            case Game.Difficulty.random:
                width = UnityEngine.Random.Range(3, 20);
                height = UnityEngine.Random.Range(3, 20);
                mineCount = UnityEngine.Random.Range(1, width*height/2);
                break;

        }
    }


    public void NewGame()
    {
        score = 0;
        timePass = 0;
        firstClick = true;
        winLoseText.text = "";
        setDifficulty();
        state = new Cell[width,height];
        gameOver = false;

        GenerateCelles();
        
        //GenerateMines();
        //GenerateNumber();
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
            if(!(x == MousePositionOnGameBoard().x && y == MousePositionOnGameBoard().y))
            {
                state[x, y].type = Cell.Type.Mine;
            }
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

        }
      
        if (!gameOver)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Flag();
            }
            else if (Input.GetMouseButtonDown(0))
            {

                if (firstClick)
                {
                    GenerateMines();
                    GenerateNumber();
                    firstClick = false;
                }
                Reveal();
                
                
            }
            
            timePass += Time.deltaTime;
            tempTime = (int)timePass;
            timerText.text = "Time : " + tempTime.ToString();

            // score decrease every frame. need to decrease score every second only !
            //score -= tempTime;
            //if (score < 0)
            //{
            //    score = 0;
            //}
            //tempScore = (int)score;
            //scoreText.text = "Your score : " + tempScore.ToString();

        }
        else
        {
            timeLastGame = (int)timePass;
            tempTime = timeLastGame;
            timerText.text = "You played " + tempTime.ToString() + "s";
            timePass = 0;
            tempScore= 0;
        }
        
    }


    private void Flag()
    {
        
        Cell cell = GetCell(MousePositionOnGameBoard().x, MousePositionOnGameBoard().y);

        if (cell.type == Cell.Type.Invalid)
        {
            return;
        }
        cell.flagged = !cell.flagged;
        state[MousePositionOnGameBoard().x, MousePositionOnGameBoard().y] = cell;
        gameBoard.Draw(width, height, state);
        FlagThemAll();

    }

    private Vector3Int MousePositionOnGameBoard()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = gameBoard.tilemap.WorldToCell(worldPosition);
        return cellPosition;
    }
    private void Reveal()
    {
        Cell cell = GetCell(MousePositionOnGameBoard().x, MousePositionOnGameBoard().y);

        if ( cell.type == Cell.Type.Invalid || cell.revealed || cell.flagged)
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
                score += 10;
                state[MousePositionOnGameBoard().x, MousePositionOnGameBoard().y] = cell;
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
        score += 10;
        state[cell.position.x, cell.position.y] = cell;

        if(cell.type == Cell.Type.Empty) 
        { 
            Flood(GetCell(cell.position.x-1,cell.position.y)); 
            Flood(GetCell(cell.position.x-1,cell.position.y+1)); 
            Flood(GetCell(cell.position.x,cell.position.y+1)); 
            Flood(GetCell(cell.position.x+1,cell.position.y+1)); 
            Flood(GetCell(cell.position.x+1,cell.position.y)); 
            Flood(GetCell(cell.position.x+1,cell.position.y-1)); 
            Flood(GetCell(cell.position.x,cell.position.y-1)); 
            Flood(GetCell(cell.position.x-1,cell.position.y-1)); 
        }
    }

    private void Explode(Cell cell)
    {
        winLoseText.text = "You lost !";
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


        winLoseText.text = "You won !";
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

    

    private void FlagThemAll()
    {
        bool cheat = false;
        int count = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type == Cell.Type.Mine && cell.flagged)
                {
                    count++;
                }
                if (cell.type != Cell.Type.Mine && cell.flagged)
                {
                    cheat = true;
                }
            }
        }
        if (count == mineCount && !cheat)
        {
            winLoseText.text = "You won !";
            gameOver = true;
        }
    }

}
