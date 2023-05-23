using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card{

    /*
    Character action class represented as a card
    */
    public string cardName; //Name of card
    public float manaCost; //Cost of using
    public bool area; //If it targets every char or just one
    public Action_Type type; //Type of card
    public int id; //ID of card
    public Sprite cardSprite; //Image of card

    //Type of each card
    public enum Action_Type
    {
        Damage,
        Status,
        Defense,
        Special
    }

    public Card(int _id, string _name, float _manaCost, bool _area, Action_Type _type, string imagePath){
        this.area = _area;
        this.cardName = _name;
        this.manaCost = _manaCost;
        this.type = _type;
        this.id = _id;
        this.cardSprite = Resources.Load<Sprite>(imagePath);
    }
    
    //Abstract method
    public abstract void UseCardOnTarget(Character target);

}
    

