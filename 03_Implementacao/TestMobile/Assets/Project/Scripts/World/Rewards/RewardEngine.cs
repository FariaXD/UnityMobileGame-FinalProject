using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardEngine : MonoBehaviour
{
    private GameEngine gameEngine;

    public enum RewardType{
        Gold,
        Platinum,
        Artifact,
        Heal,
        Shield
    }

    private void Start() {
        gameEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
    }

    public void ReceiveReward(RewardType type, string ammount){
        switch(type) {
            case RewardType.Gold:
                gameEngine.combatEngine.team.inventory.AddGold(int.Parse(ammount));
                break;
            case RewardType.Platinum:
                gameEngine.combatEngine.team.inventory.AddPlatinum(int.Parse(ammount));
                break;
            case RewardType.Artifact:
                gameEngine.AddArtifact(Artifact.GetRarityByName(ammount));
                break;
            case RewardType.Heal:
                gameEngine.combatEngine.team.HealHeroesPercentage(float.Parse(ammount));
                break;
            case RewardType.Shield:
                gameEngine.combatEngine.team.ShieldHeroesAmmount(float.Parse(ammount));
                break;
        }
    }

    public static RewardType GetRewardTypeByName(string name){
        switch(name){
            case "gold":
                return RewardType.Gold;
            case "platinum":
                return RewardType.Platinum;
            case "artifact":
                return RewardType.Artifact;
            case "heal":
                return RewardType.Heal;
            case "shield":
                return RewardType.Shield;
        }
        return RewardType.Gold;
    }
}
