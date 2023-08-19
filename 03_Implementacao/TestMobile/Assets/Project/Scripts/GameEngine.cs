using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEngine : MonoBehaviour
{
    /*
        *Runtime class
        Game Engine
        Controls the State machine of the game
        Loads the stage
    */
    public WorldEngine worldEngine;
    public UIEngine uiEngine;
    public EventEngine eventEngine; 
    public CombatEngine combatEngine;
    public RewardEngine rewardEngine;
    public PenaltyEngine penaltyEngine;
    public ArtifactEngine artifactEngine;
    public MenuEngine menuEngine;
    public List<TextMeshProUGUI> goldTexts = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> platinumTexts = new List<TextMeshProUGUI>();
    private void Start() {
        worldEngine = GameObject.FindGameObjectWithTag("WorldEngine").GetComponent<WorldEngine>();
        uiEngine = GameObject.FindGameObjectWithTag("UIEngine").GetComponent<UIEngine>();
        menuEngine = GameObject.FindGameObjectWithTag("MenuEngine").GetComponent<MenuEngine>();
        combatEngine = GameObject.FindGameObjectWithTag("CombatEngine").GetComponent<CombatEngine>();
        eventEngine = GameObject.FindGameObjectWithTag("EventEngine").GetComponent<EventEngine>();
        rewardEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<RewardEngine>();
        penaltyEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<PenaltyEngine>();
        artifactEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<ArtifactEngine>();
        //inventoryEngine.Initialize();
    }
    private void Update() {
        goldTexts.ForEach(gT => gT.text = combatEngine.team.inventory.gold.ToString());
        platinumTexts.ForEach(pT => pT.text = combatEngine.team.inventory.platinum.ToString());
    }
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

    public string GetCurrentWorld(){
        return worldEngine.currentWorld.name;
    }

    private void SwitchComponentsStage(Stage.StageType type){
        switch (type)
        {
            case Stage.StageType.COMBAT:
                break;
            case Stage.StageType.EVENT:
                break;
            case Stage.StageType.MERCHANT:
                break;
        }
    }

    public void StageCompletedOrWorldEnded(bool playerWon){
        if(playerWon){
            worldEngine.CurrentStageIsCompleted();
            menuEngine.IncrementStat(MenuOptionsEngine.STATS.STAGES_COMPLETED);
        }
        else
            worldEngine.PlayerLostGame();
    }

    public void AddArtifact(Artifact.ArtifactRarity rarity){
        Artifact artifact = artifactEngine.RequestNewArtifact(rarity);
        eventEngine.ShowNewArtifact(true, artifact);
        combatEngine.AddArtifact(artifact);
    }
}