using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int gold = 0;
    public int platinum = 0;
    public List<Artifact> artifacts = new List<Artifact>();
    //Adds new artifact
    public void AddArtifact(Artifact artifact){
        artifacts.Add(artifact);
    }
    //Adds a gold ammount
    public void AddGold(int gold){
        this.gold += gold;
    }
    //Adds a platinum ammount
    public void AddPlatinum(int platinum){
        this.platinum += platinum;
    }
}
