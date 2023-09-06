using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EventEngine : MonoBehaviour {
    /*
    Runtime class
    Responsible for controlling the event stage
    */
    public List<int> listOfUsedEvents = new List<int>(); //list of used events
    public List<OptionEngine> optionEngines = new List<OptionEngine>(); //option buttons
    public int checkedOption = 0; //selected option
    public StageEvent sEvent; //associated event
    public TextMeshProUGUI prompt; //prompt text field
    public bool active = false; //active state
    public RewardEngine rewardEngine; //reward engine
    public GameEngine gameEngine; //main game engine
    public GameObject newArtifactScreen; // object that displays a reward

    private void Start() {
        rewardEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<RewardEngine>();
        gameEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();

    }
    //Loads the stage
    public void NewStageEvent(StageEvent _event){
        RestartValues();
        this.sEvent = _event;
        optionEngines[0].SetOption(sEvent.sEvent.option1);
        optionEngines[1].SetOption(sEvent.sEvent.option2);
        optionEngines[2].SetOption(sEvent.sEvent.option3);
        prompt.text = sEvent.sEvent.prompt;
        active = true;
    }
    //Selects new option
    public void SelectNewOption(int optionId){
        if(active){
            optionEngines[checkedOption].SetSelected(false);
            checkedOption = optionId;
            optionEngines[checkedOption].SetSelected(true);
        }
    }
    //Restarts values
    private void RestartValues(){
        optionEngines[checkedOption].SetSelected(false);
        checkedOption = 0;
        optionEngines[checkedOption].SetSelected(true);
    }
    //Confirms chosen option
    public void ConfirmOption(){
        active = false;
        prompt.text = optionEngines[checkedOption].option.result;
        optionEngines[checkedOption].SetConfirmed(true);
        NewReward(optionEngines[checkedOption].option.reward);
    }
    //Returns to stage selector
    public void StageSelector(){
        gameEngine.StageCompletedOrWorldEnded(true);
    }
    //Show a popup that displays an artifact received
    public void ShowNewArtifact(bool state, Artifact artifact = default(Artifact))
    {
        newArtifactScreen.SetActive(state);
        if(state)
            newArtifactScreen.GetComponent<NewArtifactScreenEngine>().SetText(artifact);
    }
    //Receives a new reward
    public void NewReward(EventReward reward)
    {
        rewardEngine.ReceiveReward(RewardEngine.GetRewardTypeByName(reward.item), reward.value);
    }
}