using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public Text timerText;
    public Text winLoseText;
    public Text scoreText;
    public Text widthText;
    public Text heightText;
    public Text mineCountText;
    public Slider Sliderwidth;
    public Slider Sliderheight;
    public Slider SlidermineCount;
    public TMPro.TMP_Dropdown dropdownDifficulty;

    Renderer render;
    private void Awake()
    {
        render = GetComponent<Renderer>();
    }
    public void hideShow()
    {
        render.enabled = !render.enabled;
    }
}
