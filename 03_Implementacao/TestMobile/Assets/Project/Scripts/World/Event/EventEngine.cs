using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EventEngine : MonoBehaviour {
    public List<int> listOfUsedEvents = new List<int>();
    public List<OptionEngine> optionEngines = new List<OptionEngine>();
    public int checkedOption = 0;
    public StageEvent sEvent;
    public TextMeshProUGUI prompt;
    public bool active = false;
    public GameEngine engine;
    public GameObject newArtifactScreen;

    private void Start() {
        engine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
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
        engine.NewReward(optionEngines[checkedOption].option.reward);
    }
    //Returns to stage selector
    public void StageSelector(){
        engine.StageCompletedOrWorldEnded(true);
    }
    public void ShowNewArtifact(bool state, Artifact artifact = default(Artifact))
    {
        newArtifactScreen.SetActive(state);
        if(state)
            newArtifactScreen.GetComponent<NewArtifactScreenEngine>().SetText(artifact);
    }
}