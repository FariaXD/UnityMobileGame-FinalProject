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
        ReloadIcon();
    }

    public void IsCompleted(){
        stage.completed = true;
        stage.LoadImage();
        ReloadIcon();
    }

    public void ReloadIcon(){
        spriteRenderer.sprite = stage.image;
    }

    public void LoadStage(){
        if(stage != null){
            engine.LoadStage(this);
        }
    }

    public void StageCompleted(){
        stage.completed = true;
    }

    private void OnMouseDown() {
        if(!stage.completed)
            LoadStage();
    }
}