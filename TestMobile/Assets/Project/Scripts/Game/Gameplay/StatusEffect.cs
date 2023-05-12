using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    /*
        Method containing information regardind a status effect or debuff
    */
    public enum Effect
    {
        Burn, //End of turn damage
        Poison, //Action damage
        Shock, //Stun can't use actions
        None
    }

    public float duration = 3f; //duration of status decreasing each turn
    public float damage = 3f; //damage 
    public Effect effect ; //effect type

    public StatusEffect(Effect _effect, float _damage, float _duration){
        this.effect = _effect;
        this.damage = _damage;
        this.duration = _duration;
    }

    //Decrese duration
    public bool DecreaseDuration(float quantity){
        duration-=quantity;
        if(duration <= 0)
            return true;
        return false;
    }
    //Get respective effect via string
    public static Effect GetEffectByName(string name){
        switch (name){
            case "Burn":
                return Effect.Burn;
            case "Poison":
                return Effect.Poison;
            case "Shock":
                return Effect.Shock;   
        }
        return Effect.None;      
    }
}
