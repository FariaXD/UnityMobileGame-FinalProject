using System.Collections.Generic;
using UnityEngine;

public class ArtifactEngine : MonoBehaviour {
    private List<Artifact> givenArtifacts = new List<Artifact>();
    public GameEngine gameEngine;
    private ArtifactPowersEngine artPowers;

    private void Start() {
        gameEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
        artPowers = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<ArtifactPowersEngine>();

    }
    /**
    Requests a new artifact based on the rarity received
    Uncomment comments to make them unstackable
    */
    public Artifact RequestNewArtifact(Artifact.ArtifactRarity rarity) {
        List<Artifact> newArtifacts = ArtifactInitializer.GetArtifactsByRarity(gameEngine.GetCurrentWorld(),Artifact.GetStringOfRarity(rarity));
        //bool found = true;
        //while(found){ //Until it finds an artifact
        //found = false;
        int r = Random.Range(0, newArtifacts.Count);
        Artifact art = newArtifacts[r];
        /* foreach(Artifact given in givenArtifacts)
            if(art.name == given.name)
                found = true; */
        givenArtifacts.Add(art);
        if(art.GetArtifactActivation() == Artifact.ArtifactActivation.PASSIVE)
            RunArtifact(art); //If its a passive effect runs the artifact function
        return art;
        //}
        //return newArtifacts[0];
    }

    /*  Runs all the artifacts based on the state received
        Example Start Turn
    */
    public void RunArtifacts(Artifact.ArtifactActivation state) {
        foreach(Artifact given in givenArtifacts) 
            if(state == given.GetArtifactActivation())
                artPowers.ExecuteArtifactPower(given.method);
    }
    //Runs a singular artifact used on PASSIVE artifacts that are activated once
    public void RunArtifact(Artifact art){
        artPowers.ExecuteArtifactPower(art);
    }
}