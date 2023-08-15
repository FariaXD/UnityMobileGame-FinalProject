using UnityEngine;
using System.Collections.Generic;
public class MenuBagEngine : MonoBehaviour {
    List<ArtifactCountPair> artifactsCount = new List<ArtifactCountPair>();
    public InventoryMenuEngine inventoryMenuEngine;
    private void Start() {
        inventoryMenuEngine = GameObject.FindGameObjectWithTag("MenuBagInventoryEngine").GetComponent<InventoryMenuEngine>();
    }

    public void Initialize(List<Artifact> artifacts) {
        artifactsCount.Clear();
        for(int i = 0; i < artifacts.Count; i++){
            int count = 0;
            for (int j = 0; j < artifacts.Count; j++)
            {
                if(artifacts[i].name == artifacts[j].name && artifacts[i].rarity == artifacts[j].rarity)
                    count++;
            }
            artifactsCount.Add(new ArtifactCountPair(artifacts[i], count, artifacts[i].rarity));
        }
        if(!inventoryMenuEngine.initialized)
            inventoryMenuEngine.Initialize(artifactsCount);
        else
            inventoryMenuEngine.UpdateArtifacts(artifactsCount);
    }

    public void SwitchRarity(Artifact.ArtifactRarity rarity){
        inventoryMenuEngine.SwitchRarity(rarity);
    }
}

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