using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEngine : MonoBehaviour
{
    private WorldEngine worldEngine;
    private GameEngine gameEngine;
    private CameraRepository cameras;

    public enum Screen{
        MAINSCREEN,
        STAGESELECTOR,
        STAGE
    }
    public void Start()
    {
        worldEngine = GameObject.FindGameObjectWithTag("WorldEngine").GetComponent<WorldEngine>();
        gameEngine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
        cameras = GetComponent<CameraRepository>();
    }


    public void SwitchScreen(Screen _screen){
        switch(_screen){
            case Screen.MAINSCREEN:
                cameras.mainScreen.enabled = true;
                cameras.stageSelector.enabled = false;
                break;
            case Screen.STAGESELECTOR:
                cameras.stageSelector.enabled = true;
                cameras.mainScreen.enabled = false;
                cameras.game.enabled = false;
                break;
            case Screen.STAGE:
                cameras.game.enabled = true;
                cameras.stageSelector.enabled = false;
                break;
        }
    } 
}
