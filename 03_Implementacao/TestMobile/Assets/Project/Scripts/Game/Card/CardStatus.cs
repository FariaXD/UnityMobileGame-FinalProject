using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStatus : Card
{
    /*
        Card used to inflict status effects on target
    */
    public StatusEffect.Effect effect; //Effect
    public float duration; //Duration in turns
    public float currentDamage; //Damage to be given
    public float baseDamage; //Base damage

    public const Action_Type cardType = Action_Type.Status; //Type

    //Normal constructor for player characters
    public CardStatus(int _id, string _name, float _manaCost, bool _area, StatusEffect.Effect _effect, float _duration, float _damage, string _imagePath) : base(_id, _name, _manaCost, _area, cardType, _imagePath)
    {
        this.effect = _effect;
        this.duration = _duration;
        this.currentDamage = _damage;
        this.baseDamage = _damage;
    }
    //Overloaded constructor for enemy characters
    public CardStatus(bool _area, StatusEffect.Effect _effect, float _duration, float _damage) : base(0, "", 0, _area, cardType, "")
    {
        this.effect = _effect;
        this.duration = _duration;
        this.currentDamage = _damage;
        this.baseDamage = _damage;
    }
    //Use card on target
    public override void UseCardOnTarget(Character target)
    {
        StatusEffect newEffect = new StatusEffect(effect, currentDamage, duration);
        target.GetStatus(newEffect);
        target.TakeDamage(currentDamage);
    }
}
