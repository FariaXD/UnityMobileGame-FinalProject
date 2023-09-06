using UnityEngine;
using TMPro;

public class OptionEngine : MonoBehaviour {
    /*
    *Runtime Class
    When the player clicks an option this class sets the respective values
    */

    public int optionID = -1;
    public EventOption option;
    public SpriteRenderer selected;
    public TextMeshProUGUI optionText;
    public EventEngine engine;
    public Sprite confirmedTarget;
    public Sprite selectedTarget;
    private void Start() {
        engine = GameObject.FindGameObjectWithTag("EventEngine").GetComponent<EventEngine>();
    }
    public void SetOption(EventOption option){
        selected.sprite = selectedTarget;
        this.option = option;
        optionText.text = option.name;
    }

    public void SetSelected(bool state){
        selected.enabled = state;
    }
    public void SetConfirmed(bool state){
        selected.sprite = confirmedTarget;
    }

    private void OnMouseDown() {
        engine.SelectNewOption(optionID);
    }
}