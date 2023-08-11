using UnityEngine;

public class OpenCloseInventory : MonoBehaviour {
    public GameObject menu;
    public GameObject menuPosition;
    public GameObject closePosition;
    public bool openMethod;
    private void OnMouseDown() {
        if(openMethod)
            menu.transform.position = menuPosition.transform.position;
        else
            menu.transform.position = closePosition.transform.position;
    }
}