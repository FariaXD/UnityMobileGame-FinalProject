using UnityEngine;
using System.Collections.Generic;

public class MenuEngine : MonoBehaviour {
    /*
    Runtime class
    Controls all the different menus and switches beetween them
    */
    private MenuDeckEngine menuDeckEngine; //deck inventory menu engine
    private MenuBagEngine menuBagEngine; //artifact menu engine
    private MenuOptionsEngine menuOptionsEngine;//options menu engine
    private GameEngine gameEngine; //main game engine
    public List<GameObject> menus = new List<GameObject>(); //list of menu gameobjects
    public MENUTYPE selectedMenu = MENUTYPE.MAP; //current selected menu
    public GameObject menuPositionCombat; //position of stage combat
    public GameObject menuPositionEvent; //position of stage event
    public GameObject menuPositionOff; //position off stage (stage selector)
    public GameObject closePosition; //position to hide menus
    public PLAYERSTAGE currentStageType = PLAYERSTAGE.OFF; //current stage type
    public bool initializedFirstTime = false; //first time initialization

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
    //Initializes the menus
    public void Initialize(){
        menuDeckEngine.Initialize();
        menuBagEngine.Initialize(gameEngine.combatEngine.team.inventory.artifacts);
    }
    //Update artifact bag
    public void UpdateBag(List<Artifact> artifacts){
        menuBagEngine.Initialize(artifacts);
    }
    //Increment a stat
    public void IncrementStat(MenuOptionsEngine.STATS stat){
        menuOptionsEngine.IncrementStat(stat);
    }
    //Get player stage
    public PLAYERSTAGE GetPlayerStageType(){
        return currentStageType;
    }
    //Switch menu
    public void SwitchMenu(MENUTYPE newMenu){
        OpenClose(MENUACTION.SWITCH, currentStageType);
        selectedMenu = newMenu;
        OpenClose(MENUACTION.OPEN, currentStageType);
    }
    //Open or close a menu
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