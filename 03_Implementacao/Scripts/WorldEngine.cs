using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class WorldEngine : MonoBehaviour
{
    /*
    Runtime class
    Main engine that controls the world generation of the game
    Initializing new worlds and controlling the progression of the player
    */
    public int currentLevel = 0;
    public int currentWorldCount = 1;
    public int currentWorldIndex = 0;
    List<World> worlds = new List<World>(); //list of worlds
    List<string> worldNames = new List<string>(); //list of world names to be generated
    public List<List<StageEngine>> stageEngines = new List<List<StageEngine>>(); //stage game objects to be associated with stages
    public StageEngine bossStageEngine; //boss stage
    public StageEngine currentStage; //current player selected stage
    public World currentWorld; //current player 
    private GameEngine gameEngine;
    private  UIEngine uiEngine;
    public float difficultyRamp = 0f; //difficulty ramp
    private void Start()
    {
        gameEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
        uiEngine = GameObject.FindGameObjectWithTag("UIEngine").GetComponent<UIEngine>();
        InitializeWorldNames();
        InitializeStageEngines();
        NewGame(); 
    }
    //Load a new selected stage
    public void LoadStage(StageEngine stageEn){
        if(gameEngine.uiEngine.menuEngine.currentStageType == MenuEngine.PLAYERSTAGE.OFF){
            currentStage = stageEn;
            gameEngine.NewStageSelected();
            uiEngine.SwitchScreen((stageEn.stage.type == Stage.StageType.COMBAT) ? UIEngine.Screen.STAGECOMBAT : UIEngine.Screen.STAGEEVENT);
        }   
    }
    //Initialize all stages engines objects
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
    //Initialize stage UI
    private void InitializeStageUI(){
        for(int i = 0; i < currentWorld.levels.Count; i++){
            for(int j = 0; j < currentWorld.levels[0].Count; j++){
                stageEngines[i][j].SetStage(currentWorld.levels[i][j]);
            }
        }
        bossStageEngine.SetStage(currentWorld.bossStage);
        //InitializeConnections();
    }
    //Updates the stage has completed and updates parameters like current level counter and difficulty ramp
    public void CurrentStageIsCompleted(){
        currentStage.IsCompleted();
        uiEngine.SwitchScreen(UIEngine.Screen.STAGESELECTOR);
        currentLevel++;
        difficultyRamp += 0.05f; 
        uiEngine.UpdateStageNumbersUI();
        if (currentStage.stage.difficulty == StageCombat.CombatType.BOSS)
           NewWorld();    
    }

    //Creates a new world
    public void NewWorld(){
        World world = new World(worldNames[(currentWorldIndex % worldNames.Count)], new float[] { stageEngines.Count, stageEngines[0].Count });
        currentWorld = world;
        worlds.Add(world);
        currentLevel = 0;
        difficultyRamp += 0.1f;
        currentWorldCount++;
        uiEngine.UpdateStageNumbersUI();
        InitializeStageUI();
    }
    //Executed when player loses the game resetting all 
    public void PlayerLostGame(){
        uiEngine.SwitchScreen(UIEngine.Screen.MAINSCREEN);
        NewGame();
        gameEngine.combatEngine.ResetCharacters();
    }
    //New game method loading a new world and updating the stage ui
    private void NewGame(){
        currentLevel = 0;
        currentWorldCount = 1;
        worlds.Clear();
        World world = new World(worldNames[0], new float[]{stageEngines.Count, stageEngines[0].Count});
        currentWorld = world;
        worlds.Add(world);
        InitializeStageUI();
    }
    //Load new world names from JSON text file
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
