using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class grid : MonoBehaviour
{
    [SerializeField] GameObject square;
    [SerializeField] GameObject outline;
    int timeleft = 10;
    Text timer;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i =-4; i < 5; i++)
        {
            for(int j = -4;j < 5; j++)
            {
                Instantiate(outline, new Vector2(i, j), Quaternion.identity);
                Instantiate(square, new Vector2(i, j), Quaternion.identity);
            }
        } 
    }

    private void Update()
    {
        
    }
}
