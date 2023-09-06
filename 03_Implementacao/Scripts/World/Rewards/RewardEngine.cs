using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardEngine : MonoBehaviour
{
    /*
    *Runtime class
        Delivers rewards to player
        Responsible for creating new rewards
    */
    private GameEngine gameEngine;
    private ArtifactEngine artifactEngine;
    private float platinumChance = 20f; //platinum chance
    private float healChance = 30f; //heal chance
    private int[] platinumRewards = new int[]{20,40}; //reward range
    private int[] healRewards = new int[] { 10, 60 }; //reward range

    private Dictionary<StageCombat.CombatType, float[]> artifactChances = new Dictionary<StageCombat.CombatType, float[]>(); //Artifact rarity chances

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
    //Player receives the chosen reward
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
    //Get a new artifact depending on the diff of the stage
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
    //Get a number of rewards
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
    //Player receives reward
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
    //Activate reward if its currency or heal
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
    //Receive a new artifact reward
    public void ReceiveReward(Artifact art){
        gameEngine.combatEngine.AddArtifact(art);
    }
    //Get reward by name
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
