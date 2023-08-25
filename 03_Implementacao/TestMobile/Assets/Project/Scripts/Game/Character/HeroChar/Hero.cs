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
    public Sprite cardTemplate; //Hero card template
    public Sprite heroToken; //hero token


    public Hero(string _name, float _health, float _shield, string _cardTemplate, RuntimeAnimatorController _anim) : base(_name, _health, _shield, _anim){
        this.hand = new Hand(deck);
        this.cardTemplate = Resources.Load<Sprite>(_cardTemplate);
    }

}