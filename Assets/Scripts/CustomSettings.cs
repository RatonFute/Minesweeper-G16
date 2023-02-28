using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomSettings : MonoBehaviour
{

    [SerializeField] Button newGame;
    [SerializeField] UnityEngine.UI.Text widthText;
    [SerializeField] UnityEngine.UI.Text heightText;
    [SerializeField] UnityEngine.UI.Text mineCountText;
    [SerializeField] Slider Sliderwidth;
    [SerializeField] Slider Sliderheight;
    [SerializeField] Slider SlidermineCount;

    public int Width { get; set; }
    public int Height { get; set; }
    public int MineCount { get; set; }

    public bool loadedSettings;


    private void Awake()
    {
        Slider heightSetting = Sliderheight.GetComponent<Slider>();
        Slider widthSetting = Sliderwidth.GetComponent<Slider>();
        Slider mineCountSetting = SlidermineCount.GetComponent<Slider>();
        loadedSettings = false;
    }

    private void Update()
    {
        Width = (int)Sliderwidth.value;
        Height = (int)Sliderheight.value;
        SlidermineCount.maxValue = Width * Height - 1;
        MineCount = (int)SlidermineCount.value;

        int widthValue = (int)Sliderwidth.value;
        int heightValue = (int)Sliderheight.value;
        int mineCountValue = (int)SlidermineCount.value;
        widthText.text = widthValue.ToString();
        heightText.text = heightValue.ToString();
        mineCountText.text = mineCountValue.ToString();
    }
   
    public void changeScene()
    {
        loadedSettings = true;
        SceneManager.LoadScene("MineSweeper", LoadSceneMode.Additive);
    }

}
