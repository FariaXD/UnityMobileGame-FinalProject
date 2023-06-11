using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEngine : MonoBehaviour
{
    public int currentLevel = 0;
    List<World> worlds = new List<World>();
    public List<List<StageEngine>> stageEngines = new List<List<StageEngine>>();
    public StageEngine bossStageEngine;
    public StageEngine currentStage;
    public World currentWorld;
    private GameEngine gameEngine;
    private  UIEngine uiEngine;

    public void LoadStage(StageEngine stageEn){
        currentStage = stageEn;
        gameEngine.NewStageSelected();
        uiEngine.SwitchScreen(UIEngine.Screen.STAGE);
    }

    private void Start() {
        gameEngine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
        uiEngine = GameObject.FindGameObjectWithTag("UIEngine").GetComponent<UIEngine>();
        InitializeStageEngines();
        NewGame();
    }

    private void InitializeStageEngines(){
        GameObject[] engines = GameObject.FindGameObjectsWithTag("StageEngine");
        int numbOfStages = (engines.Length - 1) / 3;
        for(int i = 0; i < numbOfStages; i++){
            stageEngines.Add(new List<StageEngine>());
        }
        foreach(GameObject engine in engines){
            StageEngine en = engine.GetComponent<StageEngine>();
            if(en.level != ((engines.Length - 1) / 3) + 1){
                stageEngines[en.level - 1].Insert(en.stagePos-1, en);
            }
            else
                bossStageEngine = en;
        }
        //stageEngines.ForEach(e => e.ForEach(en => Debug.Log(en.level + " " + en.stagePos)));        

    }

    private void InitializeStageUI(){
        for(int i = 0; i < currentWorld.levels.Count; i++){
            for(int j = 0; j < currentWorld.levels[0].Count; j++){
                stageEngines[i][j].SetStage(currentWorld.levels[i][j]);
            }
        }
        bossStageEngine.SetStage(currentWorld.bossStage);
    }

    public void CurrentStageIsCompleted(){
        currentStage.IsCompleted();
        uiEngine.SwitchScreen(UIEngine.Screen.STAGESELECTOR);
        currentLevel++;
    }

    public void PlayerLostGame(){
        uiEngine.SwitchScreen(UIEngine.Screen.MAINSCREEN);
        NewGame();
    }

    private void NewGame(){
        currentLevel = 0;
        worlds.Clear();
        World world = new World("Mystical Forest", new float[]{stageEngines.Count, stageEngines[0].Count});
        currentWorld = world;
        worlds.Add(world);
        InitializeStageUI();
    }
}
