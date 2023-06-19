using UnityEngine;
using TMPro;
using System;

public class CombatRewardOptionEngine : MonoBehaviour {
    public RewardEngine.RewardType type;
    public SpriteRenderer rewardSprite;
    public SpriteRenderer targeted;
    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;
    public TextMeshProUGUI typeReward;
    public int index;
    public CombatEngine combatEngine;
    public Artifact artifact;
    public float heal;
    public float platinum;
    private CombatRewardEngine cRewardEngine;
    private void Start() {
        cRewardEngine = GameObject.FindGameObjectWithTag("CombatRewardEngine").GetComponent<CombatRewardEngine>();
    }

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

    public void SetSelected(bool state){
        targeted.enabled = state;
    }

    private void OnMouseDown() {
        cRewardEngine.SelectReward(index);
    }

}