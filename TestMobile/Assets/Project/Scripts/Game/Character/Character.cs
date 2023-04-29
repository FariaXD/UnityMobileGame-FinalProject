using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    public string name;
    public float maxHealth;
    public float currentHealth;
    public float shield;
    public RuntimeAnimatorController anim;
    //List containing all the status affecting the character
    public List<StatusEffect> debuffs = new List<StatusEffect>(); 
    public Character(string _name, float _maxHealth, float _shield, RuntimeAnimatorController _anim = default(RuntimeAnimatorController)){
        this.name = _name;
        this.maxHealth = _maxHealth;
        this.currentHealth = _maxHealth;
        this.shield = _shield;
        this.anim = _anim;
    }
    
    public void TakeDamage(float damage){
        currentHealth -= damage;
    }
    public void GetDefense(float ammount, CardDefense.Defense_Type type)
    {
        if(type == CardDefense.Defense_Type.Healing)
            currentHealth = (currentHealth + ammount > maxHealth)? maxHealth : currentHealth + ammount;
        else
            shield += ammount;
    }
    public void GetStatus(StatusEffect effect){
        Debug.Log(name + " received " + effect.effect);
        debuffs.Add(effect);
    }
}
