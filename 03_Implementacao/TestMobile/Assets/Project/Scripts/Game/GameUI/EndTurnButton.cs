using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    /*
        *Runtime class
        Ends the player turn when pressing the button
    */
    private GameEngine engine;
    void Start()
    {
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
    }

    //If player presses the collider it ends turn
    private void OnMouseDown()
    {
        engine.EndTurn();
    }
}
