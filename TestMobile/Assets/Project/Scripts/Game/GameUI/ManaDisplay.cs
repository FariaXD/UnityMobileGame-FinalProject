using UnityEngine;
using TMPro;

public class ManaDisplay : MonoBehaviour {

    /*
        *Runtime class
        Displays current mana
    */
    private GameEngine engine;
    private TextMeshProUGUI text;
    private float manaDisplayed = Team.startingMana;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
    }


    private void Update()
    {
        if(manaDisplayed != engine.team.currentMana){
            text.text = engine.team.currentMana + "/" + Team.startingMana;
            manaDisplayed = engine.team.currentMana;
        }
    }
}