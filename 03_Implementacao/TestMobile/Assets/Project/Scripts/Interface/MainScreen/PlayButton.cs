using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private UIEngine uIEngine;
    private void Start() {
        uIEngine = GameObject.FindGameObjectWithTag("UIEngine").GetComponent<UIEngine>();

    }
    private void OnMouseDown() {
        Debug.Log("Switching screen");
        uIEngine.SwitchScreen(UIEngine.Screen.STAGESELECTOR);
    }
}
