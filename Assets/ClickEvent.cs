using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickEvent : MonoBehaviour
{
    public GameObject Case;

    bool clicked = false;
    bool rightClick = false;

    Vector3 mousePos = new Vector3();

    Collider collider ;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnMouseEnter()
    {

    }
    private void OnMouseExit()
    {
        if (!clicked)
        {
            Case.GetComponent<SpriteRenderer>().color = Color.gray;
        }

    }

    private void OnMouseOver()
    {
        if (!clicked)
        {
            Case.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    void OnMouseDown()
    {
        if (!clicked)
        {
            Case.GetComponent<SpriteRenderer>().color = Color.green;
        }
        /* 


         if (!clicked)
         {
             if (Input.GetMouseButtonDown(0))
             {
                //if(collider.bounds.Contains(mousePos)) {
                     Case.GetComponent<SpriteRenderer>().color = Color.black;
                     clicked = true;
                //}




             }
             if (Input.GetMouseButtonDown(1))
             {
                 switch (rightClick)
                 {
                     case true:
                         GetComponent<SpriteRenderer>().color = Color.red;
                         rightClick = false;
                         break;
                     default:
                         GetComponent<SpriteRenderer>().color = Color.gray;
                         rightClick = true;
                         break;
                 }

             }

         }*/
    }
}
