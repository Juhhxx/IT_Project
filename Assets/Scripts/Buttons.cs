using UnityEngine;

public class Buttons : MonoBehaviour
{
    private void Update()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick button " + i))
                Debug.Log("Joystick button " + i + " pressed");
        }
    }

    public static bool InputYes()
    {
        return Input.GetKeyDown(KeyCode.JoystickButton10) || Input.GetKeyDown(KeyCode.Y);
    }
    public static bool InputNo()
    {
        return Input.GetKeyDown(KeyCode.JoystickButton11) || Input.GetKeyDown(KeyCode.N);
    }
}
