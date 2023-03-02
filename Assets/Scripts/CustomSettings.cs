using UnityEngine;
using UnityEngine.UI;

public class CustomSettings : MonoBehaviour
{

    GameUI renderUI;

    public int Width { get; set; }
    public int Height { get; set; }
    public int MineCount { get; set; }

    private void Awake()
    {
        renderUI = GetComponentInParent<GameUI>();
    }

    private void Update()
    {
        
        Width = (int)renderUI.Sliderwidth.value;
        Height = (int)renderUI.Sliderheight.value;
        renderUI.SlidermineCount.maxValue = Width * Height - 1;
        MineCount = (int)renderUI.SlidermineCount.value;

        renderUI.widthText.text = Width.ToString();
        renderUI.heightText.text = Height.ToString();
        renderUI.mineCountText.text = MineCount.ToString();
    }
   

}
