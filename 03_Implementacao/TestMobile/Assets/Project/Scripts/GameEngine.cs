using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEngine : MonoBehaviour
{
    /*
        *Runtime class
        Game Engine
        Loads the stages
        Controls the core of the game (enemies, heroes, rewards)
    */
    public WorldEngine worldEngine;
    public UIEngine uiEngine;
    public EventEngine eventEngine; 
    public CombatEngine combatEngine;
    public RewardEngine rewardEngine;
    public PenaltyEngine penaltyEngine;
    public ArtifactEngine artifactEngine;
    public List<TextMeshProUGUI> goldTexts = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> platinumTexts = new List<TextMeshProUGUI>();
    private void Start() {
        worldEngine = GameObject.FindGameObjectWithTag("WorldEngine").GetComponent<WorldEngine>();
        uiEngine = GameObject.FindGameObjectWithTag("UIEngine").GetComponent<UIEngine>();
        combatEngine = GameObject.FindGameObjectWithTag("CombatEngine").GetComponent<CombatEngine>();
        eventEngine = GameObject.FindGameObjectWithTag("EventEngine").GetComponent<EventEngine>();
        rewardEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<RewardEngine>();
        penaltyEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<PenaltyEngine>();
        artifactEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<ArtifactEngine>();
        //inventoryEngine.Initialize();
    }
    private void Update() {
        goldTexts.ForEach(gT => gT.text = combatEngine.team.inventory.gold.ToString()); //updates gold values
        platinumTexts.ForEach(pT => pT.text = combatEngine.team.inventory.platinum.ToString()); //updates plat values
    }
    //Selects a new stage and executes the respective methods to load them
    public void NewStageSelected(){
        switch(worldEngine.currentStage.stage.type){
            case Stage.StageType.COMBAT:
                artifactEngine.RunArtifacts(Artifact.ArtifactActivation.START_STAGE);
                StageCombat sc = (StageCombat)worldEngine.currentStage.stage;
                combatEngine.NewStageCombat(sc);    
            break;
            case Stage.StageType.EVENT:
                StageEvent se = (StageEvent)worldEngine.currentStage.stage;
                eventEngine.NewStageEvent(se);
                break;
            case Stage.StageType.MERCHANT:
            break;
        }
    }
    //Return current world name
    public string GetCurrentWorld(){
        return worldEngine.currentWorld.name;
    }
    //Executes the respective methods when a stage ends
    public void StageCompletedOrWorldEnded(bool playerWon){
        if(playerWon){
            worldEngine.CurrentStageIsCompleted();
            uiEngine.menuEngine.IncrementStat(MenuOptionsEngine.STATS.STAGES_COMPLETED);
        }
        else
            worldEngine.PlayerLostGame();
    }
    //Adds a new artifact and updates the menu
    public void AddArtifact(Artifact.ArtifactRarity rarity){
        Artifact artifact = artifactEngine.RequestNewArtifact(rarity);
        eventEngine.ShowNewArtifact(true, artifact);
        combatEngine.AddArtifact(artifact);
    }
}