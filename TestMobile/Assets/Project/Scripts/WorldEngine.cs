using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEngine : MonoBehaviour
{
    List<World> worlds = new List<World>();
    public List<StageEngine> stageEngines = new List<StageEngine>();
    public Stage currentStage;
    public World currentWorld;

    private GameEngine gameEngine;
    private  UIEngine uiEngine;

    public void LoadStage(Stage stage){
        currentStage = stage;
        gameEngine.NewStageSelected();
        uiEngine.SwitchScreen(UIEngine.Screen.STAGE);
    }

    private void Start() {
        gameEngine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
        uiEngine = GameObject.FindGameObjectWithTag("UIEngine").GetComponent<UIEngine>();
        World world = new World("Mystical Forest");
        currentWorld = world;
        worlds.Add(world);
        InitializeStageUI();
    }

    private void InitializeStageUI(){
        for(int i = 0; i < currentWorld.stages.Count; i++){
            stageEngines[i].SetStage(currentWorld.stages[i]);
        }
    }
}
