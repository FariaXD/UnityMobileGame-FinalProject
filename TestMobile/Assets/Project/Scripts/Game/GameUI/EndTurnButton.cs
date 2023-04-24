using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    private GameEngine engine;
    void Start()
    {
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
    }


    private void OnMouseDown()
    {
        Debug.Log("Ending Turn");
        engine.EndTurn();
    }

   
}
