﻿using UnityEngine;

public class AttackIcon{
    /*
        Classed used to load every icon so they can be displayed dynamically
    */

    public Sprite attackIcon, burnIcon, poisonIcon, shockedIcon, shieldIcon, healIcon;

    public AttackIcon() {
        attackIcon = Resources.Load<Sprite>("sprites/GameUI/attackIcon");
        burnIcon = Resources.Load<Sprite>("sprites/GameUI/burnIcon");
        poisonIcon = Resources.Load<Sprite>("sprites/GameUI/poisonIcon");
        shockedIcon = Resources.Load<Sprite>("sprites/GameUI/shockIcon");
        shieldIcon = Resources.Load<Sprite>("sprites/GameUI/shieldIcon");
        healIcon = Resources.Load<Sprite>("sprites/GameUI/healIcon");
    }
    //Returns status icon based on effect
    public Sprite GetStatusIcon(StatusEffect eff){
        switch(eff.effect){
            case StatusEffect.Effect.Burn:
                return burnIcon;
            case StatusEffect.Effect.Poison:
                return poisonIcon;
            case StatusEffect.Effect.Shock:
                return shockedIcon;
        }
        return null;
    }

}