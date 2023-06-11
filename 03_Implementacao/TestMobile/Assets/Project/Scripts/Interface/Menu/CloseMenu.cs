using UnityEngine;

public class CloseMenu : MonoBehaviour {
    public GameObject menu;
    private void Start()
    {
        menu.SetActive(false);
    }
    private void OnMouseDown()
    {
        menu.SetActive(false);
    }
}