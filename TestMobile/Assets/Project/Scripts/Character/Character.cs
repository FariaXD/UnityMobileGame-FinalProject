using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    public string name;
    public float maxHealth;
    public float currentHealth;
    public float shield;
    public Animator anim;
    //List containing all the status affecting the character
    public List<StatusEffect> debuffs = new List<StatusEffect>(); 
    public Character(string _name, Animator _anim, float _maxHealth, float _shield){
        this.name = _name;
        this.maxHealth = _maxHealth;
        this.currentHealth = _maxHealth;
        this.anim = _anim;
        this.shield = _shield;
    }
    
    public void CheckIfAlive(){
        if(currentHealth <= 0)
            anim.SetBool("Dead", true);
    }
    public void TakeDamage(float damage){
        currentHealth -= damage;
        CheckIfAlive();
    }
    public void GetDefense(float ammount, CardDefense.Defense_Type type)
    {
        if(type == CardDefense.Defense_Type.Healing)
            currentHealth = (currentHealth + ammount > maxHealth)? maxHealth : currentHealth + ammount;
        else
            shield += ammount;
    }
    public void GetStatus(StatusEffect effect){
        debuffs.Add(effect);
    }
}
