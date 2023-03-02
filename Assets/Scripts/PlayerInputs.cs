using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    GameUI gameUI;

    private void Awake()
    {
        gameUI = GetComponentInParent<GameUI>();
    }
    public float MouseScroll { get; private set; }
    public void move(float cameraSpeed)
    {
       
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
        {
            Camera.main.transform.position += new Vector3(0, cameraSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            Camera.main.transform.position += new Vector3(-cameraSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Camera.main.transform.position += new Vector3(0, -cameraSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Camera.main.transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.H))
        {
            gameUI.hideShow();
        }
    }

    public float getMouseScroll(float sensitivity)
    {
        MouseScroll = Input.mouseScrollDelta.y * -sensitivity; //Last float = sensitivity
        return MouseScroll;
    }
}
