using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEngine : MonoBehaviour
{
    /*
    Runtime class
    Main engine that controls the interface ofthe game
    */
    private WorldEngine worldEngine;
    private GameEngine gameEngine;
    private CameraRepository cameras; //all cameras
    public List<TextMeshProUGUI> levelCount = new List<TextMeshProUGUI>(); //list of level text fields
    public MenuEngine menuEngine; //Engine responsible for the menu
    public Animator mainScreenAnimator; //
    private float mainAnimSpeed;

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
        menuEngine = GameObject.FindGameObjectWithTag("MenuEngine").GetComponent<MenuEngine>();
        cameras = GetComponent<CameraRepository>();
        mainAnimSpeed = mainScreenAnimator.speed;
    }
    //If player forces an end to his run
    public void PlayerEndedRun(){
        gameEngine.StageCompletedOrWorldEnded(false);
    }
    //Update stage numbers
    public void UpdateStageNumbersUI(){
        string a = worldEngine.currentWorldCount.ToString() + "-" + (worldEngine.currentLevel + 1).ToString();
        levelCount.ForEach(lc => lc.text = a);
    }
    //Switches the current screen
    public void SwitchScreen(Screen _screen){
        cameras.DeactiveAllCameras();
        switch(_screen){
            case Screen.MAINSCREEN:
                mainScreenAnimator.speed = mainAnimSpeed;
                cameras.mainScreen.enabled = true;
                menuEngine.selectedMenu = MenuEngine.MENUTYPE.MAP;
                break;
            case Screen.STAGESELECTOR:
                mainScreenAnimator.speed = 0;
                cameras.stageSelector.enabled = true;
                menuEngine.selectedMenu = MenuEngine.MENUTYPE.MAP;
                menuEngine.OpenClose(MenuEngine.MENUACTION.OPEN, MenuEngine.PLAYERSTAGE.OFF);
                break;
            case Screen.STAGECOMBAT:
                cameras.combatStage.enabled = true;
                menuEngine.OpenClose(MenuEngine.MENUACTION.CLOSE, MenuEngine.PLAYERSTAGE.COMBAT);
                break;
            case Screen.STAGEEVENT:
                cameras.eventStage.enabled = true;
                menuEngine.OpenClose(MenuEngine.MENUACTION.CLOSE, MenuEngine.PLAYERSTAGE.EVENT);
                break;
        }
    } 
}
