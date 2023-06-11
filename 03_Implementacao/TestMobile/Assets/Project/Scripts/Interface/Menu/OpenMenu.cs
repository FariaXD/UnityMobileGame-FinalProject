using UnityEngine;

public class OpenMenu : MonoBehaviour {
    public GameObject menu;
    private void OnMouseDown() {
        menu.SetActive(true);
    }
}