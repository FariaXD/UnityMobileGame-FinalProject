using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    /*
        *Runtime class
        Ends the player turn when pressing the button
    */
    private CombatEngine engine;
    void Start()
    {
        engine = GameObject.FindGameObjectWithTag("CombatEngine").GetComponent<CombatEngine>();
    }

    //If player presses the collider it ends turn
    private void OnMouseDown()
    {
        engine.EndTurn();
    }
}
