using UnityEngine;
using UnityEngine.InputSystem;

public class SoloPlayer : MonoBehaviour
{
    public GameObject P0; // playerTestSolo

    void Start()
    {
        Debug.Log(Gamepad.all.Count);
        if (Gamepad.all.Count == 0)
        {
            Instantiate(P0, new Vector3(0, 1, 0), Quaternion.identity);
        }


        /*bool noGamepadConnected = true;
        Debug.Log("test1");
        string[] joystickNames = Input.GetJoystickNames();
        Debug.Log("test2");
        //detection d'une manette
        foreach (string joystickName in joystickNames)
        {
            Debug.Log("Joystick détecté : " + joystickName);
            if (!string.IsNullOrEmpty(joystickName))
            {
                noGamepadConnected = false;
                break;
            }
        }
        if (noGamepadConnected)
        {
            Instantiate(P0, new Vector3(0, 1, 0), Quaternion.identity);
            Debug.Log("Aucune manette connectée. Objet instancié.");
        }*/
    }
}
