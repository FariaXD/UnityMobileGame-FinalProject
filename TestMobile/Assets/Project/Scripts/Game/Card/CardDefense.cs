using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDefense : Card
{
    public enum Defense_Type
    {
        Healing,
        Shielding,
        None
    }

    public float ammount = 7f;
    public const Action_Type cardType = Action_Type.Defense;
    public Defense_Type defType;
    public CardDefense(int _id, string _name, float _manaCost, bool _area, Defense_Type _defType, float _ammount, string imagePath) : base(_id, _name, _manaCost, _area, cardType, imagePath)
    {
        this.ammount = _ammount;
        this.defType = _defType;
    }

    public CardDefense(bool _area, Defense_Type _defType, float _ammount) : base(0, "", 0, _area, cardType, ""){
        this.ammount = _ammount;
        this.defType = _defType;
    }

    public override void UseCardOnTarget(Character target)
    {
        target.GetDefense(ammount, defType);
    }
    
    public static Defense_Type GetDefenseByName(string name)
    {
        switch (name)
        {
            case "Healing":
                return Defense_Type.Healing;
            case "Shielding":
                return Defense_Type.Shielding;
            
        }
        return Defense_Type.None;
    }
}
