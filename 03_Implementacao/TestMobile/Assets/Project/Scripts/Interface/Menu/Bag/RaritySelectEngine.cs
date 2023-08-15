using TMPro;
using UnityEngine;

public class RaritySelectEngine : MonoBehaviour {
    public Artifact.ArtifactRarity rarity;
    public MenuBagEngine menuBagEngine;
    public TextMeshProUGUI counter;
    private void Start() {
        menuBagEngine = GameObject.FindGameObjectWithTag("MenuBagEngine").GetComponent<MenuBagEngine>();
    }

    public void ChangeCounter(int counter){
        this.counter.text = counter.ToString();
    }

    private void OnMouseDown() {
        menuBagEngine.SwitchRarity(rarity);
    }   
}