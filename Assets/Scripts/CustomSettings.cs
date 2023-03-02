using UnityEngine;
using UnityEngine.UI;

public class CustomSettings : MonoBehaviour
{

    [SerializeField] UnityEngine.UI.Text widthText;
    [SerializeField] UnityEngine.UI.Text heightText;
    [SerializeField] UnityEngine.UI.Text mineCountText;
    [SerializeField] Slider Sliderwidth;
    [SerializeField] Slider Sliderheight;
    [SerializeField] Slider SlidermineCount;

    public int Width { get; set; }
    public int Height { get; set; }
    public int MineCount { get; set; }

    private void Update()
    {
        Width = (int)Sliderwidth.value;
        Height = (int)Sliderheight.value;
        SlidermineCount.maxValue = Width * Height - 1;
        MineCount = (int)SlidermineCount.value;

        widthText.text = Width.ToString();
        heightText.text = Height.ToString();
        mineCountText.text = MineCount.ToString();
    }
   

}
