using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class StageCombat : Stage
{
    /*
    Combat Stage
    Has 1-3 enemies
    Has 3 difficulties
    */
    public Enemy[] enemies;
    public enum CombatType {
        NORMAL,
        ELITE,
        BOSS
        
    }

    public StageCombat(int _pathNumber, CombatType _difficulty, string _world) : base(_pathNumber, StageType.COMBAT, _difficulty){
        InitializeEnemies(_world);
    }
    //Initializes all enemies of thiss tage
    public void InitializeEnemies(string _world){
        string[] enemyNames = EnemyEncounter.GetEnemyEncounter(difficulty, _world);
        enemies = new Enemy[enemyNames.GetLength(0)];
        for(int i = 0; i < enemyNames.GetLength(0); i++){
            enemies[i] = EnemyInitializer.InitializeEnemyWithName(enemyNames[i]);
        }
    }
    //Get difficulty of stage
    public static string GetTypeString(StageCombat.CombatType diff){
        switch(diff){
            case CombatType.NORMAL:
                return "normal";
            case CombatType.ELITE:
                return "elite";
            case CombatType.BOSS:
                return "boss";
        }
        return "";
    }
}
//Static class responsible for loading the enemies from a json file
public static class EnemyEncounter
{
    public static string[] GetEnemyEncounter(StageCombat.CombatType diff, string _world){
        Dictionary<string, Dictionary<string, string[]>> values = new Dictionary<string, Dictionary<string, string[]>>(); //Creates Dictionary for JSON
        List<Card> deck = new List<Card>(); //Temporary deck
        TextAsset mytxtData = (TextAsset)Resources.Load("data/worlds/" + _world + "/enemies"); //Load from Resources folder
        string txt = mytxtData.text;
        values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string[]>>>(txt); //Convert to JSON
        Dictionary<string, string[]> possibleEnemies = values[StageCombat.GetTypeString(diff)];
        int r = Random.Range(0, possibleEnemies.Count);
        return possibleEnemies[r.ToString()];
    }
}
