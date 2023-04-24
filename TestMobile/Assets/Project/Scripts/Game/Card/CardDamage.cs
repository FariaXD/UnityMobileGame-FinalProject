using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDamage : Card
{
    public float damage = 7f;
    public const Action_Type cardType = Action_Type.Damage;
    public CardDamage(int _id, string _name, float _manaCost, bool _area, float _damage, string imagePath) : base(_id, _name, _manaCost, _area, cardType, imagePath)
    {
        this.damage = _damage;
    }
    public CardDamage(bool _area, float _damage) : base(0, "", 0, _area, cardType, ""){
        this.damage = _damage;
    }

    public override void UseCardOnTarget(Character target){
        target.TakeDamage(damage);
    }
}
