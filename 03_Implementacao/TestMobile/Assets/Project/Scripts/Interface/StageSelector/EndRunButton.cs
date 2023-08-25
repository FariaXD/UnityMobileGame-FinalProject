using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRunButton : MonoBehaviour
{
    private UIEngine uIEngine;
    private void Start()
    {
        uIEngine = GameObject.FindGameObjectWithTag("UIEngine").GetComponent<UIEngine>();

    }
    //Button to end the current adventure and return to main screen
    private void OnMouseDown()
    {
        uIEngine.SwitchScreen(UIEngine.Screen.MAINSCREEN);
        uIEngine.PlayerEndedRun();
    }
}
