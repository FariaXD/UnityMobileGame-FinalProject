using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    /*
    Class that holds all the information about a world including events and stages
    Each level has 1-3 stages
    */
    public List<List<Stage>> levels = new List<List<Stage>>(); //list of stages
    List<Event> events = new List<Event>(); //list of events
    public Stage bossStage; //final stage
    public string name; //world name
    public Dictionary<Stage.StageType, float> stageProb = new Dictionary<Stage.StageType, float>(); //stage probabilities
    private const float eliteProb = 25f; // chance of spawning an elite difficulty stage
    private const float spawnStageProb = 50f; // chance of spawning a stage

    public World(string _name, float[] _array){
        this.name = _name;
        stageProb.Add(Stage.StageType.COMBAT, 80f); //adds the chance of a combat stage
        stageProb.Add(Stage.StageType.EVENT, 20f); //adds the change of an event stage
        events = StageEventInitializer.InitializeEvent(name); //initialize events
        GenerateLevels(_array);
    }
    //Feel levels with empty stages
    private void GenerateList(float[] size){
        for(int i = 0; i < size[0]; i++){
            List<Stage> newLevel = new List<Stage>();
            for(int j = 0; j < size[1]; j++){
                newLevel.Add(new StageEmpty(i));
            }
            levels.Add(newLevel);
        }
    }
    //Generate all the levels of a world given the initialized probabilities
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
    //Return a new generated stage
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
