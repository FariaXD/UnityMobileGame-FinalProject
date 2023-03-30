using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card {

    public string name;
    public float manaCost;
    public float rarity;
    public bool used = false;
    public Card_Type type;

    public enum Card_Type
    {
        Damage,
        Status,
        Defense,
        Special
    }

    public Card(string _name, float _manaCost, float _rarity, Card_Type _type){
        this.name = _name;
        this.manaCost = _manaCost;
        this.rarity = _rarity;
        this.type = _type;
    }
    public abstract void UseCardOnTarget(Character target);
}
    

