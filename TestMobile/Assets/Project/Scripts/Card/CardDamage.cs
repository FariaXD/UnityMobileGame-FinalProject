using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDamage : Card
{
    public float damage = 7f;
    public const Card_Type cardType = Card_Type.Damage;
    public CardDamage(int _id, string _name, float _manaCost, float _damage) : base(_id, _name, _manaCost, cardType)
    {
        this.damage = _damage;
    }

    public override void UseCardOnTarget(Character target){
        target.TakeDamage(damage);
    }
}
