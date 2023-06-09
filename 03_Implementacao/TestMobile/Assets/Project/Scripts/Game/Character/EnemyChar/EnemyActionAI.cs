using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionAI
{
    /*
        This class is responsible for determining the attack
        to be used in the next turn of an enemy
        By comparing data it changes the priority of each attack
    */
    private const int MAX_ATTACKS = 4; //Max number of attacks
    private List<Card> enemyAttacks = new List<Card>(MAX_ATTACKS); //List of attacks
    private Card preparedAttack; //Current prepared attack to be used next turn
    private Dictionary<Card.Action_Type, float[]> priorityMoves = new Dictionary<Card.Action_Type, float[]>(); //Dictionary with priority for each attack type
    private Enemy enemy; //Associated enemy
    private GameEngine engine; //GameEngine reference
    private EnemyEngine enemyEngine; //Associated enemy's engine

    //Priority Options
    private enum PRIORITY_ACTION{
        Defense, //Focus on shielding or healing
        Status, //Focus on inflicting status effects
        Normal //Normal priority focused on damaging
    }
    
    
    // ~ Damage, Status, Defense, Special
    public EnemyActionAI(Enemy _enemy){
        this.enemy = _enemy;
        priorityMoves.Add(Card.Action_Type.Damage, GenerateRange(100f,35f));
        priorityMoves.Add(Card.Action_Type.Status, GenerateRange(35f, 5f));
        priorityMoves.Add(Card.Action_Type.Defense, GenerateRange(5f, 5f));
        priorityMoves.Add(Card.Action_Type.Special, GenerateRange(5f, 0));
    }

    //Runs the sequence to use attack and prepare the next one
    public void RunEnemyAI(){
        if (!engine.team.GameEnded()){
            PriorityUpdate();
            UseAttack();
            PrepareAttack();
        }
        
    }

    public void SetEngine(GameEngine _engine, EnemyEngine _enemyEngine){
        this.engine = _engine;
        this.enemyEngine = _enemyEngine;
        enemyEngine.SetNewPreparedAttack(preparedAttack);
    }
    //Adds attack do list
    public void AddAttack(Card c){
        enemyAttacks.Add(c);
        preparedAttack = enemyAttacks[0];
    }
    /*
        Attack method
        Determines if attack is single target or multi-target
        If single asks for a possible target to be targeted
        If multiple uses on them all
        If defensive uses on teammate    
    */
    private void UseAttack(){
        if(enemy.CheckActionForStatus(Character.Character_Action.USE_ATTACK)){
            if (!preparedAttack.area)
                preparedAttack.UseCardOnTarget(ChooseCharacterToCast());
            else
                if (preparedAttack.type != Card.Action_Type.Defense)
                    foreach (HeroEngine hero in engine.team.teamGO)
                        preparedAttack.UseCardOnTarget(hero.hero);
                else
                    foreach (EnemyEngine enemy in engine.enemies)
                        if (enemy.enemy != null && !enemy.enemy.diceased)
                            preparedAttack.UseCardOnTarget(enemy.enemy);
        }
    }

    //If defense and single cast on itself
    //Else chooses a random target to target
    private Character ChooseCharacterToCast(){
        if(preparedAttack.type == Card.Action_Type.Defense)
            return enemy;
        return ChooseTargetForAttack(preparedAttack.type);
    }

    //Randomly decides what type of attack to prepare
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

    //When the type is chosen, chooses an attack on the attack list
    private bool ChooseNextAttackByType(Card.Action_Type type){
        List<Card> tmp = new List<Card>();
        foreach(Card att in enemyAttacks){
            Card tmpC = att;
            if(att.type == type)
                tmp.Add(tmpC);
        }
        if(tmp.Count == 0)
            return false;
        int random = Random.Range(0, tmp.Count);
        preparedAttack = tmp[random];
        ChangeAttackValuesBasedOnModifier();
        enemyEngine.SetNewPreparedAttack(preparedAttack);
        return true;
    }

    //Every 5 turns the enemies attacks increase in value 
    private void ChangeAttackValuesBasedOnModifier()
    {
        Card c = null;
        switch (preparedAttack.type)
        {
            case Card.Action_Type.Damage:
                CardDamage cd = (CardDamage)preparedAttack;
                cd.currentDamage = Mathf.Round(cd.baseDamage*engine.difficultyModifier);
                c = (Card)cd;
                break;
            case Card.Action_Type.Defense:
                CardDefense cdef = (CardDefense)preparedAttack;
                cdef.currentAmmount = Mathf.Round(cdef.baseAmmount * engine.difficultyModifier);
                c = (Card) cdef;
                break;
            case Card.Action_Type.Status:
                CardStatus cs = (CardStatus)preparedAttack;
                cs.currentDamage = Mathf.Round(cs.baseDamage * engine.difficultyModifier);
                c = (Card)cs;
                break;
            case Card.Action_Type.Special:
                break;
        }
        preparedAttack = c;
    }

    //Method that updates the priority system
    private void PriorityUpdate(){
        if(CheckDefensePriority());
        else if(CheckStatusPriority());
        else
            SetPriorities(PRIORITY_ACTION.Normal);
    }

    //Checks if Defense priority should be chosen
    private bool CheckDefensePriority(){
        if(enemy.currentHealth <= enemy.maxHealth/2){
            SetPriorities(PRIORITY_ACTION.Defense);
            return true;
        }
        return false;
    }

    //Checks if Status priority should be chosen
    private bool CheckStatusPriority(){
        foreach (HeroEngine heroGO in engine.team.teamGO)
            if(heroGO.hero.debuffs.Count == 0){
                SetPriorities(PRIORITY_ACTION.Status);
                return true;
            }
        return false;
    }

    //Choose a target for attack if a target already has status try to find new target
    private Character ChooseTargetForAttack(Card.Action_Type type){
        if(type == Card.Action_Type.Defense)
            return enemy;
        else if(type == Card.Action_Type.Status){
            CardStatus tmp = (CardStatus) preparedAttack;
            foreach(HeroEngine heroGO in engine.team.teamGO){
                if(!heroGO.hero.diceased){
                    bool hasEffect = false;
                    foreach (StatusEffect eff in heroGO.hero.debuffs)
                        if (tmp.effect == eff.effect)
                            hasEffect = true;
                    if (!hasEffect)
                        return heroGO.hero;
                }
            }
        }
        Character att = engine.team.GetRandomHero(); //Ask for random target if all targets already have status
        return att;
    }

    //Changes priority system values
    private void SetPriorities(PRIORITY_ACTION priority){
        switch(priority){
            case PRIORITY_ACTION.Defense:
                priorityMoves[Card.Action_Type.Damage] = GenerateRange(100f, 55f);
                priorityMoves[Card.Action_Type.Status] = GenerateRange(55f, 50f);
                priorityMoves[Card.Action_Type.Defense] = GenerateRange(50f, 5f);
            break;
            case PRIORITY_ACTION.Status:
                priorityMoves[Card.Action_Type.Damage] = GenerateRange(100f, 55f);
                priorityMoves[Card.Action_Type.Status] = GenerateRange(55f, 5f);
                priorityMoves[Card.Action_Type.Defense] = GenerateRange(5f, 5f);
            break;
            case PRIORITY_ACTION.Normal:
                priorityMoves[Card.Action_Type.Damage] = GenerateRange(100f, 35f);
                priorityMoves[Card.Action_Type.Status] = GenerateRange(35f, 5f);
                priorityMoves[Card.Action_Type.Defense] = GenerateRange(5f, 5f);
            break;
        }
    }

    //If number is in range
    private bool IsInRange(float num, float[] range){
        return ((range[0] >= num && range[1] < num) || (num == 0 && range[1] == 5f));
    }
    
    //Generate a range
    private float[] GenerateRange(float range0, float range1){
        return new float[]{range0, range1};
    }
}
