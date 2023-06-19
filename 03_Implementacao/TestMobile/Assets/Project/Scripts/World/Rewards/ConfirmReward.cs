using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmReward : MonoBehaviour
{
    public EventEngine eventEngine;
    public CombatRewardEngine cRewardEngine;


    private void Start() {
        eventEngine = GameObject.FindGameObjectWithTag("EventEngine").GetComponent<EventEngine>();
        cRewardEngine = GameObject.FindGameObjectWithTag("CombatRewardEngine").GetComponent<CombatRewardEngine>();
    }
    private void OnMouseDown() {
        if(gameObject.tag == "Event")
            eventEngine.ShowNewArtifact(false);
        else
            cRewardEngine.ConfirmReward();
    }
}
