using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAI
{
    private const int MAX_ATTACKS = 4;
    private List<Card> enemyAttacks = new List<Card>(MAX_ATTACKS);
    private Card preparedAttack;
    private Dictionary<Card.Action_Type, float[]> priorityMoves = new Dictionary<Card.Action_Type, float[]>();
    private Enemy enemy;
    private Hero selectedTarger = null;
    private GameEngine engine;

    private enum PRIORITY_ACTION{
        Defense,
        Status,
        Normal
    }
    
    
    // ~ Damage, Status, Defense, Special
    public EnemyAttackAI(Enemy _enemy){
        this.enemy = _enemy;
        priorityMoves.Add(Card.Action_Type.Damage, GenerateRange(100f,50f));
        priorityMoves.Add(Card.Action_Type.Status, GenerateRange(50f, 20f));
        priorityMoves.Add(Card.Action_Type.Defense, GenerateRange(20f, 5f));
        priorityMoves.Add(Card.Action_Type.Special, GenerateRange(5f, 0));
    }

    public void RunEnemyAI(){
        if (!engine.team.GameEnded()){
            PriorityUpdate();
            UseAttack();
            PrepareAttack();
        }
        
    }

    public void SetEngine(GameEngine _engine){
        this.engine = _engine;
    }

    public void AddAttack(Card c){
        enemyAttacks.Add(c);
        preparedAttack = enemyAttacks[0];
    }

    private void UseAttack(){
        if (!preparedAttack.area)
            preparedAttack.UseCardOnTarget(ChooseCharacterToCast());
        else
            if(preparedAttack.type != Card.Action_Type.Defense)
                foreach (HeroEngine hero in engine.team.teamGO)
                    preparedAttack.UseCardOnTarget(hero.hero);
            else
                foreach (EnemyEngine enemy in engine.enemies)
                    if(!enemy.enemy.diceased)
                        preparedAttack.UseCardOnTarget(enemy.enemy);
        
    }

    private Character ChooseCharacterToCast(){
        if(preparedAttack.type == Card.Action_Type.Defense)
            return enemy;
        return ChooseTargetForAttack(preparedAttack.type);
    }

    private void PrepareAttack(){
        bool chosen = false;
        while(!chosen){
            int random = Random.Range(0, 100);
            if (IsInRange(random, priorityMoves[Card.Action_Type.Damage]))
                chosen = ChooseNextAttackByType(Card.Action_Type.Damage);
            else if(IsInRange(random, priorityMoves[Card.Action_Type.Status]))
                chosen = ChooseNextAttackByType(Card.Action_Type.Status);
            else if (IsInRange(random, priorityMoves[Card.Action_Type.Defense]))
                chosen = ChooseNextAttackByType(Card.Action_Type.Defense);
            else if (IsInRange(random, priorityMoves[Card.Action_Type.Special]))
                chosen = ChooseNextAttackByType(Card.Action_Type.Special);
        }
    }

    private bool ChooseNextAttackByType(Card.Action_Type type){
        List<Card> tmp = new List<Card>();
        foreach(Card att in enemyAttacks){
            if(att.type == type)
                tmp.Add(att);
        }
        if(tmp.Count == 0)
            return false;
        int random = Random.Range(0, tmp.Count);
        preparedAttack = tmp[random];
        return true;
    }

    private void PriorityUpdate(){
        if(CheckDefensePriority());
        else if(CheckStatusPriority());
        else
            SetPriorities(PRIORITY_ACTION.Normal);
    }

    private bool CheckDefensePriority(){
        if(enemy.currentHealth <= enemy.maxHealth/2){
            SetPriorities(PRIORITY_ACTION.Defense);
            return true;
        }
        return false;
    }

    private bool CheckStatusPriority(){
        foreach (HeroEngine heroGO in engine.team.teamGO)
            if(heroGO.hero.debuffs.Count == 0){
                SetPriorities(PRIORITY_ACTION.Status);
                return true;
            }
        return false;
    }

    private Character ChooseTargetForAttack(Card.Action_Type type){
        if(type == Card.Action_Type.Defense)
            return enemy;
        else if(type == Card.Action_Type.Status){
            CardStatus tmp = (CardStatus) preparedAttack;
            foreach(HeroEngine heroGO in engine.team.teamGO){
                bool hasEffect = false;
                foreach(StatusEffect eff in heroGO.hero.debuffs){
                    if(tmp.effect == eff.effect)
                        hasEffect = true;
                }
                if(!hasEffect)
                    return heroGO.hero;
            }
        }
        Character att = engine.team.GetRandomHero();
        return att;
    }

    private void SetPriorities(PRIORITY_ACTION priority){
        switch(priority){
            case PRIORITY_ACTION.Defense:
                priorityMoves[Card.Action_Type.Damage] = GenerateRange(100f, 55f);
                priorityMoves[Card.Action_Type.Status] = GenerateRange(55f, 50f);
                priorityMoves[Card.Action_Type.Defense] = GenerateRange(50f, 5f);
            break;
            case PRIORITY_ACTION.Status:
                priorityMoves[Card.Action_Type.Damage] = GenerateRange(100f, 65f);
                priorityMoves[Card.Action_Type.Status] = GenerateRange(65f, 15f);
                priorityMoves[Card.Action_Type.Defense] = GenerateRange(15f, 5f);
            break;
            case PRIORITY_ACTION.Normal:
                priorityMoves[Card.Action_Type.Damage] = GenerateRange(100f, 50f);
                priorityMoves[Card.Action_Type.Status] = GenerateRange(50f, 20f);
                priorityMoves[Card.Action_Type.Defense] = GenerateRange(20f, 5f);
            break;
        }
    }

    private bool IsInRange(float num, float[] range){
        return ((range[0] >= num && range[1] < num) || (num == 0 && range[1] == 5f));
    }
    private float[] GenerateRange(float range0, float range1){
        return new float[]{range0, range1};
    }

    /*
    TODO - PRIORITY SYSTEM, based on the state of the game set each action type as a priority

    & Attack Priority
    ^ - Special priority is constant at 5%
    * - Defense priority depends on the current health of the enemy and the average enemy party health  
        ~ If current health <45% or average team health <55% - Priority (30,30,25,5)
    ? - Status priority changes based on the heroes currently afflicting status effects
        ~ If all targets are afflicted with a status effect - Priority (70,5,25,5)
    ! - Damage has the most priority out of the 4.

    & Target Priority
    ? - If character already has a status then try to find another target, if all targets have status, refresh
    ! - Will try to target low health first but not always
    * - Can only self heal or area heal
    ^ - To be determined
    */


}
