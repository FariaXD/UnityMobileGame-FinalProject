using UnityEngine;

public class CameraRepository : MonoBehaviour {
    public Camera mainScreen, stageSelector, game;

    public void DeactiveAllCameras(){
        mainScreen.enabled = false;
        stageSelector.enabled = false;
        game.enabled = false;
    }
}