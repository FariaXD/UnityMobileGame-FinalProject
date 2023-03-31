using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    public enum Effect
    {
        Burn, //End of turn damage
        Poison, //Action damage
        Shocked //Stun
    }

    public float duration = 3f;
    public float damage = 3f;
    public Effect effect = Effect.Burn;

    public StatusEffect(Effect _effect, float _damage, float _duration){
        this.effect = _effect;
        this.damage = _damage;
        this.duration = _duration;
    }

    public bool DecreaseDuration(float quantity){
        duration-=quantity;
        if(duration <= 0)
            return true;
        return false;
    }
}
