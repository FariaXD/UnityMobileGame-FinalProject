using UnityEngine;
using System.Collections.Generic;

public class CombatRewardEngine : MonoBehaviour {
    public List<CombatRewardOptionEngine> options;
    private int selectedReward = 0;
    public bool rewardChosen = false;
    private CombatEngine combatEngine;
    private void Start() {
        combatEngine = GameObject.FindGameObjectWithTag("CombatEngine").GetComponent<CombatEngine>();

    }

    public void SetReward(object reward, int index, RewardEngine.RewardType type) {
        options[index].SetReward(reward, type);
    }

    public void SelectReward(int index)
    {
        selectedReward = index;
        options.ForEach(opt => opt.SetSelected(false));
        options[index].SetSelected(true);
    }

    public void ConfirmReward()
    {
        switch(options[selectedReward].type){
            case RewardEngine.RewardType.Heal:
            combatEngine.rewardEngine.ReceiveReward(RewardEngine.RewardType.Heal, options[selectedReward].heal);
                break;
            case RewardEngine.RewardType.Artifact:
                combatEngine.rewardEngine.ReceiveReward(options[selectedReward].artifact);
                break;
            case RewardEngine.RewardType.Platinum:
                combatEngine.rewardEngine.ReceiveReward(RewardEngine.RewardType.Platinum, options[selectedReward].platinum);
                break;
        }
        Show(false);
        options.ForEach(opt => opt.SetSelected(false));
        selectedReward = 0;
        rewardChosen = true;
    }

    public void Show(bool state)
    {   if(state)
            this.gameObject.transform.localPosition = Vector3.zero;
        else
            this.gameObject.transform.localPosition = new Vector3(0, 10000, 0);
        options[0].SetSelected(true);

    }
}