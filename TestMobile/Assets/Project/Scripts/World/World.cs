using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public List<Stage> stages = new List<Stage>();
    public string name;

    public const int NUM_OF_ROUNDS = 4;
    public Dictionary<Stage.StageType, float> stageProb = new Dictionary<Stage.StageType, float>();
    private const float eliteProb = 25f;


    public World(string _name){
        this.name = _name;
        stageProb.Add(Stage.StageType.COMBAT, 80f);
        stageProb.Add(Stage.StageType.EVENT, 20f);
        GenerateStages();
    }

    public void GenerateStages(){
        stages.Add(new StageCombat(StageCombat.CombatType.NORMAL));
        for(int i = 0; i < NUM_OF_ROUNDS - 2; i++){
            float r = Random.Range(0f, 100f);
            if(r <= eliteProb)
                stages.Add(new StageCombat(StageCombat.CombatType.ELITE));
            else
                stages.Add(new StageCombat(StageCombat.CombatType.NORMAL));
        }
        stages.Add(new StageCombat(StageCombat.CombatType.BOSS));
    }
}
