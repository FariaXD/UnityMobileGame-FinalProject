using UnityEngine;

public class OpenCloseMenu: MonoBehaviour {
    public bool openMethod;
    public bool isCombatStage;
    private MenuEngine menuEngine;

    private void Start() {
        menuEngine = GameObject.FindGameObjectWithTag("MenuEngine").GetComponent<MenuEngine>();

    }
    private void OnMouseDown() {
        menuEngine.OpenClose(openMethod, isCombatStage);
    }
}