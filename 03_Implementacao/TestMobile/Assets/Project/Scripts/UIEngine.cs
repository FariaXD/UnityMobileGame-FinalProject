using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEngine : MonoBehaviour
{
    private WorldEngine worldEngine;
    private GameEngine gameEngine;
    private CameraRepository cameras;
    public List<TextMeshProUGUI> levelCount = new List<TextMeshProUGUI>();

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

    public void UpdateStageNumbersUI(){
        string a = worldEngine.currentWorldCount.ToString() + "-" + (worldEngine.currentLevel + 1).ToString();
        levelCount.ForEach(lc => lc.text = a);
    }


    public void SwitchScreen(Screen _screen){
        cameras.DeactiveAllCameras();
        switch(_screen){
            case Screen.MAINSCREEN:
                cameras.mainScreen.enabled = true;
                gameEngine.menuEngine.selectedMenu = MenuEngine.MENUTYPE.MAP;
                break;
            case Screen.STAGESELECTOR:
                cameras.stageSelector.enabled = true;
                gameEngine.menuEngine.selectedMenu = MenuEngine.MENUTYPE.MAP;
                gameEngine.menuEngine.OpenClose(MenuEngine.MENUACTION.OPEN, MenuEngine.PLAYERSTAGE.OFF);
                break;
            case Screen.STAGECOMBAT:
                cameras.combatStage.enabled = true;
                gameEngine.menuEngine.OpenClose(MenuEngine.MENUACTION.CLOSE, MenuEngine.PLAYERSTAGE.COMBAT);
                break;
            case Screen.STAGEEVENT:
                cameras.eventStage.enabled = true;
                gameEngine.menuEngine.OpenClose(MenuEngine.MENUACTION.CLOSE, MenuEngine.PLAYERSTAGE.EVENT);
                break;
        }
    } 
}
