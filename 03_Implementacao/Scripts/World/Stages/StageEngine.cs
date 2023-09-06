using UnityEngine;

public class StageEngine : MonoBehaviour {
    /*
    *Runtime Class
    Stage Engine is a runtime class that loads a received stage
    and when clicked on the game gives that information to the main program
    engine
    */
    public Stage stage;
    public int level = 0;
    public int stagePos = 0;
    private WorldEngine engine;
    public SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        engine = GameObject.FindGameObjectWithTag("WorldEngine").GetComponent<WorldEngine>();
    }

    public void SetStage(Stage stage){
        this.stage = stage;
        ReloadIcon();
        this.GetComponent<BoxCollider>().enabled = (stage.type == Stage.StageType.NONE) ? false : true;
        
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
        if(stage != null && stage.pathNumber == engine.currentLevel){
            engine.LoadStage(this);
        }
    }

    private void OnMouseDown() {
        if(stage != null && !stage.completed)
            LoadStage();
    }
}