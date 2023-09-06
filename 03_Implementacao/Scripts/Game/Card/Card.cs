using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Card{

    /*
    Character action class represented as a card
    */
    public string cardName; //Name of card
    public float manaCost; //Cost of using
    public bool area; //If it targets every char or just one
    public Action_Type type; //Type of card
    public int id; //ID of card
    public Sprite cardImage, cardTypeIcon; //Image of card
    public PartyStats ps;

    //Type of each card
    public enum Action_Type
    {
        Damage,
        Status,
        Defense,
        Special
    }

    public Card(int _id, string _name, float _manaCost, bool _area, Action_Type _type, string imagePath){
        this.area = _area;
        this.cardName = _name;
        this.manaCost = _manaCost;
        this.type = _type;
        this.id = _id;
        this.cardImage = Resources.Load<Sprite>(imagePath);
        cardTypeIcon = LoadCardTypeDynamically(this);
    }

    public void InitializeStats(PartyStats ps){
        this.ps = ps; 
    }

    //Returns the card description
    public string GetCardDescriptionDynamically(){
        switch(type){
            case Action_Type.Damage:
                CardDamage dmg = (CardDamage) this;
                return "Deals " + dmg.baseDamage + " points of damage";
            case Action_Type.Status:
                CardStatus stt = (CardStatus)this;
                return "Deals " + stt.baseDamage + " points of damage and applies " + stt.effect;
            case Action_Type.Defense:
                CardDefense def = (CardDefense)this;
                if(def.defType == CardDefense.Defense_Type.Healing)
                    return "Heals for " + def.baseAmmount + " points";
                else
                    return "Shields for " + def.baseAmmount + " points";
        }
        return "";
    }

    //Loads the card type icon returning the sprite
    public static Sprite LoadCardTypeDynamically(Card c){
        switch (c.type)
            {
                case Action_Type.Damage:
                    CardDamage dmg = (CardDamage)c;
                    return Resources.Load<Sprite>("sprites/cards/CardIcons/damage");
                case Action_Type.Status:
                    CardStatus stt = (CardStatus)c;
                    return Resources.Load<Sprite>("sprites/cards/CardIcons/"+stt.effect); ;
                case Action_Type.Defense:
                    CardDefense def = (CardDefense)c;
                    return Resources.Load<Sprite>("sprites/cards/CardIcons/" + def.defType);
            }
            return null;
    }
    //Get ammount of damage/defense a card gives 
    public int GetAmmountDynamically(){
        switch (type)
        {
            case Action_Type.Damage:
                CardDamage dmg = (CardDamage) this;
                return Mathf.RoundToInt(dmg.currentDamage * ps.cardDamageMultipler);
            case Action_Type.Status:
                CardStatus stt = (CardStatus) this;
                return Mathf.RoundToInt(stt.currentDamage * ps.cardStatusMultipler);
            case Action_Type.Defense:
                CardDefense def = (CardDefense) this;
                return Mathf.RoundToInt(def.currentAmmount * ps.cardDefenseMultipler);
        }
        return 0;
    }
    //Abstract method - card effect to be implemented by different types
    public abstract void UseCardOnTarget(Character target, PartyStats ps);

}
    

