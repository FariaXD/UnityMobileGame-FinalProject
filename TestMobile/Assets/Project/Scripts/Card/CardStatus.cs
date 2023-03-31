using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStatus : Card
{
    public StatusEffect.Effect effect = StatusEffect.Effect.Burn;
    public float duration;
    public float damage;
    public const Card_Type cardType = Card_Type.Status;

    public CardStatus(int _id, string _name, float _manaCost, StatusEffect.Effect _effect, float _duration, float _damage) : base(_id, _name, _manaCost, cardType)
    {
        this.effect = _effect;
        this.duration = _duration;
        this.damage = _damage;
    }

    public override void UseCardOnTarget(Character target)
    {
        StatusEffect newEffect = new StatusEffect(effect, damage, duration);
        target.GetStatus(newEffect);
    }
}
