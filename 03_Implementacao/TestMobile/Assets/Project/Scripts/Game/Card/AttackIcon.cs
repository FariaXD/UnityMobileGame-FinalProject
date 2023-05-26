using UnityEngine;

public class AttackIcon{
    /*
        Class used to load every icon so they can be displayed dynamically
    */

    public Sprite attackIcon, burnIcon, poisonIcon, shockedIcon, shieldIcon, healIcon;

    public AttackIcon() {
        attackIcon = Resources.Load<Sprite>("sprites/GameUI/Attack_Icon");
        burnIcon = Resources.Load<Sprite>("sprites/GameUI/Burn_Icon");
        poisonIcon = Resources.Load<Sprite>("sprites/GameUI/Poison_Icon");
        shockedIcon = Resources.Load<Sprite>("sprites/GameUI/Shock_Icon");
        shieldIcon = Resources.Load<Sprite>("sprites/GameUI/Shield_Icon");
        healIcon = Resources.Load<Sprite>("sprites/GameUI/Heal_Icon");
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