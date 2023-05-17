using UnityEngine;

public class StageEngine : MonoBehaviour {
    public Stage stage;

    private WorldEngine engine;

    private void Start() {
        engine = GameObject.FindGameObjectWithTag("WorldEngine").GetComponent<WorldEngine>();
    }

    public void SetStage(Stage stage){
        this.stage = stage;
    }

    public void LoadStage(){
        engine.LoadStage(stage);
    }

    private void OnMouseDown() {
        LoadStage();
    }
}