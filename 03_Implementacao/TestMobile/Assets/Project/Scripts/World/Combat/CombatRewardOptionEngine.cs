using UnityEngine;
using TMPro;
using System;

public class CombatRewardOptionEngine : MonoBehaviour {
    /*
    Runtime class
    Responsible for being an option in the select screen and getting assigned a reward
    */
    public RewardEngine.RewardType type; //reward type
    public SpriteRenderer rewardSprite; //reward image
    public SpriteRenderer targeted; //reward selected target
    public TextMeshProUGUI title; //title field
    public TextMeshProUGUI desc; //desc field
    public TextMeshProUGUI typeReward; //type field
    public int index; //index of option
    public CombatEngine combatEngine; //combat engine responsible for managing this
    public Artifact artifact; //assigned artifact can be null
    public float heal; //assigned heal value
    public float platinum; //assigned platinum value
    private CombatRewardEngine cRewardEngine;
    private void Start() {
        cRewardEngine = GameObject.FindGameObjectWithTag("CombatRewardEngine").GetComponent<CombatRewardEngine>();
    }
    //Set reward to option
    public void SetReward<T>(T reward, RewardEngine.RewardType type)
    {
        this.type = type;
        switch (type)
        {
            case RewardEngine.RewardType.Platinum:
                if (reward is int platinum)
                {
                    title.text = "Platinum";
                    title.color = Artifact.GetColorViaRarity(Artifact.ArtifactRarity.RARE);
                    desc.text = "Receive " + platinum + " platinum.";
                    typeReward.text = "Platinum";
                    this.platinum = platinum;
                    rewardSprite.sprite = Resources.Load<Sprite>("sprites/GameUI/gold");
                }
                break;
            case RewardEngine.RewardType.Artifact:
                if (reward is Artifact artifact)
                {
                    title.text = artifact.name;
                    title.color = Artifact.GetColorViaRarity(artifact.rarity);
                    desc.text = artifact.description;
                    typeReward.text = "Artifact";
                    this.artifact = artifact;
                    rewardSprite.sprite = artifact.GetSpriteViaString();
                }
                break;
            case RewardEngine.RewardType.Heal:
                if (reward is int heal)
                {
                    title.text = "Healing";
                    title.color = Artifact.GetColorViaRarity(Artifact.ArtifactRarity.COMMON);
                    desc.text = "Heal for " + heal + "% of characters max health.";
                    typeReward.text = "Heal";
                    this.heal = heal;
                    rewardSprite.sprite = Resources.Load<Sprite>("sprites/GameUI/Heal_Icon");
                }
                break;
        }
    }
    //Set this option has selected
    public void SetSelected(bool state){
        targeted.enabled = state;
    }
    //Select this option
    private void OnMouseDown() {
        cRewardEngine.SelectReward(index);
    }

}