using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public Deck deck = new Deck();
    public Hand hand;

    public Hero(string _name, float _health, float _shield, RuntimeAnimatorController _anim = default(RuntimeAnimatorController)) : base(_name, _health, _shield, _anim){
        this.hand = new Hand(deck);
    }

}