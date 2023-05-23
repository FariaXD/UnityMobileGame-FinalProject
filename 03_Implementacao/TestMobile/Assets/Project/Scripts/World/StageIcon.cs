using UnityEngine;

public class StageIcon : MonoBehaviour {
    
    public Sprite normalEncounter, normalEncounterDone;
    public Sprite eliteEncounter, eliteEncounterDone;
    public Sprite bossEncounter, bossEncounterDone;
    public Sprite eventEncounter, eventEncounterDone;
    public Sprite merchantEncounter, merchantEncounterDone;
    public Sprite secretEncounter;

    public Sprite LoadImageFromType(Stage stage){
        switch(stage.type){
            case Stage.StageType.COMBAT:
                switch(stage.difficulty){
                    case StageCombat.CombatType.NORMAL:
                    return (stage.completed) ? normalEncounterDone : normalEncounter;
                    case StageCombat.CombatType.ELITE:
                    return (stage.completed) ? eliteEncounterDone : eliteEncounter;
                    case StageCombat.CombatType.BOSS:
                    return (stage.completed) ? bossEncounterDone : bossEncounter;
                }
                return (stage.completed) ? normalEncounterDone : normalEncounter;
            case Stage.StageType.EVENT:
            return (stage.completed) ? eventEncounterDone : eventEncounter;
            case Stage.StageType.MERCHANT:
            return (stage.completed) ? merchantEncounterDone : merchantEncounter;
        }
        return secretEncounter;
    }
}