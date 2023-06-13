using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class WorldEngine : MonoBehaviour
{
    public int currentLevel = 0;
    List<World> worlds = new List<World>();
    List<string> worldNames = new List<string>();
    public List<List<StageEngine>> stageEngines = new List<List<StageEngine>>();
    public StageEngine bossStageEngine;
    public StageEngine currentStage;
    public World currentWorld;
    private GameEngine gameEngine;
    private  UIEngine uiEngine;

    public void LoadStage(StageEngine stageEn){
        currentStage = stageEn;
        gameEngine.NewStageSelected();
        uiEngine.SwitchScreen((stageEn.stage.type == Stage.StageType.COMBAT)?UIEngine.Screen.STAGECOMBAT:UIEngine.Screen.STAGEEVENT);
    }

    private void Start() {
        gameEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
        uiEngine = GameObject.FindGameObjectWithTag("UIEngine").GetComponent<UIEngine>();
        InitializeWorldNames();
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
                stageEngines[en.level - 1].Add(en);
            }
            else
                bossStageEngine = en;
        }
    }

    private void InitializeStageUI(){
        for(int i = 0; i < currentWorld.levels.Count; i++){
            for(int j = 0; j < currentWorld.levels[0].Count; j++){
                stageEngines[i][j].SetStage(currentWorld.levels[i][j]);
            }
        }
        bossStageEngine.SetStage(currentWorld.bossStage);
        //InitializeConnections();
    }

    //TODO
    private void InitializeConnections(){
        for (int i = 0; i < stageEngines.Count; i++)
        {
            for (int j = 0; j < stageEngines[0].Count; j++)
            {
                for (int k = 0; k < stageEngines[0].Count; k++){
                    //if(i==5)
                    //else

                }
            }
        }
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
        World world = new World(worldNames[0], new float[]{stageEngines.Count, stageEngines[0].Count});
        currentWorld = world;
        worlds.Add(world);
        InitializeStageUI();
    }

    private void InitializeWorldNames(){
        Dictionary<string, string> worldsText = new Dictionary<string, string>();
        TextAsset mytxtData = (TextAsset)Resources.Load("data/worlds/worlds"); //Load from Resources folder
        string txt = mytxtData.text;
        worldsText = JsonConvert.DeserializeObject<Dictionary<string, string>>(txt); //Convert to JSON
        for(int i = 1; i <= worldsText.Count; i++){
            worldNames.Add(worldsText[i.ToString()]);
        }
    }
}
