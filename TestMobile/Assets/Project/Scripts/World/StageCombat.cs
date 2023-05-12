using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCombat : Stage
{
    public List<Enemy> enemies = new List<Enemy>();
    public enum CombatType {
        NORMAL,
        ELITE,
        BOSS
    }
    public CombatType difficulty;

    public StageCombat(CombatType _difficulty) : base(){
        this.difficulty = _difficulty;
    }

    public void InitializeEnemies(){
        
    }
}

public class EnemyEncounter
{
    
}
