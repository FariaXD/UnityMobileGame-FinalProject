using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPowersEngine : MonoBehaviour{

    /*
    *Runtime Class
    Dynamic method used to run an artifact ability based on the method name received
    */
    private ArtifactEngine artEngine;

    private void Start() {
        artEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<ArtifactEngine>();
    }
    //Execute via string
    public void ExecuteArtifactPower(string method){
        Invoke(method, 0f);
    }
    //Execute via given Artifact
    public void ExecuteArtifactPower(Artifact art){
        Invoke(art.method, 0f);
    }
    //Specific Artifact Powers
    public void MagicStick(){
        int shield = 5;
        artEngine.gameEngine.combatEngine.team.ShieldHeroesAmmount(shield);
    }
    
}