using TMPro;
using UnityEngine;

public class RaritySelectEngine : MonoBehaviour {
    /*
        Runtime class that changes the current selected rarity in the artifact menu
    */
    public Artifact.ArtifactRarity rarity; //associated rarity
    public MenuBagEngine menuBagEngine; //engine
    public TextMeshProUGUI counter; //counter text field
    private void Start() {
        menuBagEngine = GameObject.FindGameObjectWithTag("MenuBagEngine").GetComponent<MenuBagEngine>();
    }
    //Changes the counter
    public void ChangeCounter(int counter){
        this.counter.text = counter.ToString();
    }
    //Selects new rarity
    private void OnMouseDown() {
        menuBagEngine.SwitchRarity(rarity);
    }   
}