using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public List<List<Stage>> levels = new List<List<Stage>>();
    List<Event> events = new List<Event>();
    public Stage bossStage;
    public string name;
    public Dictionary<Stage.StageType, float> stageProb = new Dictionary<Stage.StageType, float>();
    private const float eliteProb = 25f;
    private const float spawnStageProb = 50f;
    private Dictionary<Stage.StageType, float> stageChance = new Dictionary<Stage.StageType, float>(); 


    public World(string _name, float[] _array){
        this.name = _name;
        stageProb.Add(Stage.StageType.COMBAT, 80f);
        stageProb.Add(Stage.StageType.EVENT, 20f);
        events = StageEventInitializer.InitializeEvent(name);
        stageChance.Add(Stage.StageType.COMBAT, 80f);
        stageChance.Add(Stage.StageType.EVENT, 20f);
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

    public Stage GenerateStages(int level){
        float stageType = Random.Range(0f, 100f);
        if(stageType <= stageProb[Stage.StageType.EVENT] && events.Count > 0){
            int e = Random.Range(0, events.Count);
            Event newEvent = events[e];
            events.RemoveAt(e);
            return new StageEvent(level, newEvent);
        }
        else{
            float r = Random.Range(0f, 100f);
            if (r <= eliteProb)
                return new StageCombat(level, StageCombat.CombatType.ELITE, name);
            else
                return new StageCombat(level, StageCombat.CombatType.NORMAL, name);
        }  
    }
}
