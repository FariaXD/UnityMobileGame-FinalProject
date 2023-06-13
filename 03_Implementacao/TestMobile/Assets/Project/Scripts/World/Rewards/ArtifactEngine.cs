using System.Collections.Generic;
using UnityEngine;

public class ArtifactEngine : MonoBehaviour {
    private ArtifactPowers artPowers = new ArtifactPowers();
    private GameEngine engine;
    private void Start() {
        engine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();

    }
    public Artifact RequestNewArtifact(Artifact.ArtifactRarity rarity) {
        List<Artifact> newArtifacts = ArtifactInitializer.GetArtifactsByRarity(engine.GetCurrentWorld(),Artifact.GetStringOfRarity(rarity));
        Debug.Log(newArtifacts[0].name + " " + newArtifacts[0].description + " " + newArtifacts[0].method);
        return newArtifacts[0];
    }
}