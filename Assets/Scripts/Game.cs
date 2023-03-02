using UnityEngine;

public class Game : MonoBehaviour
{
    public int Width { set; get; }
    public int Height { set; get; }
    public int MineCount { set; get; }
    float score;

    int tempScore;
    int tempTime;

    bool easterEgg;

    int timeLastGame;
    int scoreLastGame;

    float cameraZoom = 10;
    float cameraSpeed = 4;
    float scrollSensitivity = 0.3f;
    
    //use to check first click in a game
    bool firstClick;

    bool gameOver = true;
    public bool customGame = false;
    


    private GameBoard gameBoard;
    private CustomSettings customSetting;
    private PlayerInputs inputPlayer;
    private CellsGeneration cellsGeneration;
    private GameUI gameUI;
    private ChoseDifficulty difficulty;
    public Cell[,] state;

    
    

    [SerializeField] ParticleSystem explosion;
    [SerializeField] AudioSource explosionSound;
    [SerializeField] ParticleSystem winParticule;
    [SerializeField] ParticleSystem winParticule2;
    [SerializeField] AudioSource winSound;
    float timePass = 0;

    private void Awake()
    {
        gameBoard = GetComponentInChildren<GameBoard>();
        customSetting = GetComponent<CustomSettings>();
        inputPlayer = GetComponent<PlayerInputs>();
        cellsGeneration = GetComponent<CellsGeneration>();
        gameUI = GetComponent<GameUI>();
        difficulty = GetComponent<ChoseDifficulty>();
    }



    public void getCustomSettings()
    {
            Width = customSetting.Width;
            Height = customSetting.Height;
            MineCount = customSetting.MineCount;
    }


    public void NewGame()
    {
        easterEgg = false;
        Camera.main.transform.rotation = new Quaternion(0, 0, 0, 0);
        winSound.Stop();
        winParticule.Stop();
        winParticule2.Stop();
        explosionSound.Stop();
        explosion.Stop();

        score = 0;
        timePass = 0;
        firstClick = true;
        gameUI.winLoseText.text = "";
        difficulty.setDifficulty();
        state = new Cell[Width, Height];
        gameOver = false;

        cellsGeneration.GenerateCelles();

        Camera.main.orthographicSize = cameraZoom;
        Camera.main.transform.position = new Vector3(Width / 2, Height / 2, -10);

        gameBoard.Draw(Width, Height, state);
    }

    

    private void Update()
    {

        if (!gameOver)
        {
            if (inputPlayer != null)
            {
                if (Input.anyKey)
                {
                    inputPlayer.move(cameraSpeed);
                }
                Camera.main.orthographicSize += inputPlayer.getMouseScroll(scrollSensitivity);
            }
            else
            {
                Debug.Log("error, can't find inputPlayer");
            }
            
                if (Input.GetMouseButtonDown(1))
            {
                Flag();
            }
            else if (Input.GetMouseButtonDown(0))
            {

                if (firstClick)
                {
                    cellsGeneration.GenerateMines();
                    cellsGeneration.GenerateNumber();
                    firstClick = false;
                }
                Reveal();
            }

            timePass += Time.deltaTime;
            tempTime = (int)timePass;
            gameUI.timerText.text = "Time : " + tempTime.ToString();



            if (score < 0)
            {
                score = 0;
            }
            tempScore = (int)score;
            gameUI.scoreText.text = "Your score : " + tempScore.ToString();

            if (easterEgg)
            {
                Camera.main.transform.Rotate(0,0,0.1f);
            }

        }
        else
        {
            timeLastGame = tempTime; 
            gameUI.timerText.text = "You played " + timeLastGame.ToString() + "s";
            scoreLastGame = tempScore;
            gameUI.scoreText.text = "Your last score : " + scoreLastGame.ToString();
            timePass = 0;
            tempScore = 0;
        }
        if (customGame)
        {
            getCustomSettings();
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
        gameBoard.Draw(Width, Height, state);
        FlagThemAll();

    }

    public Vector3Int MousePositionOnGameBoard()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = gameBoard.tilemap.WorldToCell(worldPosition);
        return cellPosition;
    }
    private void Reveal()
    {
        Cell cell = GetCell(MousePositionOnGameBoard().x, MousePositionOnGameBoard().y);

        if (cell.type == Cell.Type.Invalid || cell.revealed || cell.flagged)
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




        gameBoard.Draw(Width, Height, state);
    }

    private void Flood(Cell cell)
    {
        if (cell.revealed) return;
        if (cell.type == Cell.Type.Mine || cell.type == Cell.Type.Invalid) return;


        cell.revealed = true;
        score += 10;
        state[cell.position.x, cell.position.y] = cell;

        if (cell.type == Cell.Type.Empty)
        {
            Flood(GetCell(cell.position.x - 1, cell.position.y));
            Flood(GetCell(cell.position.x - 1, cell.position.y + 1));
            Flood(GetCell(cell.position.x, cell.position.y + 1));
            Flood(GetCell(cell.position.x + 1, cell.position.y + 1));
            Flood(GetCell(cell.position.x + 1, cell.position.y));
            Flood(GetCell(cell.position.x + 1, cell.position.y - 1));
            Flood(GetCell(cell.position.x, cell.position.y - 1));
            Flood(GetCell(cell.position.x - 1, cell.position.y - 1));
        }
    }

    private void Explode(Cell cell)
    {
        gameUI.winLoseText.text = "You lost !";
        gameOver = true;
        cell.revealed = true;
        cell.exploded = true;

        Cell tempCell = cell;
        explosion.transform.position = tempCell.position += new Vector3Int(0, 0, -1);




        state[cell.position.x, cell.position.y] = cell;

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    cell.revealed = true;
                    state[x, y] = cell;
                }
            }
        }


        explosion.Play();
        explosionSound.Play();
    }


    private void CheckWinCondition()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Cell cell = state[x, y];
                if (cell.type != Cell.Type.Mine && !cell.revealed)
                {
                    return;
                }
            }
        }


        gameUI.winLoseText.text = "You won !";
        gameOver = true;
        Vector3 tempCameraPos = Camera.main.transform.position;
        winParticule.transform.position = tempCameraPos += new Vector3Int(-16, 11, 0);
        winParticule2.transform.position = tempCameraPos += new Vector3Int(28, 10, 0);
        winSound.Play();
        winParticule.Play();
        winParticule2.Play();

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    cell.flagged = true;
                    state[x, y] = cell;
                }
                else
                {
                    cell.revealed = true;
                }
            }
        }
    }

    public Cell GetCell(int x, int y)
    {
        if (IsValid(x, y))
        {
            return state[x, y];
        }
        else
        {
            return new Cell();
        }
    }

    private bool IsValid(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    private void FlagThemAll()
    {
        bool cheat = false;
        int count = 0;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
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
        if (count == (MineCount / 3) * 2) easterEgg = true;
        if (count == MineCount && !cheat)
        {
            Vector3 tempCameraPos = Camera.main.transform.position;
            winParticule.transform.position = tempCameraPos += new Vector3Int(-16, 11, 0);
            winParticule2.transform.position = tempCameraPos += new Vector3Int(28, 10, 0);

            gameUI.winLoseText.text = "You won !";
            winSound.Play();
            winParticule.Play();
            winParticule2.Play();
            gameOver = true;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cell cell = state[x, y];
                    cell.revealed = true;

                }
            }
        }
    }

}
