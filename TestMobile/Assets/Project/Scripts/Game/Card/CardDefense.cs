using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDefense : Card
{
    /*
    Action used to protect characters
    */
    public enum Defense_Type //Type of defense
    {
        Healing,
        Shielding,
        None
    }

    public float currentAmmount = 7f; //Ammount of defense to give
    public float baseAmmount = 7f;
    public const Action_Type cardType = Action_Type.Defense; //Type of card
    public Defense_Type defType; //Defense type

    //Normal constructor for player characters
    public CardDefense(int _id, string _name, float _manaCost, bool _area, Defense_Type _defType, float _ammount, string imagePath) : base(_id, _name, _manaCost, _area, cardType, imagePath)
    {
        this.currentAmmount = _ammount;
        this.baseAmmount = _ammount;
        this.defType = _defType;
    }
    //Overloaded constructor for enemy characters
    public CardDefense(bool _area, Defense_Type _defType, float _ammount) : base(0, "", 0, _area, cardType, ""){
        this.currentAmmount = _ammount;
        this.baseAmmount = _ammount;
        this.defType = _defType;
    }
    //Give defense to target
    public override void UseCardOnTarget(Character target)
    {
        target.GetDefense(currentAmmount, defType);
    }
    //Get defense type via a string
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
