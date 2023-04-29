using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public EnemyAttackAI enemyAI;
    public Enemy(string _name, float _health, float _shield, RuntimeAnimatorController _anim = default(RuntimeAnimatorController)) : base(_name, _health, _shield, _anim)
    {
        enemyAI = new EnemyAttackAI(this);
    }
}
