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
    private void OnMouseDown()
    {
        uIEngine.SwitchScreen(UIEngine.Screen.MAINSCREEN);
    }
}
