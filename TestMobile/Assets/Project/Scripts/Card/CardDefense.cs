using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDefense : Card
{
    public enum Defense_Type
    {
        Healing,
        Shielding
    }

    public float ammount = 7f;
    public Defense_Type defType;
    public CardDefense(int _id, string _name, float _manaCost, Card_Type _cardType, Defense_Type _defType, float _ammount) : base(_id, _name, _manaCost, _cardType)
    {
        this.ammount = _ammount;
        this.defType = _defType;
    }

    public override void UseCardOnTarget(Character target)
    {
        target.GetDefense(ammount, defType);
    }
}
