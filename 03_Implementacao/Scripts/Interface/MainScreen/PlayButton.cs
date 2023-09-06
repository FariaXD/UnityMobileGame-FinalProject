using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    //Play button object
    private UIEngine uIEngine;
    private void Start() {
        uIEngine = GameObject.FindGameObjectWithTag("UIEngine").GetComponent<UIEngine>();

    }
    //Switches the screen to the stage selector
    private void OnMouseDown() {
        uIEngine.SwitchScreen(UIEngine.Screen.STAGESELECTOR);
    }
}
