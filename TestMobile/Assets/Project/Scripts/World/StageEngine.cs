using UnityEngine;

public class StageEngine : MonoBehaviour {
    public Stage stage;

    private WorldEngine engine;
    public SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        engine = GameObject.FindGameObjectWithTag("WorldEngine").GetComponent<WorldEngine>();
    }

    public void SetStage(Stage stage){
        this.stage = stage;
    }

    public void LoadStage(){
        if(stage != null){
            engine.LoadStage(stage);
        }
    }

    private void OnMouseDown() {
        LoadStage();
    }
}