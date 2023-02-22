using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class grid : MonoBehaviour
{
    [SerializeField] GameObject square;
    [SerializeField] GameObject outline;
    [SerializeField] GameObject bomb;
    [SerializeField] float timeleft;
    [SerializeField] Text timerText;
    int displayedTime;
    Vector3 position;
    int countBomb;
    
    // Start is called before the first frame update
    void Start()
    {
        timerText.text = timeleft.ToString() ;
        drawGrid();
        drawBomb();

    }

    private void Update()
    {

            if (timeleft > 0)
            {
                timeleft -= Time.deltaTime;
                displayedTime = (int)timeleft;
                timerText.text = "time left : " + displayedTime.ToString();
            }
            if (timeleft < 0)
            {
                timeleft = 0;
                timerText.text = "Game over";
                
            }

        
    }
    public void drawGrid ()
    {
        for (int i = -4; i < 5; i++)
        {
            for (int j = -4; j < 5; j++)
            {
                Instantiate(outline, new Vector2(i, j), Quaternion.identity);
                Instantiate(square, new Vector2(i, j), Quaternion.identity);
                
            }
        }
    }
    void drawBomb()
    {
        position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), -2);
        Instantiate(bomb, position, Quaternion.identity);
        position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), -2);
        Instantiate(bomb, position, Quaternion.identity);
        position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), -2);
        Instantiate(bomb, position, Quaternion.identity);
        position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), -2);
        Instantiate(bomb, position, Quaternion.identity);
    }

    void addCount()
    {
        
        Vector3 posTemp = position + new Vector3(1, 0, 0);
        if(posTemp == square.transform.position) { countBomb += 1; }
        //(position + new Vector3(1, -1, 0))
        //(position + new Vector3(0, -1, 0))
        //(position + new Vector3(-1, -1, 0))
        //(position + new Vector3(-1, 0, 0))
        //(position + new Vector3(0, 1, 0))     
    }
}
