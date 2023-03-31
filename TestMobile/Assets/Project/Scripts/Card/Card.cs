using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card {

    public string name;
    public float manaCost;
    public Card_Type type;
    public int id;

    public enum Card_Type
    {
        Damage,
        Status,
        Defense,
        Special
    }

    public Card(int _id, string _name, float _manaCost, Card_Type _type){
        this.name = _name;
        this.manaCost = _manaCost;
        this.type = _type;
        this.id = _id;
    }
    
    public abstract void UseCardOnTarget(Character target);
}
    

