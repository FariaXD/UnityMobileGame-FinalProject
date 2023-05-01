using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStatus : Card
{
    public StatusEffect.Effect effect;
    public float duration;
    public float damage;
    public const Action_Type cardType = Action_Type.Status;

    public CardStatus(int _id, string _name, float _manaCost, bool _area, StatusEffect.Effect _effect, float _duration, float _damage, string imagePath) : base(_id, _name, _manaCost, _area, cardType, imagePath)
    {
        this.effect = _effect;
        this.duration = _duration;
        this.damage = _damage;
    }

    public CardStatus(bool _area, StatusEffect.Effect _effect, float _duration, float _damage) : base(0, "", 0, _area, cardType, "")
    {
        this.effect = _effect;
        this.duration = _duration;
        this.damage = _damage;
    }

    public override void UseCardOnTarget(Character target)
    {
        StatusEffect newEffect = new StatusEffect(effect, damage, duration);
        target.GetStatus(newEffect);
        target.TakeDamage(damage);
    }
}
