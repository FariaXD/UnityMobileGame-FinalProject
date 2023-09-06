using UnityEngine;
using System.Collections.Generic;

public class CombatRewardEngine : MonoBehaviour {
    /*
    Runtime class
    Sub engine of combat engine responsible for displaying the reward screen
    */
    public List<CombatRewardOptionEngine> options;
    private int selectedReward = 0;
    public bool rewardChosen = false;
    private CombatEngine combatEngine;
    private void Start() {
        combatEngine = GameObject.FindGameObjectWithTag("CombatEngine").GetComponent<CombatEngine>();

    }
    //Associate a reward to an option
    public void SetReward(object reward, int index, RewardEngine.RewardType type) {
        options[index].SetReward(reward, type);
    }
    //Select a reward 
    public void SelectReward(int index)
    {
        selectedReward = index;
        options.ForEach(opt => opt.SetSelected(false));
        options[index].SetSelected(true);
    }
    //Confirm selected reward
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
        Show(false); // hide reward screen
        options.ForEach(opt => opt.SetSelected(false)); //clear options
        selectedReward = 0;
        rewardChosen = true;
    }
    //Show screen
    public void Show(bool state)
    {   if(state)
            this.gameObject.transform.localPosition = Vector3.zero;
        else
            this.gameObject.transform.localPosition = new Vector3(0, 10000, 0);
        options[0].SetSelected(true);

    }
}