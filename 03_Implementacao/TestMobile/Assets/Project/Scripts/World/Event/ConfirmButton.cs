using UnityEngine;

public class ConfirmButton : MonoBehaviour {
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