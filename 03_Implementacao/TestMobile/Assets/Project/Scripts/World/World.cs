using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public List<List<Stage>> levels = new List<List<Stage>>();
    public Stage bossStage;
    public string name;
    public const int NUM_OF_ROUNDS = 4;
    public Dictionary<Stage.StageType, float> stageProb = new Dictionary<Stage.StageType, float>();
    private const float eliteProb = 25f;
    private const float spawnStageProb = 50f;


    public World(string _name, float[] _array){
        this.name = _name;
        stageProb.Add(Stage.StageType.COMBAT, 80f);
        stageProb.Add(Stage.StageType.EVENT, 20f);
        GenerateLevels(_array);
    }

    private void GenerateList(float[] size){
        for(int i = 0; i < size[0]; i++){
            List<Stage> newLevel = new List<Stage>();
            for(int j = 0; j < size[1]; j++){
                newLevel.Add(new StageEmpty(i));
            }
            levels.Add(newLevel);
        }
    }
    private void GenerateLevels(float[] size){
        GenerateList(size);
        int levelCount = 0;
        foreach(List<Stage> stages in levels){
            int spawned = 0;
            for(int i = 0; i < stages.Count; i++){
                float r = Random.Range(0f, 100f);
                if (r <= spawnStageProb){
                    stages[i] = GenerateStages(levelCount);
                    spawned++;
                }
            }
            //In case no stages are spawned in level
            if(spawned == 0){
                int f = Random.Range(0,Mathf.RoundToInt(size[1]));
                stages[f] = GenerateStages(levelCount);
            }
            levelCount++;
        }
        bossStage = new StageCombat(levelCount, StageCombat.CombatType.BOSS, name);
    }

    //TODO EVENT AND SECRET
    public Stage GenerateStages(int level){
        float r = Random.Range(0f, 100f);
        if(r <= eliteProb)
            return new StageCombat(level, StageCombat.CombatType.ELITE, name);
        else
            return new StageCombat(level, StageCombat.CombatType.NORMAL, name);
    }
}
