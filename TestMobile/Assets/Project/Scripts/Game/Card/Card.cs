using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card{

    public string cardName;
    public float manaCost;
    public bool area;
    public Action_Type type;
    public int id;
    public Sprite cardSprite;

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
    
    public abstract void UseCardOnTarget(Character target);

}
    

