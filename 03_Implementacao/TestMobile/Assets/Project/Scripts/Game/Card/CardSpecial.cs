using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpecial : Card
{
    /*
        !Future work
    */
    public CardDamage damage;
    public CardStatus status;
    public CardDefense defense;
    public const Action_Type cardType = Action_Type.Special;

    public CardSpecial(int _id, string _name, float _manaCost, bool _area, CardDamage _damage, CardStatus _status, CardDefense _defense, string _imagePath) : base(_id, _name, _manaCost, _area, cardType, _imagePath)
    {
        this.damage = _damage;
        this.status = _status;
        this.defense = _defense;
    }

    public override void UseCardOnTarget(Character target)
    {
        
    }
}
