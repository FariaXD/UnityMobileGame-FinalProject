using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    /*
        Class holding everything a playable character needs
    */
    public Deck deck = new Deck(); //Player's deck
    public Hand hand; //Players's Hand
    public AttackIcon icons; //Icons


    public Hero(string _name, float _health, float _shield, RuntimeAnimatorController _anim = default(RuntimeAnimatorController)) : base(_name, _health, _shield, _anim){
        this.hand = new Hand(deck);
        this.icons = new AttackIcon();
    }

}