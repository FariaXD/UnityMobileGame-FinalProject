using UnityEngine;
using System.Collections.Generic;

public class MenuEngine : MonoBehaviour {
    private MenuDeckEngine menuDeckEngine;
    private MenuBagEngine menuBagEngine;
    private MenuOptionsEngine menuOptionsEngine;
    private GameEngine gameEngine;
    public List<GameObject> menus = new List<GameObject>();
    public MENUTYPE selectedMenu = MENUTYPE.MAP;
    public GameObject menuPositionCombat;
    public GameObject menuPositionEvent;
    public GameObject menuPositionOff;
    public GameObject closePosition;
    public PLAYERSTAGE currentStageType = PLAYERSTAGE.OFF;
    public enum MENUTYPE {
        DECK,
        ARTIFACTS,
        OPTIONS,
        MAP
    }
    public enum PLAYERSTAGE{
        COMBAT,
        EVENT,
        OFF
    }
    public enum MENUACTION{
        OPEN,
        CLOSE,
        SWITCH
    }
    private void Start() {
        menuDeckEngine = GameObject.FindGameObjectWithTag("MenuDeckEngine").GetComponent<MenuDeckEngine>();
        menuBagEngine = GameObject.FindGameObjectWithTag("MenuBagEngine").GetComponent<MenuBagEngine>();
        menuOptionsEngine = GameObject.FindGameObjectWithTag("MenuOptionsEngine").GetComponent<MenuOptionsEngine>();
        gameEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
    }
    public void Initialize(){
        menuDeckEngine.Initialize();
        menuBagEngine.Initialize(gameEngine.combatEngine.team.inventory.artifacts);
    }

    public void UpdateBag(List<Artifact> artifacts){
        menuBagEngine.Initialize(artifacts);
    }

    public void IncrementStat(MenuOptionsEngine.STATS stat){
        menuOptionsEngine.IncrementStat(stat);
    }

    public PLAYERSTAGE GetPlayerStageType(){
        return currentStageType;
    }

    public void SwitchMenu(MENUTYPE newMenu){
        OpenClose(MENUACTION.SWITCH, currentStageType);
        selectedMenu = newMenu;
        OpenClose(MENUACTION.OPEN, currentStageType);
    }

    public void OpenClose(MENUACTION action, PLAYERSTAGE currentStageType)
    {
        if (action == MENUACTION.CLOSE && this.currentStageType != PLAYERSTAGE.OFF){
            menus[(int)selectedMenu].transform.position = closePosition.transform.position;
        }
        else if(action == MENUACTION.SWITCH){
            menus[(int)selectedMenu].transform.position = closePosition.transform.position;
        }
        else if(action == MENUACTION.OPEN)
        {
            this.currentStageType = currentStageType;
            switch (currentStageType)
            {
                case PLAYERSTAGE.COMBAT:
                    menus[(int)selectedMenu].transform.position = menuPositionCombat.transform.position;
                    break;
                case PLAYERSTAGE.EVENT:
                    menus[(int)selectedMenu].transform.position = menuPositionEvent.transform.position;
                    break;
                case PLAYERSTAGE.OFF:
                    menus[(int)selectedMenu].transform.position = menuPositionOff.transform.position;
                    break;
            }
        }
    }
}