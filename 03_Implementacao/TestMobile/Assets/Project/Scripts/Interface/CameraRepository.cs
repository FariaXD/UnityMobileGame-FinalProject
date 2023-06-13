using UnityEngine;

public class CameraRepository : MonoBehaviour {
    public Camera mainScreen, stageSelector, combatStage, eventStage;

    public void DeactiveAllCameras(){
        mainScreen.enabled = false;
        stageSelector.enabled = false;
        combatStage.enabled = false;
        eventStage.enabled = false;
    }
}