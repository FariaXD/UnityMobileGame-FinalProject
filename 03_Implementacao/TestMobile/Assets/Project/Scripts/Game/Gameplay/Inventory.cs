using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int gold = 0;
    public int platinum = 0;
    public List<Artifact> artifacts = new List<Artifact>();
 
    public void AddArtifact(Artifact artifact){
        artifacts.Add(artifact);
    }

    public void AddGold(int gold){
        this.gold += gold;
    }
    public void AddPlatinum(int platinum){
        this.platinum += platinum;
    }
}
