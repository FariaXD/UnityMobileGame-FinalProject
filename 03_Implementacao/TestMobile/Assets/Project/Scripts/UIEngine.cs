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
        STAGECOMBAT,
        STAGEEVENT
    }
    public void Start()
    {
        worldEngine = GameObject.FindGameObjectWithTag("WorldEngine").GetComponent<WorldEngine>();
        gameEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
        cameras = GetComponent<CameraRepository>();
    }

    public void PlayerEndedRun(){
        gameEngine.StageCompletedOrWorldEnded(false);
    }


    public void SwitchScreen(Screen _screen){
        cameras.DeactiveAllCameras();
        switch(_screen){
            case Screen.MAINSCREEN:
                cameras.mainScreen.enabled = true;
                break;
            case Screen.STAGESELECTOR:
                cameras.stageSelector.enabled = true;
                break;
            case Screen.STAGECOMBAT:
                cameras.combatStage.enabled = true;
                break;
            case Screen.STAGEEVENT:
                cameras.eventStage.enabled = true;
                break;
        }
    } 
}
