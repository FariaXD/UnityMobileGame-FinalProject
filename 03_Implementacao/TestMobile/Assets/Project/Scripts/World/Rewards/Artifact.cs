using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact
{
    /*
    Artifact data class that holds all the methods needed to create
    to succesfully generate and run an artifact
    */
    public string name;
    public string description;
    public string method;
    public string activated;

    public enum ArtifactActivation{
        START_STAGE,
        START_TURN,
        PLAY_CARD,
        END_TURN,
        END_STAGE,
        PASSIVE
    }
    //Rariry
    public enum ArtifactRarity{
        COMMON,
        RARE,
        EPIC,
        LEGENDARY
    }
    public List<Color> artColors = new List<Color>(){
        new Color(173,173,173),
        new Color(47,181,250),
        new Color(255,69,171),
        new Color(255,125,23)
        };
    /*<summary> 
    Receive rarity via string 

    </summary>*/

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

    public Color GetColorViaRarity(ArtifactRarity rarity){
        return artColors[(int)rarity];
    }
}
