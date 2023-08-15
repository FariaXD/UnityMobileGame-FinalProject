using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    public string name; //Name of char
    public float maxHealth; //Max health of char
    public float currentHealth; //Current health of char
    public float shield; //Current shield of char
    public float maxShield;
    public RuntimeAnimatorController anim; //Animation controller
    public bool diceased = false; //If character is dead
    public AttackIcon icons; //Icons
    public bool damageTaken = false; 

    public List<StatusEffect> debuffs = new List<StatusEffect>(); //List containing all the status affecting the character


    public enum Character_Action {
        USE_ATTACK, //When character uses card or attack
        END_TURN //When turn ends
    }

    //Constructor
    public Character(string _name, float _maxHealth, float _shield, RuntimeAnimatorController _anim = default(RuntimeAnimatorController)){
        this.name = _name;
        this.maxHealth = _maxHealth;
        this.currentHealth = _maxHealth;
        this.maxShield = _shield;
        this.shield = _shield;
        this.anim = _anim;
        this.icons = new AttackIcon();
    }
    
    //Receive damage
    public void TakeDamage(float damage){
        float damageToShield;
        if(shield>damage){
            damageToShield = shield;
            shield = shield-damage;
            damageToShield = damageToShield-shield;
        }
        else{
            damageToShield = shield;
            shield = 0;
        }
        currentHealth = currentHealth - (damage-damageToShield);
        damageTaken = true;
    }
    //Receive defense type (shield or heal)
    public void GetDefense(float ammount, CardDefense.Defense_Type type)
    {
        if(type == CardDefense.Defense_Type.Healing)
            currentHealth = (currentHealth + ammount > maxHealth)? maxHealth : currentHealth + ammount;
        else
            shield += ammount;
    }
    /*
        Receive status effect
        Check for existing and increase duration
    */
    public void GetStatus(StatusEffect effect){
        bool contains = false;
        foreach(StatusEffect debuff in debuffs){
            if(effect.effect == debuff.effect){
                contains = true;
                debuff.duration += effect.duration;
            }
        }
        if(!contains)
            debuffs.Add(effect);
    }
    //Reduces each status effect duration by a turn
    public void ReduceStatusEffectDurations(){
        debuffs.ForEach(debuff => debuff.duration -= 1);
        List<StatusEffect> tmp = new List<StatusEffect>(debuffs);
        for(int i = tmp.Count - 1; i >= 0; i--){
            if(tmp[i].duration <= 0)
                debuffs.RemoveAt(i);
        }
    }

    //Returns true if the user cant use attacks
    public bool CheckActionForStatus(Character_Action action){
        switch(action){
            case Character_Action.USE_ATTACK:
                foreach(StatusEffect debuff in debuffs)
                    if (debuff.effect == StatusEffect.Effect.Shock)
                        return false; // cant use cards
                    else if(debuff.effect == StatusEffect.Effect.Poison)
                        TakeDamage(Mathf.Round(maxHealth*0.03f));
            break;
            case Character_Action.END_TURN:
            foreach(StatusEffect debuff in debuffs)
                if(debuff.effect == StatusEffect.Effect.Burn)
                    TakeDamage(Mathf.Round(maxHealth*0.1f));
            break;
        }
        return true;
    }

    public void ClearStatus(){
        debuffs.Clear();
    }

    //New Stage clears debuffs
    public void NewStage(){
        debuffs.Clear();
        float newHealth = currentHealth + maxHealth * 0.25f;
        currentHealth = (newHealth > maxHealth) ? maxHealth : newHealth;
    }
}
