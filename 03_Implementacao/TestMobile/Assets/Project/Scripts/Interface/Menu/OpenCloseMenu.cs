using UnityEngine;

public class OpenCloseMenu: MonoBehaviour {
    /*
    Opens or close a menu
    */
    public MenuEngine.MENUACTION action;
    public MenuEngine.PLAYERSTAGE stageType;
    private MenuEngine menuEngine;

    private void Start() {
        menuEngine = GameObject.FindGameObjectWithTag("MenuEngine").GetComponent<MenuEngine>();

    }
    private void OnMouseDown() {
        menuEngine.OpenClose(action, stageType);
    }
}