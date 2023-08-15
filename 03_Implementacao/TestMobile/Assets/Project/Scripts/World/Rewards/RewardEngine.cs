using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardEngine : MonoBehaviour
{
    /*
    *Runtime class
    Delivers rewards to player
    */
    private GameEngine gameEngine;
    private ArtifactEngine artifactEngine;
    private float platinumChance = 20f;
    private float healChance = 30f;
    private int[] platinumRewards = new int[]{20,40};
    private int[] healRewards = new int[] { 10, 60 };

    private Dictionary<StageCombat.CombatType, float[]> artifactChances = new Dictionary<StageCombat.CombatType, float[]>();

    public enum RewardType{
        Gold,
        Platinum,
        Artifact,
        Heal,
        Shield
    }

    private void Start() {
        gameEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
        artifactEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<ArtifactEngine>();
        artifactChances.Add(StageCombat.CombatType.NORMAL, new float[]{55f,94f,99f,100f});
        artifactChances.Add(StageCombat.CombatType.ELITE, new float[] { 25f, 80f, 95f, 100f });
        artifactChances.Add(StageCombat.CombatType.BOSS, new float[] { 0f, 0f, 60f, 100f });
    }

    public void ReceiveRewardChoiceCombatSetReward(StageCombat stage){
        int index = 0;
        List<RewardType> rewards = GetNumberOfRewards(3);
        foreach(RewardType reward in rewards){
            switch(reward){
                case RewardType.Artifact:
                    gameEngine.combatEngine.cRewardEngine.SetReward(GetArtifactViaStage(stage.difficulty), index, RewardType.Artifact);
                    break;
                case RewardType.Platinum:
                    gameEngine.combatEngine.cRewardEngine.SetReward(Random.Range(platinumRewards[0],platinumRewards[1]), index, RewardType.Platinum);
                    break;
                case RewardType.Heal:
                    gameEngine.combatEngine.cRewardEngine.SetReward(Random.Range(healRewards[0], healRewards[1]), index, RewardType.Heal);
                    break;
            }
            index++;
        }
    }

    private Artifact GetArtifactViaStage(StageCombat.CombatType diff){
        float r = Random.Range(0f, 100f);
        Artifact.ArtifactRarity rarity = Artifact.ArtifactRarity.COMMON;
        if (r <= artifactChances[diff][0])
            rarity = Artifact.ArtifactRarity.COMMON;
        else if(r > artifactChances[diff][0] && r <= artifactChances[diff][1])
            rarity = Artifact.ArtifactRarity.RARE;
        else if(r > artifactChances[diff][1] && r <= artifactChances[diff][2])
            rarity = Artifact.ArtifactRarity.EPIC;
        else
            rarity = Artifact.ArtifactRarity.LEGENDARY;
        return artifactEngine.RequestNewArtifact(rarity);
    }

    private List<RewardType> GetNumberOfRewards(int numb){
        List<RewardType> rewards = new List<RewardType>();
        bool plat = false;
        bool heal = false;
        for (int i = 0; i < numb; i++)
        {
            float r = Random.Range(0f, 100f);
            if (r <= platinumChance && !plat){
                rewards.Add(RewardType.Platinum);
                plat = true;
            }
            else if(r > platinumChance && r <= platinumChance + healChance && !heal){
                rewards.Add(RewardType.Heal);
                heal = true;
            }
            else
                rewards.Add(RewardType.Artifact);
        } 
        return rewards;
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
    public void ReceiveReward(RewardType type, float ammount) {
        switch (type)
        {
            case RewardType.Heal:
                gameEngine.combatEngine.team.HealHeroesPercentage(ammount);
                break;
            case RewardType.Platinum:
                gameEngine.combatEngine.team.inventory.AddPlatinum(Mathf.RoundToInt(ammount));
                break;
        }
    }

    public void ReceiveReward(Artifact art){
        gameEngine.combatEngine.AddArtifact(art);
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
