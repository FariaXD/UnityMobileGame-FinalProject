using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    public float health;
    public float mana;
    public Animator anim;

    public Character(Animator _anim, float _health, float _mana){
        this.health = _health;
        this.mana = _mana;
        this.anim = _anim;
    }
    
    public void CheckIfAlive(){
        if(health <= 0)
            anim.SetBool("Dead", true);
    }


    public void TakeDamage(float damage){
        health -= damage;
        CheckIfAlive();
    }
}
