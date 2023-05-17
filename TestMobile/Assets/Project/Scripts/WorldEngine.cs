using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEngine : MonoBehaviour
{
    List<World> worlds = new List<World>();
    public Stage currentStage;

    public GameEngine gameEngine;

    public void LoadStage(Stage stage){
        currentStage = stage;
    }
}
