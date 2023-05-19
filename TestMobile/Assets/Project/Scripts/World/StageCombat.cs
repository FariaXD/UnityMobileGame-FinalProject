using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class StageCombat : Stage
{
    public Enemy[] enemies;
    public enum CombatType {
        NORMAL,
        ELITE,
        BOSS
        
    }
    public CombatType difficulty;

    public StageCombat(CombatType _difficulty) : base(StageType.COMBAT){
        this.difficulty = _difficulty;
        InitializeEnemies();
    }

    public void InitializeEnemies(){
        string[] enemyNames = EnemyEncounter.GetEnemyEncounter(difficulty);
        enemies = new Enemy[enemyNames.GetLength(0)];
        for(int i = 0; i < enemyNames.GetLength(0); i++){
            enemies[i] = EnemyInitializer.InitializeEnemyWithName(enemyNames[i]);
        }
    }

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

public static class EnemyEncounter
{
    public static string[] GetEnemyEncounter(StageCombat.CombatType diff){
        Dictionary<string, Dictionary<string, string[]>> values = new Dictionary<string, Dictionary<string, string[]>>(); //Creates Dictionary for JSON
        List<Card> deck = new List<Card>(); //Temporary deck
        TextAsset mytxtData = (TextAsset)Resources.Load("data/enemies/enemies"); //Load from Resources folder
        string txt = mytxtData.text;
        values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string[]>>>(txt); //Convert to JSON
        Dictionary<string, string[]> possibleEnemies = values[StageCombat.GetTypeString(diff)];
        int r = Random.Range(0, possibleEnemies.Count);
        return possibleEnemies[r.ToString()];
    }
}
