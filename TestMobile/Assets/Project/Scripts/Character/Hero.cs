using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public Deck deck;
    public Hand hand;
    public Hero(Animator _anim, float _health, float _shield) : base(_anim, _health, _shield){}

}
