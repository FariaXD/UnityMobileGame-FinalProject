using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    /*
        Enemy class containing enemy info and AI
    */
    public EnemyActionMachine enemyActionMachine;
    public Enemy(string _name, float _health, float _shield, RuntimeAnimatorController _anim = default(RuntimeAnimatorController)) : base(_name, _health, _shield, _anim)
    {
        enemyActionMachine = new EnemyActionMachine(this);
        icons = new AttackIcon();
    }
}
