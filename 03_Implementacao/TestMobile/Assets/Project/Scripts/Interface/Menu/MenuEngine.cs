using UnityEngine;
using System.Collections.Generic;

public class MenuEngine : MonoBehaviour {
    private MenuDeckEngine menuDeckEngine;
    private MenuBagEngine menuBagEngine;
    private GameEngine gameEngine;
    public List<GameObject> menus = new List<GameObject>();
    public MENUTYPE selectedMenu;
    public GameObject menuPositionCombat;
    public GameObject menuPositionEvent;
    public GameObject closePosition;
    private bool isCombatStage = true;
    public enum MENUTYPE {
        DECK,
        ARTIFACTS,
        OPTIONS,
        MAP
    }
    private void Start() {
        menuDeckEngine = GameObject.FindGameObjectWithTag("MenuDeckEngine").GetComponent<MenuDeckEngine>();
        menuBagEngine = GameObject.FindGameObjectWithTag("MenuBagEngine").GetComponent<MenuBagEngine>();
        gameEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
    }
    public void Initialize(){
        menuDeckEngine.Initialize();
        menuBagEngine.Initialize(gameEngine.combatEngine.team.inventory.artifacts);
    }

    public void UpdateBag(List<Artifact> artifacts){
        menuBagEngine.Initialize(artifacts);
    }

    public void SwitchMenu(MENUTYPE newMenu){
        OpenClose(false, isCombatStage);
        selectedMenu = newMenu;
        OpenClose(true, isCombatStage);
       /*  switch (newMenu){
            case MENUTYPE.ARTIFACTS:
            break;
            case MENUTYPE.MAP:
            break;
            case MENUTYPE.DECK:
            break;
            case MENUTYPE.OPTIONS:
            break;
        } */
    }

    public void OpenClose(bool method, bool isCombatStage){
        Vector3 pos = isCombatStage ? menuPositionCombat.transform.position : menuPositionEvent.transform.position;
        this.isCombatStage = isCombatStage;
        if (method)
            menus[(int)selectedMenu].transform.position = pos;
        else
            menus[(int)selectedMenu].transform.position = closePosition.transform.position;
    }
}