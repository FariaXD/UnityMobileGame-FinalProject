using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardButton : MonoBehaviour
{
    private GameEngine engine;
    private SpriteRenderer debug;
    void Start()
    {
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
        debug = this.GetComponent<SpriteRenderer>();
    }

    
    private void OnMouseDown() {
        engine.DrawCard();
    }

    public void ChangeColor(Color color){
        debug.color = color;
    }
}
