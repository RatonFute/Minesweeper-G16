using UnityEngine;


public class ChoseDifficulty : MonoBehaviour
{

    private enum Difficulty
    {
        easy,
        medium,
        hard,
        custom,
        random,
    }
    Difficulty difficulty;

    GameUI gameUI;
    Game game;
    private void Awake()
    {
        gameUI= GetComponent<GameUI>();
        game= GetComponent<Game>();
    }

    public void DropDownSelectDifficulty()
    {
        switch (gameUI.dropdownDifficulty.value)
        {
            case 0: difficulty = ChoseDifficulty.Difficulty.easy; Debug.Log("easy"); break;
            case 1: difficulty = ChoseDifficulty.Difficulty.medium; Debug.Log("medium"); break;
            case 2: difficulty = ChoseDifficulty.Difficulty.hard; Debug.Log("hard"); break;
            case 3: difficulty = ChoseDifficulty.Difficulty.custom; Debug.Log("custom"); break;
            case 4: difficulty = ChoseDifficulty.Difficulty.random; Debug.Log("Random"); break;
        }
    }
    public void setDifficulty()
    {

        game.customGame = false;
        switch (difficulty)
        {

            case ChoseDifficulty.Difficulty.easy:
                game.Width = 10;
                game.Height = 10;
                game.MineCount = 10;
                game.customGame = false;
                break;

            case ChoseDifficulty.Difficulty.medium:
                game.Width = 12;
                game.Height = 12;
                game.MineCount = 25;
                game.customGame = false;
                break;

            case ChoseDifficulty.Difficulty.hard:
                game.Width = 16;
                game.Height = 16;
                game.MineCount = 40;
                game.customGame = false;
                break;

            case ChoseDifficulty.Difficulty.custom:
                game.getCustomSettings();
                game.customGame = true;
                break;

            case ChoseDifficulty.Difficulty.random:
                game.Width = UnityEngine.Random.Range(3, 45);
                game.Height = UnityEngine.Random.Range(3, 45);
                game.MineCount = UnityEngine.Random.Range(1, game.Width * game.Height / 3);
                game.customGame = false;
                break;

            default:
                game.Width = 10;
                game.Height = 10;
                game.MineCount = 10;
                game.customGame = false;
                break;
        }

    }

}
