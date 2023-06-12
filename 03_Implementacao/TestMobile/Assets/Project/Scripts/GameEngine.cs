using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start() {
        worldEngine = GameObject.FindGameObjectWithTag("WorldEngine").GetComponent<WorldEngine>();
        uiEngine = GameObject.FindGameObjectWithTag("UIEngine").GetComponent<UIEngine>();
        combatEngine = GameObject.FindGameObjectWithTag("CombatEngine").GetComponent<CombatEngine>();
        eventEngine = GameObject.FindGameObjectWithTag("EventEngine").GetComponent<EventEngine>();
    }
    public void NewStageSelected(){
        switch(worldEngine.currentStage.stage.type){
            case Stage.StageType.COMBAT:
                StageCombat sc = (StageCombat)worldEngine.currentStage.stage;
                combatEngine.NewStageCombat(sc);    
            break;
            case Stage.StageType.EVENT:
            break;
            case Stage.StageType.MERCHANT:
            break;
        }
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

    public void StageCompleted(bool playerWon){
        if(playerWon)
            worldEngine.CurrentStageIsCompleted();
        else
            worldEngine.PlayerLostGame();
    }
}