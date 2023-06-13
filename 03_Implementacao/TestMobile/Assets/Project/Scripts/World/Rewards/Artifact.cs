using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact
{
    public string name;
    public string description;
    public string method;

    public enum ArtifactRarity{
        COMMON,
        RARE,
        EPIC,
        LEGENDARY
    }
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

}
