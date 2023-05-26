using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDamage : Card
{
    /*
    Action focused on dealing damage to a character
    */
    public float currentDamage = 7f; //Damage to give
    public float baseDamage = 7f;
    public const Action_Type cardType = Action_Type.Damage;

    //Normal constructor mainly used for player actions
    public CardDamage(int _id, string _name, float _manaCost, bool _area, float _damage, string _imagePath) : base(_id, _name, _manaCost, _area, cardType, _imagePath)
    {
        this.currentDamage = _damage;
        this.baseDamage = _damage;
    }
    //Overloaded constructor used for enemy actions
    public CardDamage(bool _area, float _damage) : base(0, "", 0, _area, cardType, ""){
        this.currentDamage = _damage;
        this.baseDamage = _damage;
    }
    //Deal damage to target
    public override void UseCardOnTarget(Character target){
        target.TakeDamage(currentDamage);
    }
}
