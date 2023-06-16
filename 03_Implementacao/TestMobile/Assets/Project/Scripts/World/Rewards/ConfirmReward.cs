using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmReward : MonoBehaviour
{
    public EventEngine eventEngine;

    private void Start() {
        eventEngine = GameObject.FindGameObjectWithTag("EventEngine").GetComponent<EventEngine>();

    }
    private void OnMouseDown() {
        eventEngine.ShowNewArtifact(false);
    }
}
