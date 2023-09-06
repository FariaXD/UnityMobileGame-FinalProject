using UnityEngine;

public class ConfirmButton : MonoBehaviour {
    /*
    Button to confirm reward
    */
    public EventEngine engine;
    private void Start()
    {
        engine = GameObject.FindGameObjectWithTag("EventEngine").GetComponent<EventEngine>();
    }
    private void OnMouseDown() {
        if(engine.active)
            engine.ConfirmOption();
        else
            engine.StageSelector();
    }
}