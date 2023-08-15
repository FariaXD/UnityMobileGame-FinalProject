using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryMenuEngine : MonoBehaviour {
    public List<ArtifactSlotMenuEngine> slots = new List<ArtifactSlotMenuEngine>();
    public Artifact.ArtifactRarity currentRarity = Artifact.ArtifactRarity.COMMON;
    public List<ArtifactCountPair> artifacts = new List<ArtifactCountPair>();
    public TextMeshProUGUI emptyMsg, raritySelectedText;
    public SpriteRenderer rightArrow, leftArrow;
    public List<RaritySelectEngine> rarityButtons = new List<RaritySelectEngine>();
    private DisplayArtifactMenuEngine displayArtifactMenuEngine;
    public bool initialized = false;
    private void Start() {
        displayArtifactMenuEngine = GameObject.FindGameObjectWithTag("MenuDisplayArtifact").GetComponent<DisplayArtifactMenuEngine>();
        GameObject[] rarityButtons = GameObject.FindGameObjectsWithTag("MenuBagRarities");
        for (int i = 0; i < rarityButtons.Length; i++)
            this.rarityButtons.Add(rarityButtons[i].GetComponent<RaritySelectEngine>());
    }
    public void Initialize(List<ArtifactCountPair> artifacts){
        this.artifacts = artifacts;
        initialized = true;
        GameObject[] artSlots = GameObject.FindGameObjectsWithTag("MenuArtSlot");
        for (int i = 0; i < artSlots.Length; i++)
            slots.Add(artSlots[i].GetComponent<ArtifactSlotMenuEngine>());
        SwitchRarity(Artifact.ArtifactRarity.COMMON);
        UpdateRarityCounts();
    }

    public void UpdateArtifacts(List<ArtifactCountPair> artifacts){
        this.artifacts = artifacts;
        SwitchRarity(Artifact.ArtifactRarity.COMMON);
        UpdateRarityCounts();
    }

    public void SwitchRarity(Artifact.ArtifactRarity newRarity){
        currentRarity = newRarity;
        raritySelectedText.text = Artifact.GetStringOfRarity(newRarity);
        raritySelectedText.color = Artifact.GetColorViaRarity(newRarity);
        InitializeSlots();
    }

    public void UpdateRarityCounts(){
        for(int i = 0; i < rarityButtons.Count; i++){
            int count = 0;
            for (int j = 0; j < artifacts.Count; j++)
            {
                if(artifacts[j].Rarity == rarityButtons[i].rarity)
                    count++;
            }
            rarityButtons[i].ChangeCounter(count);
        }
    }

    private void InitializeSlots(){
        AddEmptyMessage(false);
        AddPageArrows(false);
        int initialized = 0;
        slots.ForEach(s => s.ClearSlot());
        for(int i = 0; i < artifacts.Count; i++){
            if(initialized >= slots.Count)
                break;
            if(artifacts[i].Rarity == currentRarity){
                slots[initialized].SetArtifact(artifacts[i]);
                initialized++;
            }
        }
        if(initialized == 0)
            AddEmptyMessage(true);
        else if(initialized >= slots.Count)
            AddPageArrows(true);
        if(artifacts.Count > 0)
            SelectArtifact(artifacts[0]);
    }

    public void SelectArtifact(ArtifactCountPair artifact){
        displayArtifactMenuEngine.SetArtifact(artifact);
    }

    private void AddEmptyMessage(bool state){
        emptyMsg.enabled = state;
    }

    private void AddPageArrows(bool state){
        rightArrow.enabled = state;
        leftArrow.enabled = state;
    }

}

