using UnityEngine;

public class MenuSwitch : MonoBehaviour {
    /*
    Switches a menu
    */
    public MenuEngine.MENUTYPE type;
    public MenuEngine menuEngine;

    private void Start() {
        menuEngine = GameObject.FindGameObjectWithTag("MenuEngine").GetComponent<MenuEngine>();
    }

    private void OnMouseDown() {
        menuEngine.SwitchMenu(type);
    }
}