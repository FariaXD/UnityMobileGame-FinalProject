using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack
{
    private const int MAX_ATTACKS = 4;
    List<Card> enemyAttacks = new List<Card>(MAX_ATTACKS);

    public void AddAttack(Card c){
        enemyAttacks.Add(c);
    }


    //TODO AI CHOOSE BEST OPTION OR RANDOM

}
