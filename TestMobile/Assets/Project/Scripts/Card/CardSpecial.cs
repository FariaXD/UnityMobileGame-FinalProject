using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpecial : Card
{
    public CardDamage damage;
    public CardStatus status;
    public CardDefense defense;
    public CardSpecial(int _id, string _name, float _manaCost, Card_Type _type) : base(_id, _name, _manaCost, _type)
    {
    }

    public override void UseCardOnTarget(Character target)
    {
        
    }
}
