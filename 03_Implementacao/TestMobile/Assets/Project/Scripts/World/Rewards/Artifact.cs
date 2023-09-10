using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact
{
    /*
    Artifact data class that holds all the methods needed to
    succesfully generate and run an artifact
    */
    public string name; //artifact name
    public string description; //artifact description
    public string method; //artifact method name to execute
    public string activated; //artifact activate condition
    public string sprite; //sprite
    public ArtifactRarity rarity; //artifact rarity

    //Activation conditions
    public enum ArtifactActivation{
        START_STAGE,
        START_TURN,
        PLAY_CARD,
        END_TURN,
        END_STAGE,
        PASSIVE
    }
    //Rarity
    public enum ArtifactRarity{
        COMMON,
        RARE,
        EPIC,
        LEGENDARY
    }
    //Artifact rarity colors
    public static List<Color> artColors = new List<Color>(){
        new Color(173/255f,173/255f,173/255f),
        new Color(47/255f,181/255f,250/255f),
        new Color(255/255f,69/255f,171/255f),
        new Color(255/255f,125/255f,23/255f)
    };
    //Load the sprite object via string 
    public Sprite GetSpriteViaString(){
        return Resources.Load<Sprite>(sprite);
    }
    //Receive rarity via string 
    public static ArtifactRarity GetRarityByName(string rarity){
        switch(rarity){
            case "common":
            return ArtifactRarity.COMMON;
            case "rare":
            return ArtifactRarity.RARE;
            case "epic":
            return ArtifactRarity.EPIC;
            case "legendary":
            return ArtifactRarity.LEGENDARY;
        }
        return ArtifactRarity.COMMON;
    }
    //Opposite of above
    public static string GetStringOfRarity(ArtifactRarity rarity){
        switch (rarity)
        {
            case ArtifactRarity.COMMON:
                return "common";
            case ArtifactRarity.RARE:
                return "rare";
            case ArtifactRarity.EPIC:
                return "epic";
            case ArtifactRarity.LEGENDARY:
                return "legendary";
        }
        return "common";
    }
    //Returns where it is activated
    public ArtifactActivation GetArtifactActivation(){
        switch (activated){
            case "StartStage":
            return ArtifactActivation.START_STAGE;
            case "StartTurn":
            return ArtifactActivation.START_TURN;
            case "PlayCard":
            return ArtifactActivation.PLAY_CARD;
            //case "ReceiveDamage":
            //return ArtifactActivation.RECEIVE_DAMAGE;
            case "EndTurn":
            return ArtifactActivation.END_TURN;
            case "EndStage":
            return ArtifactActivation.END_STAGE;
            case "Passive":
            return ArtifactActivation.PASSIVE;
        }
        return ArtifactActivation.START_STAGE;
    }
    //Get the color via the rarity
    public static Color GetColorViaRarity(ArtifactRarity rarity){
        return artColors[(int)rarity];
    }
}