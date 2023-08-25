using UnityEngine;

public class CardIcon {
    /*
        Class used to load every icon so they can be displayed dynamically
    */

    public Sprite damageIcon, shieldIcon, healIcon;

    public CardIcon()
    {
        damageIcon = Resources.Load<Sprite>("sprites/GameUI/Shock_Icon");
        shieldIcon = Resources.Load<Sprite>("sprites/GameUI/Shield_Icon");
        healIcon = Resources.Load<Sprite>("sprites/GameUI/Heal_Icon");
    }
}