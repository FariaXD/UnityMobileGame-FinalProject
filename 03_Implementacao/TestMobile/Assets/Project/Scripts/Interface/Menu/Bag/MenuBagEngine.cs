using UnityEngine;
using System.Collections.Generic;
public class MenuBagEngine : MonoBehaviour {

    /*
    Runtime class responsible for managing the interity of the artifact menu
    */
    List<ArtifactCountPair> artifactsCount = new List<ArtifactCountPair>(); //all artifacts and their counts
    public InventoryMenuEngine inventoryMenuEngine; //the inventory menu engine
    private void Start() {
        inventoryMenuEngine = GameObject.FindGameObjectWithTag("MenuBagInventoryEngine").GetComponent<InventoryMenuEngine>();
    }
    //Initializes all artifacts while determining how many of them are in the inventory
    public void Initialize(List<Artifact> artifacts) {
        artifactsCount.Clear(); //clears list
        for(int i = 0; i < artifacts.Count; i++){
            int count = 0;
            for (int j = 0; j < artifacts.Count; j++)
            {
                if(artifacts[i].name == artifacts[j].name && artifacts[i].rarity == artifacts[j].rarity)
                    count++;
            }
            artifactsCount.Add(new ArtifactCountPair(artifacts[i], count, artifacts[i].rarity)); //adds the artifact and its count
        }
        if(!inventoryMenuEngine.initialized)
            inventoryMenuEngine.Initialize(artifactsCount); //initializes the inventory manager if not already initialized
        else
            inventoryMenuEngine.UpdateArtifacts(artifactsCount); //updates artifacts if already initialized
    }
    //Switch rarity
    public void SwitchRarity(Artifact.ArtifactRarity rarity){
        inventoryMenuEngine.SwitchRarity(rarity);
    }
}

//Extra data class that contains artifacts and their count
public class ArtifactCountPair
{
    public Artifact Artifact { get; private set; }
    public int Count { get; private set; }
    public Artifact.ArtifactRarity Rarity {get; private set; }
    public ArtifactCountPair(Artifact art, int count, Artifact.ArtifactRarity rarity)
    {
        Artifact = art;
        Count = count;
        Rarity = rarity;
    }
}