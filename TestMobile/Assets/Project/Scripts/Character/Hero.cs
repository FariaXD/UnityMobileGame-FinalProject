using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public Deck deck = new Deck();
    public Hand hand;
    public Hero(string _name, Animator _anim, float _health, float _shield) : base(_name, _anim, _health, _shield){
        this.hand = new Hand(deck);
    }

}