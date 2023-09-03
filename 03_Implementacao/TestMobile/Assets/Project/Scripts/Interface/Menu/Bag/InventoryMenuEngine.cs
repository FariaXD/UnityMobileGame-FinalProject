using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryMenuEngine : MonoBehaviour {
    /*
        Runtime responsible for updating and managing the artifact slots
    */
    public List<ArtifactSlotMenuEngine> slots = new List<ArtifactSlotMenuEngine>(); //list of slots
    public Artifact.ArtifactRarity currentRarity = Artifact.ArtifactRarity.COMMON; //current rarity selected
    public List<ArtifactCountPair> artifacts = new List<ArtifactCountPair>(); //list of all artifacts and there ammount
    public TextMeshProUGUI emptyMsg, raritySelectedText; //text fields
    public List<RaritySelectEngine> rarityButtons = new List<RaritySelectEngine>(); //list of rarity buttons
    private DisplayArtifactMenuEngine displayArtifactMenuEngine; //engine that displays the detailed artifact
    public bool initialized = false; //engine initialized bool
    private void Start() {
        displayArtifactMenuEngine = GameObject.FindGameObjectWithTag("MenuDisplayArtifact").GetComponent<DisplayArtifactMenuEngine>();
        GameObject[] rarityButtons = GameObject.FindGameObjectsWithTag("MenuBagRarities");
        for (int i = 0; i < rarityButtons.Length; i++)
            this.rarityButtons.Add(rarityButtons[i].GetComponent<RaritySelectEngine>()); //load rarity buttons list
    }
    //Method that initializes the menu
    public void Initialize(List<ArtifactCountPair> artifacts){
        this.artifacts = artifacts;
        initialized = true;
        GameObject[] artSlots = GameObject.FindGameObjectsWithTag("MenuArtSlot");
        for (int i = 0; i < artSlots.Length; i++)
            slots.Add(artSlots[i].GetComponent<ArtifactSlotMenuEngine>()); //Initialize slots
        SwitchRarity(Artifact.ArtifactRarity.COMMON); //Switches rarity
        UpdateRarityCounts(); //Update rarity counters
    }
    //Update artifacts slots
    public void UpdateArtifacts(List<ArtifactCountPair> artifacts){
        this.artifacts = artifacts;
        SwitchRarity(Artifact.ArtifactRarity.COMMON);
        UpdateRarityCounts();
    }

    //Filters to a specific rarity and updates the slots
    public void SwitchRarity(Artifact.ArtifactRarity newRarity){
        currentRarity = newRarity;
        raritySelectedText.text = Artifact.GetStringOfRarity(newRarity);
        raritySelectedText.color = Artifact.GetColorViaRarity(newRarity);
        InitializeSlots();
    }
    //Update rarity counters
    public void UpdateRarityCounts(){
        for(int i = 0; i < rarityButtons.Count; i++){
            int count = 0;
            for (int j = 0; j < artifacts.Count; j++)
            {
                if(artifacts[j].Rarity == rarityButtons[i].rarity)
                    count+= artifacts[j].Count;
            }
            rarityButtons[i].ChangeCounter(count);
        }
    }
    //Initializes the artifacts slots while accounting if there are none
    private void InitializeSlots(){
        AddEmptyMessage(false); //removes empty message
/*         AddPageArrows(false);
 */        int initialized = 0;
        slots.ForEach(s => s.ClearSlot()); // clears all slots
        for(int i = 0; i < artifacts.Count; i++){
            if(initialized >= slots.Count)
                break;
            if(artifacts[i].Rarity == currentRarity){
                slots[initialized].SetArtifact(artifacts[i]);
                initialized++;
            }
        }
        if(initialized == 0)
            AddEmptyMessage(true); //adds empty message
       // else if(initialized >= slots.Count)
/*             AddPageArrows(true);
 */     if(artifacts.Count > 0)
            SelectArtifact(artifacts[0]); //assigns new artifact to the slot
    }

    //Selects new artifact to be displayed
    public void SelectArtifact(ArtifactCountPair artifact){
        displayArtifactMenuEngine.SetArtifact(artifact);
    }

    //Adds empty message 
    private void AddEmptyMessage(bool state){
        emptyMsg.enabled = state;
    }
}

