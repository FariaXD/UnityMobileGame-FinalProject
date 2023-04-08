using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardButton : MonoBehaviour
{
    private GameEngine engine;
    void Start()
    {
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
    }

    
    private void OnMouseDown() {
        engine.DrawCard();
    }
}
