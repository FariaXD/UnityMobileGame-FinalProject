using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardButton : MonoBehaviour
{
    // !DEBUG CLASS
    private CombatEngine engine;
    private SpriteRenderer debug;
    void Start()
    {
        engine = GameObject.FindGameObjectWithTag("CombatEngine").GetComponent<CombatEngine>();
        debug = this.GetComponent<SpriteRenderer>();
    }

    
    private void OnMouseDown() {
        engine.DrawCard();
    }

    public void ChangeColor(Color color){
        debug.color = color;
    }
}
